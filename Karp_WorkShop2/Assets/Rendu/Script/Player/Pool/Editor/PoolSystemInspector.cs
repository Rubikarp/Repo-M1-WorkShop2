using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEditor;
using UnityEngine;
using Rendu;

[CustomEditor(typeof(PoolSystem))]
public class PoolSystemInspector : Editor
{
    /// <summary>
    /// Ligne 117 definit taille max d'un chunk à 100
    /// </summary>
    
    PoolSystem self;

    ReorderableList allChunk;
    SerializedProperty allChunkProp;

    SerializedProperty chunksPrefabProp;
    SerializedProperty selfProp;
    SerializedProperty scrollSpeedProp;
    SerializedProperty scrollDirProp;
    SerializedProperty chunkSizeProp;
    SerializedProperty limitProp;

    bool loadFont = false;
    GUIStyle centerStyle = new GUIStyle();
    GUIStyle titleStyle = new GUIStyle();
    GUIStyle subTitleStyle = new GUIStyle();
    GUIStyle sectionStyle = new GUIStyle();

    GameObject dirObject = null;
    //Sinon mon gameobject est créé en play mode
#if UNITY_EDITOR

    // Start like
    private void OnEnable()
    {
        //Link variables to property
        self = target as PoolSystem;
        //Font
        loadFont = false;
        //Property
        allChunkProp = serializedObject.FindProperty(nameof(PoolSystem.allChunk));

        chunksPrefabProp = serializedObject.FindProperty(nameof(PoolSystem.chunkPrefabs));
        selfProp = serializedObject.FindProperty(nameof(PoolSystem.self));
        scrollSpeedProp = serializedObject.FindProperty(nameof(PoolSystem.scrollSpeed));
        scrollDirProp = serializedObject.FindProperty(nameof(PoolSystem.scrollDir));
        chunkSizeProp = serializedObject.FindProperty(nameof(PoolSystem.chunkSize));
        limitProp = serializedObject.FindProperty(nameof(PoolSystem.limit));

        //Create
        dirObject = new GameObject("(Temp)DirObject");
        dirObject.transform.SetParent(self.transform);
        dirObject.transform.rotation = Quaternion.LookRotation(scrollDirProp.vector3Value, Vector3.up);

        #region reordable list
        //Initialise la liste
        allChunk = new ReorderableList(serializedObject, allChunkProp, true, true, true, true);
        //linker à une serielizedProperty (une collection, array, liste,etc)
        //Preparer les callbacks
        allChunk.drawElementCallback += ElementDrawer;
        allChunk.drawHeaderCallback += HeaderDrawer;
        //
        allChunk.onAddCallback += AddCallBack;
        allChunk.onAddDropdownCallback += AddDropDownCallBack;
        allChunk.onRemoveCallback += RemoveCallBack;
        //
        allChunk.elementHeightCallback += ElementHeightCallBack;
    }

    void HeaderDrawer(Rect rect)
    {
        EditorGUI.LabelField(rect, "GameChunk");
    }
    void ElementDrawer(Rect rect, int index, bool isActive, bool isFocused)
    {
        EditorGUI.PropertyField(rect, allChunkProp.GetArrayElementAtIndex(index));
    }

    void AddCallBack(ReorderableList rList)
    {
        allChunkProp.arraySize++;
        AddRandomChunk();
    }
    private void AddDropDownCallBack(Rect buttonRect, ReorderableList list)
    {
        GenericMenu gm = new GenericMenu();
        if (chunksPrefabProp.arraySize > 0)
        {
            for (int i = 0; i < chunksPrefabProp.arraySize; i++)
            {
                gm.AddItem(
                    new GUIContent(self.chunkPrefabs[i].gameObject.name)
                    , false, AddChunk, self.chunkPrefabs[i]);
            }
            gm.ShowAsContext();
        }
        else
        {
            Debug.LogWarning("Can't find a GameObject with the PoolBlock component");
        }

    }

    void RemoveCallBack(ReorderableList rList)
    {
        allChunkProp.DeleteArrayElementAtIndex(rList.index);
    }
    void ReorderCallback(ReorderableList rList)
    {

    }

    float ElementHeightCallBack(int index)
    {
        float numberOfLine = EditorGUIUtility.currentViewWidth < 334 ? 2 : 1;
        return (EditorGUIUtility.singleLineHeight * numberOfLine) + 1;
    }
    #endregion

    private void OnDisable()
    {
        //Suppr
        DestroyImmediate(dirObject.gameObject);
    }

