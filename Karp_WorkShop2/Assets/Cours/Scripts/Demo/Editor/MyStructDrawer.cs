using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(myStruct))]

public class MyStructDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float nbrLine = 2;
        float lineHeight = EditorGUIUtility.singleLineHeight + 1; //+1 pour par que ça se colle
        return nbrLine * lineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight +1;

        Rect topZone = new Rect(position.x, position.y, position.width, lineHeight-1);
        Rect botZone = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);
        //Debug Espace
        EditorGUI.DrawRect(position, new Color(0, 0, 1, 0.2f));
        EditorGUI.DrawRect(topZone, new Color(1, 1, 0, 0.2f));
        EditorGUI.DrawRect(botZone, new Color(1, 0, 1, 0.2f));

        SerializedProperty flout = property.FindPropertyRelative("aFloat");
        SerializedProperty colour = property.FindPropertyRelative("aColor");

        EditorGUI.PropertyField(topZone, flout, GUIContent.none);
        EditorGUI.PropertyField(botZone, colour, GUIContent.none);
    }
}
