using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalUpdateDispatcher : MonoBehaviour {
    static GlobalUpdateDispatcher instance;
    public Action<float> on_update;


    public static event Action<float> OnUpdate{        
        add
        {           
                GetInstance().on_update += value;        
        }

        remove
        { 
                GetInstance().on_update -= value;
        
        }
    }        
    

    static GlobalUpdateDispatcher GetInstance()
    {
        if(instance == null)
        {         
             instance = M_Extensions.MakeNew<GlobalUpdateDispatcher>("_GlobalUpdate");            
        }

        return instance;
    }
    

    void Update()
    {
        if (on_update != null)
        {
            on_update(Time.deltaTime);
        }
    }
}
