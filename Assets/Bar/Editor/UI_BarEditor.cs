using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_Bar))]
public class UI_BarEditor : Editor
{
    UI_Bar m_UI_Bar;
    float m_progress;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_UI_Bar = (target as UI_Bar);
        float old = m_progress;
        EditorGUILayout.MinMaxSlider(ref m_progress,ref m_progress, 0, 1);
        if (old != m_progress)
        {
            m_UI_Bar.SetProgress(m_progress);
        }

       
    }
}