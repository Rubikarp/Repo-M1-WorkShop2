using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
    public class PoolManager : MonoBehaviour
    {
        public KarPool pool = new KarPool();

        public float poolElementScrollSpeed = 5f;
        public Vector3 poolElementScrollDir = Vector3.back;

        public float chunkSize = 10f;
        public float outPos = -10f;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
