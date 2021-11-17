using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea2D : MonoBehaviour
{
    public float height;

    public float Width
    {
        get
        {
            return Mathf.Abs(Left-Right);
        }
    }
    public float Height
    {
        get
        {
            return Mathf.Abs(Top - Bottom);
        }
    }
    #region Limits
    public float Top
    {
        get
        {
            return Mathf.Max(CornerA.position.z, CornerB.position.z);
        }
    }
    public float Bottom
    {
        get
        {
            return Mathf.Min(CornerA.position.z, CornerB.position.z);
        }
    }
    public float Right
    {
        get
        {
            return Mathf.Max(CornerA.position.x, CornerB.position.x);
        }
    }
    public float Left
    {
        get
        {
            return Mathf.Min(CornerA.position.x, CornerB.position.x);
        }
    }
    #endregion
    #region Corners
    public Vector3 TopRightCorner
    {
        get
        {
            return new Vector3( Right, height, Top );
        }
    }
    public Vector3 TopLeftCorner
    {
        get
        {
            return new Vector3( Left, height, Top);
        }
    }
    public Vector3 BotLeftCorner
    {
        get
        {
            return new Vector3( Left, height, Bottom );
        }
    }
    public Vector3 BotRightCorner
    {
        get
        {
            return new Vector3( Right, height, Bottom );
        }
    }
    #endregion

#if UNITY_EDITOR
    [SerializeField] Transform CornerA;
    [SerializeField] Transform CornerB;
#endif

    public bool InZone(Vector3 testPos)
    {
        Rect rect = new Rect(TopLeftCorner.x, TopLeftCorner.y, Width, Height);

        return rect.Contains(testPos - TopLeftCorner);
    }
    public Vector3 ClampIn(Vector3 testPos)
    {
        testPos.x = Mathf.Clamp(testPos.x, Left, Right);
        testPos.z = Mathf.Clamp(testPos.z, Bottom, Top);

        return testPos;
    }

    private void OnDrawGizmos()
    {
        CornerA.position = Vector3.Scale(new Vector3(1, 0, 1), CornerA.position);
        CornerB.position = Vector3.Scale(new Vector3(1, 0, 1), CornerB.position);

        Debug.DrawLine(TopLeftCorner, TopRightCorner, Color.green);
        Debug.DrawLine(TopRightCorner, BotRightCorner, Color.green);
        Debug.DrawLine(BotRightCorner, BotLeftCorner, Color.green);
        Debug.DrawLine(BotLeftCorner, TopLeftCorner, Color.green);
    }
}
