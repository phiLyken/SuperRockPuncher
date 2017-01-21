using UnityEngine;
using System.Collections;
using System;

public class Completable : MonoBehaviour, ICompletable
{
    public KeyCode press_to_complete;
    bool completed = false;

    public string SaveID;
    public bool SaveCompletion;

    public event Action OnCancel;
    public event Action OnComplete;

    public bool GetComplete()
    {
        return completed;
    }

    public bool GetHasCompletedInSave()
    {
        return PlayerPrefs.HasKey(GetSaveID()) ? PlayerPrefs.GetInt(GetSaveID()) == 1 : false;
    }

    public bool GetShouldSave()
    {
        return SaveCompletion;
    }

    public string GetSaveID()
    {
        return SaveID;
    }

    public void Reset()
    {
        completed = false;
    }

    public void SaveCompleted(bool b)
    {
        PlayerPrefs.SetInt(GetSaveID(), b ? 1 : 0);
    }

    void Update()
    {
        if (GetComplete())
        {
            completed = true;
            OnComplete.AttemptCall();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            OnCancel.AttemptCall();
        }
    }
}