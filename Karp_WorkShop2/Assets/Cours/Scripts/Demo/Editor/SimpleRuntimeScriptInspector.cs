using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class SimpleRuntimeScriptInspector : Editor
{
    //tous ce que unity sait seriralizer de base
    //https://docs.unity3d.com/ScriptReference/SerializedProperty.html
    ///  hasMultipleDifferentValue, Does this property represent multiple different values due to multi-object editing? (Read Only)

    SerializedProperty myColor , secondColor;
    SerializedProperty myColors;
    SerializedProperty myFloat;
    SerializedProperty myCurve;
    SerializedProperty myRef;
    SerializedProperty myStrcut;

    SimpleRuntimeScript script;

    // Start like
    private void OnEnable()
    {
        //Link variables to property
        myColor = serializedObject.FindProperty(nameof(script.someColor));
        myColors = serializedObject.FindProperty(nameof(script.someColors));
        myFloat = serializedObject.FindProperty(nameof(script.someFloat));
        myCurve = serializedObject.FindProperty(nameof(script.someCurve));
        myRef = serializedObject.FindProperty(nameof(script.someRef));
        myStrcut = serializedObject.FindProperty(nameof(script.someStruc));
    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();

        EditorGUILayout.PropertyField(myColor);
        //myColor.name;
        //myColor.displayName;

        EditorGUILayout.PropertyField(myFloat);
        //Floor
        if (myFloat.floatValue < 0) myFloat.floatValue = 0;
        EditorGUILayout.PropertyField(myCurve);
        EditorGUILayout.PropertyField(myRef);
        EditorGUILayout.LabelField(myCurve.type);

        EditorGUILayout.PropertyField(myColors);

        #region Array manipulation
        if (myColors.arraySize < 2) myColors.arraySize = 2;
        secondColor = myColors.GetArrayElementAtIndex(1);
        EditorGUILayout.PropertyField(secondColor);

        if (GUILayout.Button("Tweak Array"))
        {
            //myColors.InsertArrayElementAtIndex(0);
            myColors.MoveArrayElement(0,2);
            //myColors.DeleteArrayElementAtIndex(0);

            /// Avant 
            /// DeleteArrayElementAtIndex devait être appel 2 fois
            /// 1 pour null la rèf et la 2 pour delete array element
        }
        #endregion

        #region Struct manipulation
        EditorGUILayout.PropertyField(myStrcut);

        SerializedProperty subPp = myStrcut.FindPropertyRelative("aFloat");
        EditorGUILayout.PropertyField(subPp);

        //Warning deth jusqu'a 7 
        //https://forum.unity.com/threads/serialization-depth-limit-7-exceeded.460291/
        //https://blog.unity.com/technology/serialization-in-unity
        #endregion

        //Check for pointer 
        //EditorGUILayout.LabelField(myRef.type);
        if (myRef.type.Contains("Transform")) { }

        //Exemple de get in ref (Transform est chiant)
        if (GUILayout.Button("Do stuff on Name"))
        {
            //(myRef.objectReferenceValue as Transform).Translate(Vector3.forward * 10);

            SerializedObject so = new SerializedObject(myRef.objectReferenceValue);
            SerializedProperty sp = so.FindProperty("m_LocalPosition");
            #region Debug Process :
            // recup iterator
            SerializedProperty it = so.GetIterator();
            // broxer toutes le prop enfants bool go to child
            while (it.Next(true))
            {
                // afficher leur nom
                // Debug.Log(it.name);
            }
            #endregion

            so.Update();
            sp.vector3Value += Vector3.forward *10;
            so.ApplyModifiedProperties();
        }

        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
        //serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}
