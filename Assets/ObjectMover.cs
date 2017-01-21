using DG.Tweening;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float MoveAmount = 10f;
    public float MoveSpeed = 10f;
    public Ease Ease;
    public Directions Direction;
    public enum Directions
    {
    vertical,
        horizontal }
    
	// Use this for initialization
	void Start ()
	{
        if(Direction == Directions.vertical)
        {
	    transform.DOMoveY(transform.position.y + MoveAmount, MoveSpeed)
	        .SetEase(Ease)
	        .SetLoops(-1, LoopType.Yoyo);
        } else
        {
            transform.DOMoveX(transform.position.y + MoveAmount, MoveSpeed)
            .SetEase(Ease)
            .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
