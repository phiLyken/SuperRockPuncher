using UnityEngine;
using System.Collections;
using System;

public class ObjectiveSetup_SpawnRocks : ObjectiveSetup
{

    static float LastRockTime;
    // Use this for initialization
    public override ObjectiveSetup Init()
    {
        StartCoroutine(SpawnRock());
        return this;
    }

    IEnumerator SpawnRock()
    {
        while (true)
        {
            float waitfor = 4 - (Time.time - LastRockTime);
            yield return new WaitForSeconds(waitfor);
            GameObject.Find("TopBlocker").GetComponent<ObstacleSpawner>().SpawnObstacle();
            LastRockTime = Time.time;
          
        }
    }
    public override void Remove()
    {
        StopAllCoroutines();
    }
}
