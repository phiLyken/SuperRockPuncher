using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition : MonoBehaviour, ICompletable {


    public event Action OnCancel;
    public event Action OnComplete;
    public Func<bool> CanComplete;
    bool ConditionMet = false;

    public string SaveID;

    public bool shouldSave;

    public bool GetComplete()
    {
        return ConditionMet;
    } 

    protected  void Complete()
    {

        ConditionMet = CanComplete();

        if (ConditionMet)
        {
            SaveCompleted(true);
            OnComplete.AttemptCall();
            
        }

    }


    public void Reset()
    {
        ConditionMet = false;

    }

    public virtual void Init(Func<bool> canComplete)
    {
        
        CanComplete = canComplete;
    }
    



    public bool GetHasCompletedInSave()
    {
        return PlayerPrefs.HasKey(SaveID) ? PlayerPrefs.GetInt(SaveID) == 1 : false;
    }

    public string GetSaveID()
    {
        return SaveID;
    }




    public void SaveCompleted(bool b)
    {
        PlayerPrefs.SetInt(GetSaveID(), b ? 1 : 0);
    }

    public bool GetShouldSave()
    {
        return shouldSave;
    }
}
