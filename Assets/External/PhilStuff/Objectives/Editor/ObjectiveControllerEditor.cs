using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectiveController))]
public class ObjectiveControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("RESET OBJECTIVES"))
        {
            (target as ObjectiveController).ResetSaves();
        }
    }
}
