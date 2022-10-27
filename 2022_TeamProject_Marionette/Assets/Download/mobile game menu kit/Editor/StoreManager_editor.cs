using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoreManager))]
public class StoreManager_editor : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnInspectorGUI()
    {
        StoreManager my_target = (StoreManager)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "StoreManager");


        //store
        //my_target.editor_show_store = EditorGUILayout.Foldout(my_target.editor_show_store, "Store");
        //if (my_target.editor_show_store)
        //{
            EditorGUI.indentLevel++;
            my_target.store_enabled = EditorGUILayout.Toggle("store enabled", my_target.store_enabled);
            if (my_target.store_enabled)
            {
                EditorGUI.indentLevel++;
                my_target.start_virtual_money = EditorGUILayout.IntField("start virtual money", my_target.start_virtual_money);
                my_target.virtual_money_cap = EditorGUILayout.IntField("virtual money cap", my_target.virtual_money_cap);
                my_target.virtual_money_name = EditorGUILayout.TextField("virtual money name", my_target.virtual_money_name);
                my_target.can_buy_virtual_money_with_real_money = EditorGUILayout.Toggle("can buy virtual money with real money", my_target.can_buy_virtual_money_with_real_money);


                my_target.show_purchase_feedback = EditorGUILayout.Toggle("show purchase feedback", my_target.show_purchase_feedback);

                EditorGUILayout.LabelField("show buttons even if its cap is reached? Yes for:");
                EditorGUI.indentLevel++;
                my_target.show_lives_even_if_cap_reached = EditorGUILayout.Toggle("lives", my_target.show_lives_even_if_cap_reached);
                my_target.show_continue_tokens_even_if_cap_reached = EditorGUILayout.Toggle("continue tokens", my_target.show_continue_tokens_even_if_cap_reached);
                my_target.show_consumable_item_even_if_cap_reached = EditorGUILayout.Toggle("consumable items", my_target.show_consumable_item_even_if_cap_reached);
                my_target.show_incremental_item_even_if_cap_reached = EditorGUILayout.Toggle("incremental items", my_target.show_incremental_item_even_if_cap_reached);
                my_target.show_virtual_money_even_if_cap_reached = EditorGUILayout.Toggle("virtual money", my_target.show_virtual_money_even_if_cap_reached);
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        //}

        serializedObject.Update();
        EditorGUIUtility.LookLikeInspector();
        SerializedProperty tps = serializedObject.FindProperty("storeContainers");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tps, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        EditorGUIUtility.LookLikeControls();


        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(my_target);
        }
    }


}
