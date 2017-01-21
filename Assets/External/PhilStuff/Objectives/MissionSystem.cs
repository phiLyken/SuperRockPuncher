using UnityEngine;
using System.Collections;
using System;

public class MissionSystem : ObjectiveController {

    void Awake()
    {
        if (_instance == null)
        {
            getInstance();
        }
    }

    public static event Action<Objective> OnNewMission
    {
        add
        {
            Instance.OnNext += value;
        }
        remove
        {
            Instance.OnNext -= value;
        }
    }

    public static event Action<Objective> OnCompleteMission
    {
        add
        {
            Instance.OnComplete += value;
           
        }
        remove
        {
            Instance.OnComplete -= value;
        }
    }

    static MissionSystem _instance;
      
    public static MissionSystem Instance
    {
       get
        {
            return _instance != null ? _instance : getInstance() ;
        }
    }

    static MissionSystem getInstance()
    {
        MissionSystem found =  GameObject.FindObjectOfType<MissionSystem>();
        if(found == null)
        {
           // Debug.LogWarning("NO MISSION SYSTEM INSTANCE FOUND");           
        } else if(_instance == null)
        {
            _instance = found;
             _instance.Init();
        }
        return _instance;
    }

    public static bool HasCompletedGlobal(string id)
    {
        return getInstance() == null || _instance.HasCompleted(id);
    }
 

}
