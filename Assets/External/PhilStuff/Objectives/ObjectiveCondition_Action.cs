using UnityEngine;
using System.Collections;

public class ObjectiveCondition_Action : ObjectiveCondition {

    public bool CompleteOnDestroy;

    public int Count;
    public Player.PlayerActions Action;

    int count;
    void Start()
    {
        Player.OnAction += OnAction;

          }

    void OnAction(Player.PlayerActions aciton)
    {
        if(aciton == Action)
        {
            count++;
            if(count >= Count)
                Complete();
        }
    }

    void OnDestroy()
    {
        if (CompleteOnDestroy)
            Complete();
    }
}
