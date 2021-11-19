using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
    [System.Serializable]
    public class GameGrid
    {
        [Header("Parameter")]
        public float cellSize = 1;
        public Vector2Int size = new Vector2Int(10, 10);
        public Vector2 offSet = new Vector2Int(0, 0);

        [Space(10)]
        [Header("Data")]
        public GridData_SCO Data;

        /// <summary>
        /// Convert a GameGrid Pos to a worldPos
        /// WARNING ! The result can be extrapolate farther than the GridSize
        /// </summary>
        /// <param name="posInGrid"> Position on the GameGrid (can be negative)</param>
        /// <returns></returns>
        public Vector3 TileToPos(Vector2Int posInGrid)
        {
            //Get bottom left corner |_
            Vector3 bottomLeft = new Vector3(offSet.x, offSet.y, 0);
            bottomLeft -= new Vector3(size.x, size.y, 0) * 0.5f * cellSize;
            //Centre de la case
            bottomLeft += new Vector3(cellSize * 0.5f, cellSize * 0.5f, 0);

            Vector3 result = bottomLeft + (new Vector3(posInGrid.x, posInGrid.y, 0) * cellSize);

            return result;
        }

        /// <summary>
        /// Convert a Point on the GamePlane to the GameGrid Pos related
        /// WARNING ! it can result a pos outside of the actual grid
        /// </summary>
        /// <param name="planePos"> point on the plane where you look for the tile</param>
        /// <returns></returns>
        public Vector2Int PosToTile(Vector3 planePos)
        {
            //Get bottom left corner |_
            Vector3 bottomLeft = new Vector3(offSet.x, offSet.y, 0);
            bottomLeft -= new Vector3(size.x, size.y, 0) * 0.5f * cellSize;
            //Pas de centrage sur la case car floor, si round activer la ligne
            //bottomLeft += new Vector3(cellSize * 0.5f, cellSize * 0.5f, 0);

            Vector3 posRelaToGrid = planePos - bottomLeft;
            float returnToCellsize = 1 / cellSize;
            Vector2Int result = new Vector2Int(Mathf.FloorToInt(posRelaToGrid.x * returnToCellsize), Mathf.FloorToInt(posRelaToGrid.y * returnToCellsize));

            return result;
        }

        private void OnDrawGizmos()
        {
            /*
            if (showDebug)
            {
                Vector3 startPos = new Vector3(offSet.x, offSet.y, 0);
                startPos -= new Vector3(size.x, size.y, 0) * 0.5f * cellSize;

                float halfCell = cellSize * 0.5f;

                if (showCenter)
                {
                    #region center point

                    Gizmos.color = Color.green;
                    for (int x = 0; x < size.x; x++)
                    {
                        for (int y = 0; y < size.y; y++)
                        {
                            Gizmos.DrawWireSphere(startPos + new Vector3(x * cellSize, y * cellSize, 0) + new Vector3(cellSize * 0.5f, cellSize * 0.5f, 0), cellSize * 0.5f * 0.8f);
                        }
                    }
                    #endregion
                }

                //GameGrid decals
                Gizmos.color = Color.red;
                for (int x = 0; x <= size.x; x++)
                {
                    Debug.DrawRay(startPos + new Vector3(cellSize * x, 0, 0), Vector3.up * size.y * cellSize, Color.red);
                }
                for (int y = 0; y <= size.y; y++)
                {
                    Debug.DrawRay(startPos + new Vector3(0, cellSize * y, 0), Vector3.right * size.x * cellSize, Color.red);
                }
            }
            */
        }
    }
}
