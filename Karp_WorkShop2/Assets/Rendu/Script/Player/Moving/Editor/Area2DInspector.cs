using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Area2D))]

public class Area2DInspector : Editor
{
    private const float HandleSize = 1f;

    SerializedProperty CornerA;
    SerializedProperty CornerB;
    //
    SerializedProperty height;
    SerializedProperty drawHeight;
    SerializedProperty thickness;

    private Area2D objectLinked;
    private Transform self;

    private Tool LastTool = Tool.None;
    Vector3 otherCornerA = Vector3.zero;
    Vector3 otherCornerB = Vector3.zero;

    // Start like
    private void OnEnable()
    {
        //Recup linkedScript
        objectLinked = target as Area2D;
        self = objectLinked.transform;

        //Link variables to property
        CornerA = serializedObject.FindProperty(nameof(Area2D.CornerA));
        CornerB = serializedObject.FindProperty(nameof(Area2D.CornerB));
        height = serializedObject.FindProperty(nameof(Area2D.height));
        //
        drawHeight = serializedObject.FindProperty(nameof(Area2D.lineProject));
        thickness = serializedObject.FindProperty(nameof(Area2D.HandleThickness));

        //Hide The transform
        LastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void OnDisable()
    {
        //Reactivate The transform
        Tools.current = LastTool;
    }

    // Update like
    public override void OnInspectorGUI()
    {
        //serializedObject copy ma target
        serializedObject.Update();

        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Property", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        Vector2 editA = EditorGUILayout.Vector2Field("angle A", new Vector2(objectLinked.CornerA.x, objectLinked.CornerA.z));
                        Vector2 editB = EditorGUILayout.Vector2Field("angle B", new Vector2(objectLinked.CornerB.x, objectLinked.CornerB.z));
                        if (check.changed)
                        {
                            objectLinked.CornerA = new Vector3(editA.x, 0, editA.y);
                            objectLinked.CornerB = new Vector3(editB.x, 0, editB.y);

                            CornerA.vector3Value = new Vector3(editA.x, 0, editA.y);
                            CornerB.vector3Value = new Vector3(editB.x, 0, editB.y);
                        }
                    }
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        Vector2 editC = EditorGUILayout.Vector2Field("angle C", new Vector2(otherCornerA.x, otherCornerA.z));
                        Vector2 editD = EditorGUILayout.Vector2Field("angle D", new Vector2(otherCornerB.x, otherCornerB.z));

                        if (check.changed)
                        {
                            otherCornerA = new Vector3(editC.x, 0, editC.y);
                            otherCornerB = new Vector3(editD.x, 0, editD.y);

                            CornerA.vector3Value = new Vector3(otherCornerA.x, height.floatValue, otherCornerB.z);
                            CornerB.vector3Value = new Vector3(otherCornerB.x, height.floatValue, otherCornerA.z);
                        }
                    }
                }

                EditorGUILayout.PropertyField(height);
                using (new EditorGUI.DisabledGroupScope(true))
                {
                    EditorGUILayout.FloatField("ZoneHeight", objectLinked.ZoneHeight);
                    EditorGUILayout.FloatField("ZoneWidth", objectLinked.ZoneWidth);
                }
            }
        }
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(drawHeight);
                EditorGUILayout.PropertyField(thickness);
            }
        }

        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        //serializedObject copy ma target
        serializedObject.Update();

        //Hide the transform tool
        Tools.current = Tool.None;

        float zoomScaler = HandleUtility.GetHandleSize(self.position);

        Matrix4x4 matrix = Matrix4x4.TRS(self.position, self.rotation, self.lossyScale);
        using (new Handles.DrawingScope(Color.red, matrix))
        {
            #region Height
            //Transform
            Vector3 offSet = objectLinked.Center - self.transform.localPosition;
            self.transform.localPosition = Handles.PositionHandle(self.transform.localPosition + offSet, self.transform.rotation) - offSet;

            float tempHeight = self.transform.localPosition.y;
            height.floatValue += tempHeight;

            self.transform.localPosition = Vector3.Scale(self.transform.localPosition, new Vector3(1, 0, 1));
            CornerA.vector3Value += self.transform.localPosition;
            CornerB.vector3Value += self.transform.localPosition;
            self.transform.localPosition = new Vector3(0, 0, 0);

            /*
            Handles.color = Color.blue;
            //TODO faire un AddValueHandle à la main
            evolve = Handles.ScaleValueHandle(evolve, CornerA.vector3Value + (CornerB.vector3Value - CornerA.vector3Value) * 0.5f, 
                new Quaternion(1, 0, 0, -1).normalized, 2f * zoomScaler, Handles.ConeHandleCap, 1);
            height.floatValue = evolve - 1 / evolve;
            //Set Height
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, height.floatValue, self.transform.localPosition.z);
            */
            #endregion

            #region Line
            Handles.color = Color.green;
            float _thickness = thickness.floatValue;
            if (drawHeight.boolValue)
            {
                for (int i = 0; i < Mathf.CeilToInt(_thickness); i++)
                {
                    Handles.DrawLine(objectLinked.UpLeftCorner + (Vector3.up * i), objectLinked.UpRightCorner + (Vector3.up * i), HandleSize * (_thickness - i));
                    Handles.DrawLine(objectLinked.UpRightCorner + (Vector3.up * i), objectLinked.DownRightCorner + (Vector3.up * i), HandleSize * (_thickness - i));
                    Handles.DrawLine(objectLinked.DownRightCorner + (Vector3.up * i), objectLinked.DownLeftCorner + (Vector3.up * i), HandleSize * (_thickness - i));
                    Handles.DrawLine(objectLinked.DownLeftCorner + (Vector3.up * i), objectLinked.UpLeftCorner + (Vector3.up * i), HandleSize * (_thickness - i));
                }
            }
            else
            {
                Handles.DrawLine(objectLinked.UpLeftCorner , objectLinked.UpRightCorner , HandleSize * _thickness);
                Handles.DrawLine(objectLinked.UpRightCorner, objectLinked.DownRightCorner , HandleSize * _thickness);
                Handles.DrawLine(objectLinked.DownRightCorner, objectLinked.DownLeftCorner , HandleSize * _thickness);
                Handles.DrawLine(objectLinked.DownLeftCorner, objectLinked.UpLeftCorner , HandleSize * _thickness);
            }
            #endregion

            #region Corner
            Handles.color = Color.red;
            //
            CornerA.vector3Value = Handles.FreeMoveHandle(CornerA.vector3Value, Quaternion.identity, HandleSize, Vector3.one, Handles.SphereHandleCap);
            CornerA.vector3Value = new Vector3(CornerA.vector3Value.x, height.floatValue, CornerA.vector3Value.z);
            //
            CornerB.vector3Value = Handles.FreeMoveHandle(CornerB.vector3Value, Quaternion.identity, HandleSize, Vector3.one, Handles.SphereHandleCap);
            CornerB.vector3Value = new Vector3(CornerB.vector3Value.x, height.floatValue, CornerB.vector3Value.z);
            //
            otherCornerA = new Vector3(CornerA.vector3Value.x, height.floatValue, CornerB.vector3Value.z);
            otherCornerA = Handles.FreeMoveHandle(otherCornerA, Quaternion.identity, HandleSize, Vector3.one, Handles.SphereHandleCap);
            //
            otherCornerB = new Vector3(CornerB.vector3Value.x, height.floatValue, CornerA.vector3Value.z);
            otherCornerB = Handles.FreeMoveHandle(otherCornerB, Quaternion.identity, HandleSize, Vector3.one, Handles.SphereHandleCap);
            //
            CornerA.vector3Value = new Vector3(otherCornerA.x, height.floatValue, otherCornerB.z);
            CornerB.vector3Value = new Vector3(otherCornerB.x, height.floatValue, otherCornerA.z);
            #endregion

            #region Try mini transform sur X/Z
            //Try with hand made arrow
            /*
            CornerA.vector3Value = new Vector3(
                Handles.ScaleValueHandle(CornerA.vector3Value.x, CornerA.vector3Value, new Quaternion(1, 0, 0, 0).normalized, 5f * zoomScaler, Handles.ArrowHandleCap, 1),
                height.floatValue,
                Handles.ScaleValueHandle(CornerA.vector3Value.z, CornerA.vector3Value, new Quaternion(0, 1, 0, 1).normalized, 5f * zoomScaler, Handles.ArrowHandleCap, 1)
                );

            CornerB.vector3Value = new Vector3(
                Handles.ScaleValueHandle(CornerB.vector3Value.x, CornerB.vector3Value, new Quaternion(0, 1, 0, -1).normalized,5f * zoomScaler, Handles.ArrowHandleCap, 1),
                height.floatValue,
                Handles.ScaleValueHandle(CornerB.vector3Value.z, CornerB.vector3Value, new Quaternion(0, 0, 0, 1).normalized, 5f * zoomScaler, Handles.ArrowHandleCap, 1)
                );
            */
            #endregion

        }
        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }

}
