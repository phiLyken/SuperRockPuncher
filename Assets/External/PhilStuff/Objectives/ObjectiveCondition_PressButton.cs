using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition_PressButton : ObjectiveCondition
{
    public  KeyCode key;


    public override void Init(Func<bool> canComplete)
    {

        base.Init(canComplete);
        Debug.Log("Condition Pressbutton");
       
       // key = config.key;
        
    }

    void Update()
    {
        if (Input.GetKey(key))
        {
            Complete();
          
        }  
    }
}
