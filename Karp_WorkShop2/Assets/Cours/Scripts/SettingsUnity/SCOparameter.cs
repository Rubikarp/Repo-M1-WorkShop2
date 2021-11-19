using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ChunckProfile_new", menuName = "ScriptableObjects/ChunckProfile")]
public class SCOparameter : ScriptableObject
{
    public string path = "Assets/";
    [Space(10)]
    public int width = 5;
    public int height = 5;
}
