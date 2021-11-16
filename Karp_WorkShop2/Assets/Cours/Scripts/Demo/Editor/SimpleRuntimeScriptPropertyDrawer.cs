using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SimpleRuntimeScript))]
public class SimpleRuntimeScriptPropertyDrawer : Editor
{
    SerializedProperty structProperty;

    // Start like
    private void OnEnable()
    {
        structProperty = serializedObject.FindProperty("someStruc");

    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();

        EditorGUILayout.PropertyField(structProperty);

        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();

    }
}
