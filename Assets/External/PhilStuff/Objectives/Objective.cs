using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Objective : MonoBehaviour, ICompletable {

    public bool InitOnStart;
    public ObjectiveConfig Config;        
    public event Action OnCancel;
    public event Action OnComplete;

    public List<ObjectiveConfig> OtherObjectivesToComplete;

    ObjectiveCondition Condition;  
     
    List<ObjectiveSetup> Setups;
 

    public bool GetComplete()
    {
        return Condition.GetComplete();
    }

 
    public Objective Init (ObjectiveConfig config, Func<Objective, bool> canComplete)
    { 
        Config = config;       

        Condition = Instantiate(config.Condition).GetComponent<ObjectiveCondition>();
        Condition.Init( delegate
            {
                return canComplete(this);
            }
        );
        
        Condition.OnComplete += Complete;
        Condition.OnCancel += Cancel;
 
        return this;
    }

    public void SpawnSetups()
    {
        if (CanComplete(this) && Setups ==null && !Config.Setup.IsNullOrEmpty())
        {
          
            Debug.Log("SPAWN SETUP");
            Setups = M_Math.SpawnFromList(Config.Setup);
            Setups.ForEach(setup => setup.Init());
        }
    }
    void Cancel()
    {
        OnCancel.AttemptCall();
        if(Setups!= null)
        {
            Setups.ForEach(setup => setup.Remove());
        }
    }

    void Complete()
    {
        OnComplete.AttemptCall();
        if (Setups != null)
        {
            Setups.ForEach(setup => setup.Remove());
        }
    }

    public void Reset()
    {
        Condition.Reset();
    }

    void Start()
    {
        if (InitOnStart)
        {
            Init(Config, CanComplete);
        }
    }

    bool CanComplete(Objective obj){
        return Condition.CanComplete();
    }



    public bool GetShouldSave()
    {
        return Condition.GetShouldSave();
    }

    public string GetSaveID()
    {
        return Condition.GetSaveID();
    }

    public void SaveCompleted(bool b)
    {
        Condition.SaveCompleted(b);
    }

 

    public bool GetHasCompletedInSave()
    {
        return Condition.GetHasCompletedInSave();
    }
}


