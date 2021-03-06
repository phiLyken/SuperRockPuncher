﻿using System.Collections;
using DG.Tweening;
using UnityEngine;


using System;

public class Player : MonoBehaviour
{
    public enum PlayerActions
    {
        move_in, move_out, move_forward, punch
    }

    [Range(0.001f, 2.0f)]
    public float StepLength = 0.1f;
    [Range(0.001f, 1.5f)]
    public float CooldownInSec = 0.1f;
    [Range(0.001f, 1f)]
    public float StepTransitionTime = 0.3f;

    public Ease StepEase = Ease.InOutCubic;
    public Transform Lane;
    public bool IsInBooth { get; private set; }

    private Transform _transform;
    private float _timeOfLastAction;


    private int _boothLayer;
    private int _obstacleLayer;
    private Animator _animator;
    private PunchMeter _punchMeter;
    private bool _isPunching;

   
    public static Action<PlayerActions> OnAction;
    public void Awake()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _punchMeter = GetComponent<PunchMeter>();

        var recognizer = GetComponent<GestureRecognizer>();
        recognizer.OnSwipe += HandleSwipe;

        _boothLayer = LayerMask.NameToLayer("Booth");
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    public void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            TryMoveStepUp();
        }
    }

    private void MoveStepUp()
    {
        OnAction.AttemptCall(PlayerActions.move_forward);
        _animator.SetTrigger("Step");

        _transform.DOMoveY(_transform.position.y + StepLength, StepTransitionTime * StepLength)
            .SetEase(StepEase);
        _timeOfLastAction = Time.time;
    }

    private void HandleSwipe(GestureRecognizer.SwipeDirection dir)
    {
        if (IsCoolingDown())
        {
            return;
        }

        switch (dir)
        {
            case GestureRecognizer.SwipeDirection.Up:
                TriggerAttack();
                break;
            case GestureRecognizer.SwipeDirection.Left:
                TryToMoveInOrOutOfBooth(Vector2.left);
                break;
            case GestureRecognizer.SwipeDirection.Right:
                TryToMoveInOrOutOfBooth(Vector2.right);
                break;
        }
    }

    private void TriggerAttack()
    {
        if (IsCoolingDown())
            return;

        StartCoroutine(ExecuteAttack());
    }

    private IEnumerator ExecuteAttack()
    {
        var punchDuration = _punchMeter.GetRange();
        OnAction.AttemptCall(PlayerActions.punch);
        //Do a step before punching
        _transform.DOMoveY(_transform.position.y + StepLength / StepTransitionTime * punchDuration, punchDuration)
            .SetEase(StepEase);

        _punchMeter.Punch();
        _animator.SetBool("Punching", true);
        _timeOfLastAction = Time.time;
        _isPunching = true;

        while (punchDuration > 0f)
        {
            Attack();
            punchDuration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _isPunching = false;
        _animator.SetBool("Punching", false);
    }

    public bool CanAttack(out GameObject obstacle)
    {
        obstacle = GetObjectInDirectionWithinDistance(Vector2.up, _obstacleLayer, 1f);

        return obstacle != null;
    }

    private void Attack()
    {
        GameObject obstacle;
       // Debug.Log("Attempting Attack!");
        if (CanAttack(out obstacle))
        {
        //    Debug.Log("Attack!");
            obstacle.GetComponent<Collider2D>().enabled = false;
            ExplodeObject explode = obstacle.GetComponent<ExplodeObject>();
            if (explode != null)
            {
                explode.SpawnParticles();
            }
           
            PointCounter.Instance.ObstacleDestroyed();

            Destroy(obstacle);
        }
    }

    private void TryToMoveInOrOutOfBooth(Vector2 dir)
    {
        if (IsInBooth)
        {
            TryToMoveOutOfBooth(dir);
        }
        else
        {
            TryToMoveIntoBooth(dir);
        }
    }

    private void TryToMoveOutOfBooth(Vector2 dir)
    {
        var dirToLane = Lane.position - _transform.position;

        if (Vector3.Dot(dirToLane, dir) > 0)
        {

            OnAction.AttemptCall(PlayerActions.move_out);
          
            _animator.SetBool("RunToBooth", true);

            _transform.DOMove(new Vector2(Lane.position.x, _transform.position.y),
                StepTransitionTime*Mathf.Abs(dirToLane.x)*StepLength)
                .SetEase(StepEase)
                .OnComplete(() => _animator.SetBool("RunToBooth", false));
            IsInBooth = false;
            _timeOfLastAction = Time.time;
        }
    }

    private void TryToMoveIntoBooth(Vector2 dir)
    {
        var booth = GetObjectInDirectionWithinDistance(dir, _boothLayer);
        if (booth == null) return;
        OnAction.AttemptCall(PlayerActions.move_in);
        _animator.SetBool("RunToBooth", true);
        var dirToBooth = booth.transform.position - _transform.position;
        _transform.DOMove(booth.transform.position, StepTransitionTime * StepLength * dirToBooth.magnitude)
            .SetEase(StepEase)
            .OnComplete(() => _animator.SetBool("RunToBooth", false));

        IsInBooth = true;
        _timeOfLastAction = Time.time;
    }

    private GameObject GetObjectInDirectionWithinDistance(Vector2 dir, int layer, float dist = 0f)
    {
        RaycastHit2D hit;

        if (dist > 0f)
        {
            hit = Physics2D.Raycast(_transform.position, dir, dist, 1 << layer);
        }
        else
        {
            hit = Physics2D.Raycast(_transform.position, dir, 1 << layer);
        }

        return hit.transform == null ? null : hit.transform.gameObject;
    }

    private void TryMoveStepUp()
    {
        if (!IsCoolingDown() && !IsInBooth && !_isPunching)
        {
            MoveStepUp();
        }
    }

    private bool IsCoolingDown()
    {
        return _timeOfLastAction > 0f && Time.time - _timeOfLastAction < CooldownInSec;
    }

	void OnDestroy(){

		var recognizer = GetComponent<GestureRecognizer>();
		recognizer.OnSwipe -= HandleSwipe;

		GameScene.Instance.GameEnded = true;
		UI_GameOver.Instance.ShowScreen();
        OnAction = null;
        GetComponent<ExplodeObject>().SpawnParticles();
	}
 
}