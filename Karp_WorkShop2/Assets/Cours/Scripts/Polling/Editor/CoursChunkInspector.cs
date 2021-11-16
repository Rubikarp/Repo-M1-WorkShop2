using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CanEditMultipleObjects]
//[CustomEditor(typeof(CoursChunk))]
public class CoursChunkInspector : Editor
{
    SerializedProperty self;
    SerializedProperty scrollSpeed;
    SerializedProperty currenchunk;

    // Start like
    private void OnEnable()
    {
        //Link variables to property
        self = serializedObject.FindProperty(nameof(CoursChunk.self));
        scrollSpeed = serializedObject.FindProperty(nameof(CoursChunk.scrollSpeed));
        
    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();


        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

}
