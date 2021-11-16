using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CourChunckProfileEditorWindow : EditorWindow
{
    CrChunkProfile currentChunk;

    SerializedObject serializedObject;
    SerializedProperty gridProp;
    SerializedProperty widthProp, heightProp;
    SerializedProperty tileColorProp;
    SerializedProperty currentTileType;

    float marginRatio;
    bool isClicking;
    Vector2 mousePos;
    Rect rectIn;

    public void InitWindow(CrChunkProfile _currentChunk)
    {
        currentChunk = _currentChunk;
        
        serializedObject = new SerializedObject(currentChunk);

        widthProp = serializedObject.FindProperty(nameof(CrChunkProfile.width));
        heightProp = serializedObject.FindProperty(nameof(CrChunkProfile.height));
        gridProp = serializedObject.FindProperty(nameof(CrChunkProfile.grid));

        tileColorProp = serializedObject.FindProperty(nameof(CrChunkProfile.tileColor));
        currentTileType = serializedObject.FindProperty(nameof(CrChunkProfile.currentType));

        marginRatio = 0.05f;

    }

    void OnGUI()
    {
        ProcessEvent();

        serializedObject.Update();

        using (var check = new EditorGUI.DisabledGroupScope(true))
        {
            EditorGUILayout.ObjectField("Current Chunk", currentChunk, typeof(CrChunkProfile));
            
            EditorGUILayout.Space();

            EditorGUILayout.Vector2Field("Debug", Event.current.mousePosition);
            EditorGUILayout.Vector2Field("Debug", Event.current.mousePosition);
        }
        using (new GUILayout.HorizontalScope())
        {

            EditorGUILayout.PropertyField(currentTileType);
        }

        EditorGUILayout.Space(); 

        EditorGUILayout.PropertyField(tileColorProp);

        using (new GUILayout.HorizontalScope())
        {

            EditorGUILayout.PropertyField(widthProp);
            EditorGUILayout.PropertyField(heightProp);


            EditorUtility.SetDirty(currentChunk);
        }

        if (GUILayout.Button("Update Array Size", EditorStyles.miniButton))
        {
            gridProp.arraySize = widthProp.intValue * heightProp.intValue;
        }


        EditorGUILayout.Space();

        float totalWidth = EditorGUIUtility.currentViewWidth;
        float gridWidth = totalWidth * (1f - 2f * marginRatio);
        Rect nextRect = EditorGUILayout.GetControlRect();

        Rect area = new Rect(nextRect.x + totalWidth * marginRatio, nextRect.y, gridWidth, gridWidth);
        EditorGUI.DrawRect(area, Color.gray);

        if (currentChunk.width < 0) return;
        if (currentChunk.height < 0) return;

        #region Draw Square Grid
        float cellToSpaceRatio = 4f;
        float totalCellWidth = gridWidth * (cellToSpaceRatio) / (cellToSpaceRatio + 1f);
        float cellWidth = totalCellWidth / (float)currentChunk.width;
        float totalSpaceWitdh = gridWidth - totalCellWidth;
        float spaceWidth = totalSpaceWitdh / ((float)currentChunk.width + 1);
        float curY = area.y + spaceWidth;

        for (int y = 0; y < currentChunk.height; y++)
        {
            float curX = area.x;

            for (int x = 0; x < currentChunk.width; x++)
            {
                curX += spaceWidth;

                Rect rect = new Rect(curX, curY, cellWidth, cellWidth);
                curX += cellWidth;

                int tileIndex = y * currentChunk.height + x;

                //Utilisateur peint
                bool isPaintingOverThis = isClicking && rect.Contains(mousePos);
                //TODO
                if (isPaintingOverThis)
                {
                    gridProp.GetArrayElementAtIndex(tileIndex).enumValueIndex = currentTileType.enumValueIndex;
                    Debug.Log("The tile : " + x + " "+ y + " " +
                        "in rect "+ rect + "have value changed" +
                        " at pose " + mousePos);
                }

                //Draw tile
                int enumIndexPalette = gridProp.GetArrayElementAtIndex(tileIndex).enumValueIndex;
                Color rendColor = tileColorProp.GetArrayElementAtIndex(enumIndexPalette).colorValue;
                EditorGUI.DrawRect(rect, rendColor);
                EditorGUI.TextArea(rect, "Pos"+ rect.x + "/" + rect.y + "\n Size" + rect.width + "/" + rect.height);

            }
            curY += cellWidth;
            curY += spaceWidth;
            GUILayout.Space(5);
        }
        #endregion

        Repaint();
        serializedObject.ApplyModifiedProperties();
    }

    void ProcessEvent()
    {
        mousePos = Event.current.mousePosition;

        if (Event.current.type == EventType.MouseDown)
        {
            isClicking = true;
        }
        if (Event.current.type == EventType.MouseUp)
        {
            isClicking = false;
        }
    }
}
