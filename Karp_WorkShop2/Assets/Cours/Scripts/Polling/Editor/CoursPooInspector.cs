using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CoursPool))]
public class CoursPoolInspector : Editor
{
    SerializedProperty self;
    SerializedProperty chunks;
    SerializedProperty chunkPrefab;

    SerializedProperty foldout, poolSize;

    // Start like
    private void OnEnable()
    {
        //Link variables to property
        self = serializedObject.FindProperty(nameof(CoursPool.self));
        chunks = serializedObject.FindProperty(nameof(CoursPool.chunks));
        chunkPrefab = serializedObject.FindProperty(nameof(CoursPool.chunkPrefab));

        foldout = serializedObject.FindProperty(nameof(CoursPool.foldout));
        poolSize = serializedObject.FindProperty(nameof(CoursPool.poolSize));

        Undo.undoRedoPerformed += RebuildPool;
    }
    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RebuildPool;
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
                EditorGUILayout.PropertyField(poolSize);

                if (check.changed)
                {
                    //Floor to 0 
                    if (poolSize.intValue < 0) poolSize.intValue = 0;
                }
            }

            if (GUILayout.Button("Rebuild The Pool", EditorStyles.miniButton))
            {
                RebuildPool();
            }
        }


        foldout.boolValue = EditorGUILayout.Foldout(foldout.boolValue, "Reference", true);
        if (foldout.boolValue)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(self);
            EditorGUILayout.PropertyField(chunkPrefab);

            EditorGUI.indentLevel--;
        }


        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

    public void RebuildPool()
    {
        serializedObject.ApplyModifiedPropertiesWithoutUndo();
        serializedObject.Update();

        //destroy obj du pool
        while (chunks.arraySize > 0)
        {
            SerializedProperty current = chunks.GetArrayElementAtIndex(0);
            Object cch = current.objectReferenceValue;
            if (cch == null)
            {
                chunks.DeleteArrayElementAtIndex(0);
            }
            else
            {
                DestroyImmediate((cch as CoursChunk).gameObject);
                chunks.DeleteArrayElementAtIndex(0);
            }
        }

        // recréer le bon nombre d'object
        // les remttre en child + reposittionner
        // les reférencer dans l'array
        for (int i = 0; i < poolSize.intValue; i++)
        {
            GameObject go = PrefabUtility.InstantiatePrefab(chunkPrefab.objectReferenceValue as GameObject) as GameObject;
            Transform tr = go.transform;

            tr.SetParent(self.objectReferenceValue as Transform);
            tr.localPosition = Vector3.zero;
            tr.localEulerAngles = Vector3.zero;
            tr.localScale = Vector3.one;
            EditorUtility.SetDirty(tr);

            chunks.InsertArrayElementAtIndex(chunks.arraySize);
            chunks.GetArrayElementAtIndex(chunks.arraySize - 1).objectReferenceValue = go.GetComponent<CoursChunk>();
        }

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }

}
