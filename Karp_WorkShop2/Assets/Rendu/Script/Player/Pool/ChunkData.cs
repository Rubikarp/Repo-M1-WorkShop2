using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendu
{
    public enum TileType
    {
        Void,
        Coin,
        Bomb,
        Target
    }

    [CreateAssetMenu(fileName = "ChunckData_new", menuName = "Game/ChunckData")]
    public class ChunkData : ScriptableObject
    {
        public int width = 20;
        public int height = 10;

        public Vector3 Size
        {
            get
            {
                return new Vector3(width, 5, height);
            }
        }

        public TileType[] grid;

#if UNITY_EDITOR
        public Color[] tileColor;
        public TileType currentType;
#endif
    }
}

