using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class ObjectiveController_View : GenericView<ObjectiveController> {

    public int ShowItemCount;
    ViewList<Objective, Objective_View> Objectives;

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    public override void Updated()
    { 
       Objectives.UpdateList(ListToShow());
    }

    List<Objective> ListToShow()
    {
        return new List<Objective>(m_Item.GetObjectives());
    }

    protected override void OnSet(ObjectiveController item)
    {
        Objectives = new ViewList<Objective, Objective_View>();
        
        Objectives.Init(MakeView, GetTarget,   ListToShow() , OnRemove, ShowItemCount);

        item.OnComplete += OnItemUpdated;      
    }

    void OnItemUpdated(Objective item)
    {
        Updated();
    }

    Objective_View MakeView(Objective item, Transform target)
    {
        Objective_View view = Resources.Load("objective_view").Instantiate(target, true).GetComponent<Objective_View>();
        view.Set(item);
        return view;
    }
   
    void OnRemove(Objective_View view)
    {
        view.transform.parent = GetComponentInParent<Canvas>().transform;
        view.Remove();
    }

 
    Transform GetTarget(Objective item)
    {
        return transform;
    }
}
