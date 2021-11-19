using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
    public class Chunk : MonoBehaviour
    {
        public Transform self;

        [Header("Parameter")]
        public float scrollSpeed = 5f;
        public Vector3 scrollDir  = Vector3.back;

        public void Awake()
        {
            enabled = false;
        }

        public void PoolInitialise(float speed, Vector3 dir, Vector3 pos)
        {
            scrollSpeed = speed;
            scrollDir = dir;
            self.localPosition = pos;

            gameObject.SetActive(true);
        }

        public void PoolReset()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            self.Translate(scrollDir * scrollSpeed * Time.deltaTime);
        }

    }
}