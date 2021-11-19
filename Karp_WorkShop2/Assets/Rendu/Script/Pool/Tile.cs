using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{

    public struct Tile
    {
        Vector2Int pos;

        public bool bombZone;
        public float spawnRate;
    }
}