using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_WorldPos))]
public class UI_WorldPosEditor : Editor
{
    UI_WorldPos m_UI_WorldPos;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_UI_WorldPos = (target as UI_WorldPos);


        if (GUILayout.Button("Set to World Pos"))
            m_UI_WorldPos.UpdatePos();
    }

    void OnEnable()
    {

    }
}


