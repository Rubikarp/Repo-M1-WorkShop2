using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2D : MonoBehaviour
{
    public float height = 0f;

    public float ZoneWidth
    {
        get
        {
            return Mathf.Abs(Left-Right);
        }
    }
    public float ZoneHeight
    {
        get
        {
            return Mathf.Abs(Up - Down);
        }
    }
    #region Limits
    public float Up
    {
        get
        {
            return Mathf.Max(CornerA.z, CornerB.z);
        }
    }
    public float Down
    {
        get
        {
            return Mathf.Min(CornerA.z, CornerB.z);
        }
    }
    public float Right
    {
        get
        {
            return Mathf.Max(CornerA.x, CornerB.x);
        }
    }
    public float Left
    {
        get
        {
            return Mathf.Min(CornerA.x, CornerB.x);
        }
    }
    #endregion
    #region Corners
    public Vector3 Center
    {
        get
        {
            return DownLeftCorner + ((UpRightCorner - DownLeftCorner) * 0.5f);
        }
    }
    public Vector3 UpRightCorner
    {
        get
        {
            return new Vector3(Right, height, Up);
        }
    }
    public Vector3 UpLeftCorner
    {
        get
        {
            return new Vector3( Left, height, Up);
        }
    }
    public Vector3 DownLeftCorner
    {
        get
        {
            return new Vector3( Left, height, Down );
        }
    }
    public Vector3 DownRightCorner
    {
        get
        {
            return new Vector3( Right, height, Down );
        }
    }
    #endregion

#if UNITY_EDITOR
    public bool lineProject;
    public float HandleThickness =  4f;

    public Vector3 CornerA = Vector3.one * 4f;
    public Vector3 CornerB = -Vector3.one * 4f;
#endif

    public bool InZone(Vector3 testPos)
    {
        Rect rect = new Rect(UpLeftCorner.x, UpLeftCorner.y, ZoneWidth, ZoneHeight);

        return rect.Contains(testPos - UpLeftCorner);
    }
    public Vector3 ClampIn(Vector3 testPos)
    {
        testPos.x = Mathf.Clamp(testPos.x, Left, Right);
        testPos.z = Mathf.Clamp(testPos.z, Down, Up);

        return testPos;
    }
}
