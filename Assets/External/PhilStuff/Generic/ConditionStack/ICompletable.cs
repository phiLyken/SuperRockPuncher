using UnityEngine;
using System.Collections;
using System;

public interface ICompletable  {

    event Action OnComplete;
    event Action OnCancel;

    bool GetShouldSave();
    bool GetHasCompletedInSave();
    string GetSaveID();
    void SaveCompleted(bool b);
    
    bool GetComplete();
    void Reset();
}

