using System;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{
    public enum SwipeDirection
    {
        Up,
        Left,
        Right
    }

    public Action<SwipeDirection> OnSwipe { get; set; }

    public void Awake()
    {
        var recognizer = new TKSwipeRecognizer();
        recognizer.gestureRecognizedEvent += r =>
        {
            Debug.Log(r.completedSwipeDirection);

            if (OnSwipe == null) return;

            if ((r.completedSwipeDirection & TKSwipeDirection.LeftSide) != 0)
            {
                OnSwipe(SwipeDirection.Left);
            }
            else if ((r.completedSwipeDirection & TKSwipeDirection.RightSide) != 0)
            {
                OnSwipe(SwipeDirection.Right);
            }
            else if ((r.completedSwipeDirection & TKSwipeDirection.TopSide) != 0)
            {
                OnSwipe(SwipeDirection.Up);
            }
        };
        TouchKit.addGestureRecognizer(recognizer);
    }
}