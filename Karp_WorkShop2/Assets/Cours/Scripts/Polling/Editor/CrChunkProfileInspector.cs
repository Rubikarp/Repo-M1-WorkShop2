using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CrChunkProfile))]
public class CrChunkProfileInspector : Editor
{
    SerializedProperty width,height;
    SerializedProperty grid;

    SerializedProperty tileColors, currentType;

    // Start like
    private void OnEnable()
    {
        //Link variables to property
        width = serializedObject.FindProperty(nameof(CrChunkProfile.width));
        height = serializedObject.FindProperty(nameof(CrChunkProfile.height));
        grid = serializedObject.FindProperty(nameof(CrChunkProfile.grid));

        tileColors = serializedObject.FindProperty(nameof(CrChunkProfile.tileColor));
        currentType = serializedObject.FindProperty(nameof(CrChunkProfile.currentType));

    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();


        if (GUILayout.Button("Open Editor Window", EditorStyles.miniButton))
        {
            OpenWindow();
        }


        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

    private void OpenWindow()
    {
        //Dock to Inspector
        CourChunckProfileEditorWindow myWindow;

        if (!EditorWindow.HasOpenInstances<CourChunckProfileEditorWindow>())
        {
            Type inspectorType = Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");
             myWindow = EditorWindow.CreateWindow<CourChunckProfileEditorWindow>("Chunk Editor Window", new Type[] { inspectorType });
        }
        else
        {
            myWindow = EditorWindow.GetWindow(typeof(CourChunckProfileEditorWindow)) as CourChunckProfileEditorWindow;
        }

        myWindow.InitWindow(target as CrChunkProfile);
        myWindow.Show();
    }

}
