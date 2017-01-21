using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ObjectScaler : MonoBehaviour {

    public Vector2 ScaleAmount;
    public float ScaleSpeed = 10f;

    public Ease Ease;
 
    // Use this for initialization
    void Start()
    {
      
            (transform as RectTransform).DOSizeDelta( (transform as RectTransform).sizeDelta + ScaleAmount, ScaleSpeed)
                .SetEase(Ease)
                .SetLoops(-1, LoopType.Yoyo);
     
 
    }
}
