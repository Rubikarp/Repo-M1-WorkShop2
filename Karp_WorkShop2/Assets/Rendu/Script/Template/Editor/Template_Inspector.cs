using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEditor;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(Template))]
public class Template_Inspector : Editor
{
    SerializedProperty prop;


    // Start like
    private void OnEnable()
    {
        //Link variables to property
        //prop = serializedObject.FindProperty(nameof(Script.prop));

    }

    // Update like
    public override void OnInspectorGUI()
    {
        #region Scope
        //Button
        if (GUILayout.Button(new GUIContent("Voici un button","Tips"))) { }

        #region EditorGUI
        //Change Check
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (check.changed)
            {
            }
        }
        
        //Disable
        using (new EditorGUI.DisabledScope(false))
        {
        }
        
        //Disable Groupe
        bool variable = false;
        variable = EditorGUILayout.Toggle("CanVar", variable);
        using (var check = new EditorGUI.DisabledGroupScope(variable)) { }
        
        // Indent block
        using (new EditorGUI.IndentLevelScope()) { }

        // Scroll view
        Vector2 scrollPos = Vector2.zero;
        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUILayout.Height(100), GUILayout.Width(300)))
        {
            scrollPos = scrollView.scrollPosition;
            GUILayout.Label("This is a string inside a Scroll view! This is a string inside a Scroll view! This is a string inside a Scroll view!This is a string inside a Scroll view! This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!");
        }

        // AreaScope
        Rect zone = new Rect(0, 200, 100, 100);
        //Rect zone = EditorGUILayout.GetControlRect();
        EditorGUI.DrawRect(zone, new Color(1, 0, 0, 0.5f));
        using (var scrollView = new GUILayout.AreaScope(zone))
        {
            GUILayout.Label("This is a string inside a Scroll view! ");
        }

        #endregion

        #region EditorGUILayout
        //Vertical
        using (new EditorGUILayout.VerticalScope()) { }
        //Horizontal
        using (new EditorGUILayout.HorizontalScope()) { }
        //Toggle
        bool GroupEnabled = true;
        bool[] bools = new bool[3] { true, true, true };
        using (var posGroup = new EditorGUILayout.ToggleGroupScope("Toggle Label", GroupEnabled))
        {
            GroupEnabled = posGroup.enabled;
            bools[0] = EditorGUILayout.Toggle("a", bools[0]);
            bools[1] = EditorGUILayout.Toggle("b", bools[1]);
            bools[2] = EditorGUILayout.Toggle("c", bools[2]);
        }
        #endregion

        #endregion

        //Space
        GUILayout.Space(15);

        #region Shlag
        
        // Les fields
        ///https://docs.unity3d.com/ScriptReference/EditorGUILayout.html
        //Object Tricks
        ///script.someRef = EditorGUILayout.ObjectField("someTransform", script.someRef, typeof(Transform), true) as Transform;
        
        //Save
        EditorUtility.SetDirty(target);
        #endregion

    }
}
