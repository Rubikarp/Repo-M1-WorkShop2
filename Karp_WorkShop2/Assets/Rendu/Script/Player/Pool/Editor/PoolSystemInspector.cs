using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Rendu;

//[CustomEditor(typeof(PoolSystem))]
public class PoolSystemInspector : Editor
{
    SerializedProperty self;
    SerializedProperty chunks;
    SerializedProperty chunkPrefab;


    // Start like
    private void OnEnable()
    {
        //Link variables to property
        self = serializedObject.FindProperty(nameof(PoolSystem.self));

    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();

        EditorGUILayout.LabelField("Pool", EditorStyles.boldLabel);

        using (new GUILayout.HorizontalScope())
        {

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (check.changed)
                {
                }
            }

            if (GUILayout.Button("Rebuild The Pool", EditorStyles.miniButton))
            {
            }
        }

        bool PoolSystem = false;
        PoolSystem = EditorGUILayout.Foldout(PoolSystem, "Reference", true);
        if (PoolSystem)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(self);
            EditorGUILayout.PropertyField(chunkPrefab);

            EditorGUI.indentLevel--;
        }


        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

}
