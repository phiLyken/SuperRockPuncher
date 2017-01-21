using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ObjectiveController : MonoBehaviour {

   
    public bool InitOnStart;
    public bool CompleteAny;

    public List<ObjectiveConfig> Configs;

    protected CompletionStack<Objective> Objectives;
    public event Action<Objective> OnComplete;
    public event Action<Objective> OnNext;

    void Start()
    {
        if(InitOnStart)
             Init(Configs);
    }

    public void Init()
    {
        Init(Configs);
    }

    public void ResetSaves()
    {
        Configs.ForEach(conf => conf.Condition.SaveCompleted(false));
        
       
    }
    public void Init(List<ObjectiveConfig> _objectives)
    {
 

        List<Objective> objectives = new List<Objective>();
        GameObject newGO = new GameObject("objective ");
        newGO.transform.parent = this.transform;

        _objectives
            .Where(obj => !obj.Condition.GetHasCompletedInSave()).ToList()
            .ForEach(obj => {
                Objective objective = newGO.AddComponent<Objective>();
                objectives.Add(objective);
                objective.Init(obj, CanComplete);
        });

      

        Objectives = new CompletionStack<Objective>(objectives, CompleteAny);

        if (CompleteAny)
        {
            Objectives.OnComplete += OnObjectiveComplete;
        }
        else
        {
            Objectives.OnCurrentComplete += OnObjectiveComplete;
        }

        Objectives.OnSetNew += OnSetNew;
        Objectives.Init();

        objectives.ForEach(objective => objective.SpawnSetups());
    }



    void OnObjectiveComplete(Objective obj)
    {
        Debug.Log("Objective complete " + obj.Config.Title);
        OnComplete.AttemptCall(obj);
    }

    void OnSetNew(Objective obj)
    {
        Debug.Log("Objective new " + obj.Config.Title +" "+obj.Config.Condition.name);
        obj.SpawnSetups();
        OnNext.AttemptCall(obj);
    }

    public List<Objective> GetObjectives()
    {
        return Objectives.GetItems();
    }

    public bool CanComplete(Objective obj)
    {
        return Objectives.CanComplete(obj);
    }

    public virtual bool HasCompleted(string missionID)
    {

        foreach(var objective in Objectives.GetItems())
        {
            if(objective.Config.ID == missionID) 
            {
                return objective.GetHasCompletedInSave();
            }
        }

        return true;
    }
}
