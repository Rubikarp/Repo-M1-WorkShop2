using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendu
{
    public class PoolBlock : MonoBehaviour
    {
        public PoolSystem poolSyst;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Chunk>())
            {
                Chunk chunk = other.gameObject.GetComponent<Chunk>();
                poolSyst.ChunkRelease(chunk);
            }
        }
    }
}