    // Update like
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        if (!loadFont)
        {
            loadFont = true;
            //
            titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.fontSize = Mathf.CeilToInt(EditorGUIUtility.singleLineHeight * 2f);
            //
            subTitleStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
            subTitleStyle.alignment = TextAnchor.LowerLeft;
            subTitleStyle.contentOffset = new Vector2Int(10, 0);
            subTitleStyle.fontSize = Mathf.CeilToInt(EditorGUIUtility.singleLineHeight * 1.25f);
            //
            centerStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
            centerStyle.alignment = TextAnchor.MiddleCenter;
            //
            sectionStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
            sectionStyle.alignment = TextAnchor.MiddleLeft;
            sectionStyle.contentOffset = new Vector2Int(30,0);
            sectionStyle.fontSize = Mathf.CeilToInt(EditorGUIUtility.singleLineHeight * 0.75f);
            sectionStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f,1);
        }

        //serializedObject copy ma target
        serializedObject.Update();

        int allChunkCount = self.avaibleChunk.Count + self.unavaibleChunk.Count;
        using (new GUILayout.VerticalScope())
        {
            using (new EditorGUI.IndentLevelScope())
            {
                //Title
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Pool System", titleStyle);
                }
                //Status
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label("Status", subTitleStyle);
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        GUILayout.Label(new GUIContent(self.unavaibleChunk.Count.ToString() + "/" + allChunkCount.ToString(), " "), centerStyle);
                        using (new GUILayout.HorizontalScope())
                        {
                            float loadPourcent = (float)self.unavaibleChunk.Count / (float)allChunkCount;
                            Rect sliderZone = EditorGUILayout.GetControlRect();
                            EditorGUI.ProgressBar(sliderZone, loadPourcent, (loadPourcent * 100).ToString() + "%");
                        }
                    }
                    GUILayout.Space(EditorGUIUtility.singleLineHeight/2);
                    using (new EditorGUI.IndentLevelScope())
                    {
                        allChunk.DoLayoutList();
                        GUILayout.Space(2);
                    }

                }
                //Parameter
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label("Parameter", subTitleStyle);
                    using (new EditorGUI.IndentLevelScope(2))
                    {
                        GUILayout.Label("Chunk", sectionStyle);
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.Label("", GUILayout.Width(30));//OffSet un peu salle
                            GUILayout.Label("Size", GUILayout.Width(30));
                            chunkSizeProp.floatValue = EditorGUILayout.Slider(chunkSizeProp.floatValue, 0f, 100f);
                        }
                        EditorGUILayout.PropertyField(chunksPrefabProp);
                        ///
                        GUILayout.Label("References", sectionStyle);
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.PropertyField(selfProp);
                            if (GUILayout.Button(new GUIContent("Auto", "Automatically get the ref")))
                            {
                                selfProp.objectReferenceValue = self.transform;
                            }
                        }
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.PropertyField(limitProp);
                            if (GUILayout.Button(new GUIContent("Find", "Automatically look for a limit component in the scene")))
                            {
                                LookingForPoolStoper();
                            }
                        }
                        ///
                        GUILayout.Label("Behavior", sectionStyle);
                        EditorGUILayout.PropertyField(scrollSpeedProp);
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.PropertyField(scrollDirProp);
                            if (GUILayout.Button(new GUIContent("Normalise", "")))
                            {
                                scrollDirProp.vector3Value = scrollDirProp.vector3Value.normalized;
                            }
                        }
                    }
                }
            }
        }

        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
        float zoomScaler = HandleUtility.GetHandleSize(self.transform.position);

        //serializedObject copy ma target
        serializedObject.Update();

        if (dirObject != null) dirObject.transform.rotation = Quaternion.LookRotation(scrollDirProp.vector3Value, Vector3.up);

        Matrix4x4 matrix = Matrix4x4.TRS(self.transform.position, self.transform.rotation, self.transform.lossyScale);
        using (new Handles.DrawingScope(Color.green, matrix))
        {
            if (dirObject != null)
            {
                #region Direction
                //Rotation
                Vector3 offSet = Vector3.up * 10f;

                dirObject.transform.rotation = Handles.DoRotationHandle(dirObject.transform.rotation, offSet);
                scrollDirProp.vector3Value = dirObject.transform.forward;

                Handles.DrawLine(offSet, offSet + scrollDirProp.vector3Value * 3, 4f);
                //Show number
                GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                style.fontSize = 20;
                style.normal.textColor = Color.blue;
                Handles.Label(offSet, "Scroll Direction " + scrollDirProp.vector3Value.ToString(), style);
                #endregion
            }
        }
        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

    void LookingForPoolStoper()
    {
        PoolBlock[] findBlocker = GameObject.FindObjectsOfType<PoolBlock>();
        GenericMenu gm = new GenericMenu();
        if (findBlocker.Length > 0)
        {
            for (int i = 0; i < findBlocker.Length; i++)
            {
                gm.AddItem(
                    new GUIContent("FindsBlocker/" + findBlocker[i].gameObject.name /*+ "->(PoolBlock)"*/)
                    , false, SetPoolStoper, findBlocker[i]);
            }
            gm.ShowAsContext();
        }
        else
        {
            Debug.LogWarning("Can't find a GameObject with the PoolBlock component");
        }
    }
    void SetPoolStoper(object arg)
    {
        serializedObject.Update();

        limitProp.objectReferenceValue = (PoolBlock)arg;

        serializedObject.ApplyModifiedProperties();
    }

    void AddRandomChunk()
    {
        serializedObject.Update();

        self.ExtendPoolChunkRandomly();

        serializedObject.ApplyModifiedProperties();

    }
    void AddChunk(object arg)
    {
        serializedObject.Update();

        self.ExtendPoolChunk((GameObject)arg);

        serializedObject.ApplyModifiedProperties();
    }
#endif
}
