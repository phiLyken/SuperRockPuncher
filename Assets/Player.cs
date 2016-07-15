﻿using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    private static readonly int BoothLayer = LayerMask.NameToLayer("Booth");
    private static readonly int ObstacleLayer = LayerMask.NameToLayer("Obstacle");
    private Animator _animator;
    private PunchMeter _punchMeter;

    public void Awake()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _punchMeter = GetComponent<PunchMeter>();

        var recognizer = GetComponent<GestureRecognizer>();
        recognizer.OnSwipe += HandleSwipe;
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
                TryToAttack();
                break;
            case GestureRecognizer.SwipeDirection.Left:
                TryToMoveInOrOutOfBooth(Vector2.left);
                break;
            case GestureRecognizer.SwipeDirection.Right:
                TryToMoveInOrOutOfBooth(Vector2.right);
                break;
        }
    }

    private void TryToAttack()
    {
        _animator.SetTrigger("Punch");
        var range = _punchMeter.GetRange();
        var obstacle = GetObjectInDirectionWithinDistance(Vector2.up, ObstacleLayer, range);

        if (obstacle != null)
        {
			ExplodeObject explode = obstacle.GetComponent<ExplodeObject>();
			if(explode != null) {
				explode.SpawnParticles();
			}

			PointCounter.Instance.ObstacleDestroyed();

            Destroy(obstacle);
        }

        _punchMeter.Punch();
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
            _transform.DOMove(new Vector2(Lane.position.x, _transform.position.y), StepTransitionTime * Mathf.Abs(dirToLane.x) * StepLength)
                .SetEase(StepEase);
            IsInBooth = false;
            _timeOfLastAction = Time.time;
        }
    }

    private void TryToMoveIntoBooth(Vector2 dir)
    {
        var booth = GetObjectInDirectionWithinDistance(dir, BoothLayer);
        if (booth == null) return;

        var dirToBooth = booth.transform.position - _transform.position;
        _transform.DOMove(booth.transform.position, StepTransitionTime * StepLength * dirToBooth.magnitude)
            .SetEase(StepEase);
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
        if (!IsCoolingDown() && !IsInBooth)
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
	}
}