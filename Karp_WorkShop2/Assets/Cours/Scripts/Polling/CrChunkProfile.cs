using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileType
{
    Void,
    Coin,
    Ennemis
}

[CreateAssetMenu(fileName = "ChunckProfile_new", menuName = "ScriptableObjects/ChunckProfile")]
public class CrChunkProfile : ScriptableObject
{
    public int width = 5;
    public int height = 5;

    public TileType[] grid;

#if UNITY_EDITOR
    public Color[] tileColor;
    public TileType currentType;

#endif

}
