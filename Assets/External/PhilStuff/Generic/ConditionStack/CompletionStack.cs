/*
    
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CompletionStack  <T>  where T : ICompletable{

    public event Action<T> OnSetNew;
    /// <summary>
    /// only called if the current object was completed. Oncomplete will be called as well
    /// </summary>
    public event Action<T> OnCurrentComplete;

    /// <summary>
    /// called whenever ANY completable was completed
    /// </summary>
    public event Action<T> OnComplete;

    /// <summary>
    /// called whenever a completable was cancelled
    /// </summary>
    public event Action<T> OnCancel;

    List<T> Completables;
    T Current;

    AnonCallBacks<T> cancel_callbacks;
    AnonCallBacks<T> complete_callbacks;
    bool CanCompleteAny;

    public CompletionStack(List<T> completables, bool canCompleteAny  )
    {
        CanCompleteAny = canCompleteAny;
        cancel_callbacks = new AnonCallBacks<T>(onCancel);
        complete_callbacks = new AnonCallBacks<T>(onComplete);
        Completables = new List<T>();

        completables.ForEach(c => Add(c));

    }

    public void Init()
    {       
        SetCurrent(GetNext());
    }
  

    /// <summary>
    /// Get current active completable
    /// </summary>
    /// <returns></returns>
    public T GetCurrent()
    {
        return Completables.IsNullOrEmpty() ? default(T) : Completables[0];
    }

    /// <summary>
    /// Returns the completable after the current one, null if there are not completables, or there is just 1 completable (which is the current one)
    /// </summary>
    /// <returns></returns>
    public T GetNext()
    {
        if (Completables.IsNullOrEmpty())
            return default(T);


        //IF we dont have a current, the first one is returned, otherwise the next one
        if (ReferenceEquals(Current, default(T)))
        {
            return Completables[0];
        } else if(Completables.Count > 1)
        {
            return Completables[1];
        }

        Debug.LogWarning("No Next");
        return default(T);
    }

    void onComplete(T completable)
    {

        if (!CanComplete(completable))
        {
            return;
        }

        if ( ReferenceEquals(completable, Current)) 
        {
            SetCurrent(GetNext());
            Remove(completable);
            OnCurrentComplete.AttemptCall(completable);         

        } else 
        {
            Remove(completable);
        }

        OnComplete.AttemptCall(completable);

    }

    void onCancel(T cancelled)
    {
        OnCancel.AttemptCall(cancelled);

        if(ReferenceEquals(cancelled, Current))  
        {
            SetCurrent(GetNext());
        }       

        Remove(cancelled);
    }

    void SetCurrent(T completable)
    {
        if (EqualityComparer<T>.Default.Equals(completable, default(T)))
        {
            return;
        }
    
        Current = completable;  
        OnSetNew.AttemptCall(completable);
    }

    public void Remove(T completable)
    {       
        if (Completables.Contains(completable))
        {           
            completable.OnCancel -= cancel_callbacks.Get(completable, true);
            completable.OnComplete -= complete_callbacks.Get(completable, true);
            Completables.Remove(completable);
        }
    }

    public void Add(T completable)
    {
        Add(completable, Completables.Count);
    }
    public void Add(T completable, int index)
    {
       // Debug.Log(completable);
        completable.OnComplete += complete_callbacks.Add(completable);
        completable.OnCancel += cancel_callbacks.Add(completable);

        if (Completables.Contains(completable))
        {
            completable.Reset();
            Completables.Remove(completable);
          
        }

        Completables.Insert(index, completable);     
       
       
    }
    
    /// <summary>
    /// Sets a new completable on the top of the stack, will trigger onsetnew, and push back the current one +1
    /// </summary>
    /// <param name="completable"></param>
    public void SetNewCurrent(T completable)
    {
        Add(completable,0);
        SetCurrent(completable);
        
    }

    public bool CanComplete(T item)
    {
        
        if (!Completables.Contains(item))
        {
            return false;
        }

        if (!CanCompleteAny && !ReferenceEquals(item, Current))
        {
            Debug.LogWarning("Only top is completable");
            return false;
        }

        return true;
    }

    public int GetCount()
    {
        return Completables.IsNullOrEmpty() ? 0 : Completables.Count;
    }

    public void Reset()
    {
       
    }

   public List<T> GetItems()
    {
        return Completables;
    }
 
}
