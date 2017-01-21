using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ObjectiveSetup_SpawnUI : ObjectiveSetup
{
    public Text FinishTF;
    
    public override ObjectiveSetup Init()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("TutorialUI").transform, true);
        (transform as RectTransform).anchoredPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        return this;
    }

    public override void Remove()
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }
}
