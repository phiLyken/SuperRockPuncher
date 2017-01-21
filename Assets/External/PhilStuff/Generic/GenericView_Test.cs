using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GenericView_Test <T> : MonoBehaviour {


    public T Observed;
      GenericView<T> View;

    protected abstract GenericView<T> GetView();
    void Start()
    {
        GetView().Set(Observed);
    }

}
