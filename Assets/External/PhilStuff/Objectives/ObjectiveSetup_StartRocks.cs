using UnityEngine;
using System.Collections;
using System;

public class ObjectiveSetup_StartRocks : ObjectiveSetup
{
    public override ObjectiveSetup Init()
    {
        return this;
    }


    public override void Remove()
    {
      
        FindObjectOfType<ObstacleSpawner>().SpawnEnabled = true;
    }
}
