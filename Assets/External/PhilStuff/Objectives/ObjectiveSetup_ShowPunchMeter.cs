using UnityEngine;
using System.Collections;
using System;

public class ObjectiveSetup_ShowPunchMeter : ObjectiveSetup
{

    public override ObjectiveSetup Init()
    {
        GameObject.FindObjectOfType<PunchMeterUI>().bar.gameObject.SetActive(true);

        return this;
    }

    public override void Remove()
    {
       
    }

}
