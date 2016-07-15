using DG.Tweening;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float MoveAmount = 10f;
    public float MoveSpeed = 10f;
    public Ease Ease;

	// Use this for initialization
	void Start ()
	{
	    transform.DOMoveY(transform.position.y + MoveAmount, MoveSpeed)
	        .SetEase(Ease)
	        .SetLoops(-1, LoopType.Yoyo);
	}
}
