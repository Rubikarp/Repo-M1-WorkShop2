using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomEditor(typeof(SimpleRuntimeScript)), */[CanEditMultipleObjects]
public class SimpleRuntimeScriptSlagInspector : Editor
{
    bool once = false;

    private Color color = Color.white;
    private SimpleRuntimeScript linkScript;
    private SimpleRuntimeScript[] linkScripts;

    Vector2 scrollPos;
    GUIStyle myStyle = new GUIStyle();

    // Start like
    private void OnEnable()
    {
        //Recup linkedScript
        linkScript = target as SimpleRuntimeScript;

        //MultiEdit à la mano
        linkScripts = new SimpleRuntimeScript[targets.Length];
        linkScripts = targets as SimpleRuntimeScript[];

        //
        color = linkScript.someColor;
    }

    // Update like
    public override void OnInspectorGUI()
    {
        if(!once)
        {
            once = true;

            //Style
            ///https://docs.unity3d.com/ScriptReference/GUIStyle.html
            //Copy a unity default style
            myStyle = new GUIStyle(EditorStyles.boldLabel); 
            //MAIS ne fonctionne pas sur OnAble()
            //myStyle.fontStyle = FontStyle.Italic;
            //myStyle.alignment = TextAnchor.MiddleRight;
            //myStyle.normal.textColor = Color.white;
            //myStyle.onHover.textColor = Color.blue;
        }

        ///La version de base
        //base.OnInspectorGUI();
        //DrawDefaultInspector();

        #region La guerre de classes GUI
        //GUI;
        //GUILayout;
        //EditorGUI;
        //EditorGUILayout;

        /* Layout class
         * place automatiquement les elements
        */
        /* Editor class
        * contient tout ce qui n'est pas runtime
        * GUI classique est déprécier car avant on l'utiliser pour draw UI
        * exeption Buuton qui est dans GUILayout
        */

        #endregion

        #region L'enfer des Fields
        // Les fields
        //https://docs.unity3d.com/ScriptReference/EditorGUILayout.html
        //EditorGUILayout.ColorField();
        //EditorGUILayout.FloatField();
        //EditorGUILayout.CurveField();
        //EditorGUILayout.ObjectField();
        #endregion

        EditorGUILayout.LabelField("Simili Header", myStyle);

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            float value = EditorGUILayout.FloatField(new GUIContent("Color", "tips : c'est une couleur"), linkScript.someFloat);
            
            if (check.changed)
            {
                Undo.RecordObject(linkScript, "tweak float");
                linkScript.someFloat = value;

                if (linkScript.someFloat < 0) linkScript.someFloat = 0;
                if (linkScript.someFloat > 1) EditorGUILayout.LabelField("someFloat is less than " + 1, myStyle); ;
            }
        }

        using (new EditorGUI.DisabledGroupScope(linkScript.someFloat >1))
        {
            linkScript.someColor = EditorGUILayout.ColorField("edit if someFloat < 1", linkScript.someColor);
            //ou
            //color = EditorGUILayout.ColorField("Some Color", color);
            //linkScript.someColor = color;
        }

        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUILayout.Height(100), GUILayout.Width(300)))
        {
            scrollPos = scrollView.scrollPosition;
            GUILayout.Label("This is a string inside a Scroll view! This is a string inside a Scroll view! This is a string inside a Scroll view!This is a string inside a Scroll view! This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!\n This is a string inside a Scroll view!");
        }

        #region Area de merde
        Rect zone = new Rect(0, 200, 100, 100);
        //Rect zone = EditorGUILayout.GetControlRect();
        EditorGUI.DrawRect(zone, new Color(1,0,0,0.5f));
        using (var scrollView = new GUILayout.AreaScope(zone))
        {
            GUILayout.Label("This is a string inside a Scroll view! ");
        }
        #endregion

        //Object Tricks
        linkScript.someRef = EditorGUILayout.ObjectField("someTransform", linkScript.someRef, typeof(Transform), true) as Transform;

        //in pixel
        GUILayout.Space(15);

        #region Groupe
        /*
        using (new GUILayout.HorizontalScope()) { }
        using (new GUILayout.VerticalScope()) { }
        https://docs.unity3d.com/ScriptReference/EditorGUI.ChangeCheckScope.html

        GUILayout.BeginVertical();
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
        EditorGUI.BeginChangeCheck();
        EditorGUI.EndChangeCheck();
        */
        #endregion

        // ligne = label + field
        //EditorGUIUtility.labelWidth;

        //Save
        EditorUtility.SetDirty(target);

        if (GUILayout.Button("Voici un button"))
        {
            Debug.Log("bam! t'as appuyer sur un button");
        }

    }
}
