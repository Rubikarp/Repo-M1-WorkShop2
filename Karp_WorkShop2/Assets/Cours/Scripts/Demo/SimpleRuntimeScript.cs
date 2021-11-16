using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct myStruct
{
    public float aFloat;
    public Color aColor;

}

public class SimpleRuntimeScript : MonoBehaviour
{
    public Color someColor = Color.blue;
    public Color[] someColors = new Color[3];
    public float someFloat = 31.42f;
    public AnimationCurve someCurve;
    public Transform someRef;

    public myStruct someStruc ;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
