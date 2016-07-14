using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0.001f, 0.5f)]
    public float StepLength = 0.1f;
    [Range(0.001f, 0.5f)]
    public float CooldownInSec = 0.1f;
    [Range(0.5f, 5f)]
    public float PunchRange = 1f;

    public Transform Lane;

    private Transform _transform;
    private float _timeOfLastAction;

    private static readonly int BoothLayer = LayerMask.NameToLayer("Booth");
    private static readonly int ObstacleLayer = LayerMask.NameToLayer("Obstacle");
    private bool _isInBooth;

    public void Awake()
    {
        _transform = GetComponent<Transform>();

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
        _transform.Translate(Vector2.up * StepLength);
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
        var obstacle = GetObjectInDirectionWithinDistance(Vector2.up, ObstacleLayer, PunchRange);

        if (obstacle != null)
        {
            Destroy(obstacle);
        }
    }

    private void TryToMoveInOrOutOfBooth(Vector2 dir)
    {
        if (_isInBooth)
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
            _transform.position = new Vector2(Lane.position.x, _transform.position.y);
            _isInBooth = false;
            _timeOfLastAction = Time.time;
        }
    }

    private void TryToMoveIntoBooth(Vector2 dir)
    {
        var booth = GetObjectInDirectionWithinDistance(dir, BoothLayer);
        if (booth == null) return;

        _transform.position = booth.transform.position;
        _isInBooth = true;
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
        if (!IsCoolingDown() && !_isInBooth)
        {
            MoveStepUp();
        }
    }

    private bool IsCoolingDown()
    {
        return _timeOfLastAction > 0f && Time.time - _timeOfLastAction < CooldownInSec;
    }
}