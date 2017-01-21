using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ObjectiveCondition_ClickUI : ObjectiveCondition {

   public string TAG;
  
   public override void Init(Func<bool> can_complete)  
    {
        base.Init(can_complete);
        GetButton().onClick.AddListener(OnClick);
       
    }

    void OnClick()
    {         
        Complete();
    }

    Button GetButton()
    {
       GameObject go = GameObject.FindGameObjectWithTag(TAG);
        if (go != null)
        {
            return go.GetComponent<Button>();
        }
        else
            return null;
    }
    void OnDestroy()
    {
        if(GetButton() != null)
            GetButton().onClick.RemoveListener(OnClick);
    }
}
