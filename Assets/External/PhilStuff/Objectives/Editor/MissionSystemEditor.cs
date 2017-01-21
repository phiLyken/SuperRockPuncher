using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MissionSystem))]
public class MissionSystemEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (GUILayout.Button("RESET OBJECTIVES"))
        {
            (target as MissionSystem).ResetSaves();
        }
    }
}
