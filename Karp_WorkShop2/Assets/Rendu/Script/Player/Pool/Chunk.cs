using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Rendu
{
    public class Chunk : MonoBehaviour
    {
        [Header("Ref")]
        public Transform self;
        public BoxCollider coll;
        //public ProBuilderMesh mesh;

        [Header("Behaviour")]
        public float scrollSpeed = 5f;
        public Vector3 scrollDir = Vector3.back;

        [Header("Data")]
        public ChunkData profile;

        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            self.Translate(scrollDir * scrollSpeed * Time.deltaTime);
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }

        public void Init(Vector3 pos, float speed, Vector3 dir)
        {
            self.position = pos;
            gameObject.SetActive(true);
        }

        public void loadProfile(ChunkData profile)
        {
            coll.size =  profile.Size;
        }
    }
}
