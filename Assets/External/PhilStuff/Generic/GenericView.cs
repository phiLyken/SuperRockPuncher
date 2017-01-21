using UnityEngine;
using System.Collections;
using System;

public abstract class GenericView<T> : MonoBehaviour
{
    protected T m_Item;


    public abstract void Updated();
    public abstract void Remove();

    protected abstract void OnSet(T item);
    public void Set(T item )
    {
        m_Item = item;
        OnSet(item);  
        Updated();
    }
    


 
}