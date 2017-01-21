using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ObjectiveSetup_Booths : ObjectiveSetup {

    public GameObject BoothInRight;
    public GameObject BoothInLeft;

    List<GameObject> _objects;


    void SpawnLeft(GameObject booth)
    {
        if(booth.layer == LayerMask.NameToLayer("Booth"))
            Spawn(booth, BoothInLeft);
    }

    void SpawnRight(GameObject booth)
    {
        if (booth.layer == LayerMask.NameToLayer("Booth"))
            Spawn(booth,BoothInRight );
    }


    void Spawn(GameObject target, GameObject prefab)
    {
        GameObject obj = prefab.Instantiate(GameObject.FindGameObjectWithTag("TutorialUI").transform, true);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<UI_WorldPos>().SetWorldPosObject(target.transform,true);
        _objects.Add(obj);
    }

    public override ObjectiveSetup Init()
    {
        _objects = new List<GameObject>();
        GameObject.FindObjectOfType<LaneGenerator>().LeftElements.ForEach(l => SpawnLeft(l));
        GameObject.FindObjectOfType<LaneGenerator>().RightElements.ForEach(l => SpawnRight(l));
        GameObject.FindObjectOfType<LaneGenerator>().OnLeftSpawn += SpawnLeft;
        GameObject.FindObjectOfType<LaneGenerator>().OnRightSpawn += SpawnRight;
        return this;
    }

    public override void Remove()
    {
        _objects.ForEach(obj => Destroy(obj));
        GameObject.FindObjectOfType<LaneGenerator>().OnLeftSpawn -= SpawnLeft;
        GameObject.FindObjectOfType<LaneGenerator>().OnRightSpawn -= SpawnRight;

 
    }
}
