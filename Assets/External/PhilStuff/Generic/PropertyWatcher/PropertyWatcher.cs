using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class  PropertyWatcher<T>  {

   protected Func<T> GetValue;
   protected T _lastValue;
   Action<T> callback;


 
    public void Init(Func<T> getvalue, Action<T> callback)
    {
        GetValue = getvalue;
        this.callback = callback;
        GlobalUpdateDispatcher.OnUpdate += OnUpdate;
        
    }
    
 
    void OnUpdate(float dt)
    {
        if (ValueChanged())
        {

            _lastValue = GetValue();
            callback(_lastValue);
            
        }
    }

    protected virtual bool ValueChanged()
    {
        return !EqualityComparer<T>.Default.Equals(_lastValue, GetValue());
    }
  
}

public class PropertyWatcher_Vector : PropertyWatcher<Vector3>
{ 

    protected override bool ValueChanged()
    {       
        return GetValue() != _lastValue;
    }

}

public class PropertyWatcher_Int : PropertyWatcher<int>
{

    protected override bool ValueChanged()
    {
        return GetValue() != _lastValue;
    }

}


public class PropertyWatcher_Float : PropertyWatcher<float>
{

    protected override bool ValueChanged()
    {
        return GetValue() != _lastValue;
    }

}


public class PropertyWatcher_Transform : PropertyWatcher<Transform>
{
    Vector3 lastpos;
    Quaternion lastrot;
    Vector3 lastsize;

    protected override bool ValueChanged()
    {
        Transform tr = GetValue();
        if (tr == null || _lastValue == null) return true;

        //Debug.Log("same?");
        if(lastpos != tr.position || lastsize != tr.localScale || lastrot != tr.rotation)
        {
            lastpos = tr.position;
            lastrot = tr.rotation;
            lastsize = tr.localScale;
            return true;
        }

        return false;
    }

}

 