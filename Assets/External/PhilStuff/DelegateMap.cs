using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class AnonCallBacks<T>  {


    Action<T> call;
    Dictionary<T, Action> calls;
    
    public AnonCallBacks(Action<T> _call)
    {

        calls = new Dictionary<T, Action>();
        call = _call;
       
    }

    public Action Add(  T _object)
    {
  
        Action new_del = null;
        new_del = () =>
        {
            call(_object);
        };


        calls.AddOrUpdate(_object, new_del);
        return new_del;
       
    }


    public Action Get(T t, bool remove)
    {
        if (remove)
        {
           return  calls.GetAndRemove(t);
        } else
        {
            return calls[t];
        }  
        
             
    }
    class M_MAPITEM<I, V>{

        public I Item;
        public V Value;

        public M_MAPITEM(I item, V value)
        {
            Item = item;
            Value = value;
        }
    }
}
