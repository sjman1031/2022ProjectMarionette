using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkinMaster)), CanEditMultipleObjects]
public class skinMaster_editor : Editor {

    public override void OnInspectorGUI()
    {
        SkinMaster my_target = (SkinMaster)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "skinMaster");

        if (my_target.currentUISkin == null)
            GUI.color = Color.red;
        else
            GUI.color = Color.white;

        my_target.currentUISkin = EditorGUILayout.ObjectField("UI Skin", my_target.currentUISkin, typeof(UI_Skin), false) as UI_Skin;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("If you had change the -UI Skin- and you whant to see the difference: ");
        if (GUILayout.Button("Reskin current active gameobjects (for debug)"))
            my_target.ReskinAll();
        EditorGUILayout.LabelField("(complete reskin instead will happen automatically when the UI is open for the first time in play mode)");


        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(my_target);
        }
    }


    

}
