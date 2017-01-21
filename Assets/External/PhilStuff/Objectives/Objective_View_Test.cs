using UnityEngine;
using System.Collections;
using System;

public class Objective_View_Test : GenericView_Test<Objective>
{
    public Objective_View view;

    protected override GenericView<Objective> GetView()
    {
        return view;
    }
}
