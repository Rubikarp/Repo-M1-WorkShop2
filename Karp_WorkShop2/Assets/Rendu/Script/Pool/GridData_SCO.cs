using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
    [CreateAssetMenu(fileName = "GridData_new", menuName = "Game/gridData")]
    public class GridData_SCO : ScriptableObject
    {
        public Vector2Int size = new Vector2Int(10, 10);

        public Tile[] tiles;

        public List<Tile[]> bombArea;
    }
}

