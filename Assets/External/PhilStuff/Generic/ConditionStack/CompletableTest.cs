using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CompletableTest : MonoBehaviour {

    public List<Completable> Completables;

   
    CompletionStack<Completable> Stack;

    void Start()
    {
        Stack = new CompletionStack<Completable>(new List<Completable>(Completables),true);
        Stack.OnCancel += c => Debug.Log("cancelled "+c);
        Stack.OnComplete += c => Debug.Log("complete " +c+ "   "+ c.press_to_complete);
        Stack.OnCurrentComplete += c => Debug.Log("current complete " + c);
        Stack.OnSetNew += c => Debug.Log("set new " + c + "   "+ c.press_to_complete);
        Stack.Init();

       
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Stack.SetNewCurrent(Completables.GetRandom());
        }
    }
 
   
}

/*
-------------
        Dictionary<T, Action> cancel_callbacks;

 -----
       Action on_complete = null;
       oncomplete += foomap(object)
       on_complete = () =>
       {
           onComplete(object);

       };
       complete_callbacks.Add(object, on_complete);
       Current.OnComplete += on_complete;
 ------
       cancelled.OnComplete -= complete_callbacks[object];
       complete_callbacks.Remove(object);



   */
