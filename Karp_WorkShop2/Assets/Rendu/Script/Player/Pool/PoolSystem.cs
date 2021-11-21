using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace Rendu
{
    public class PoolSystem : MonoBehaviour
    {
        public List<GameObject> allChunk;
        public List<Chunk> avaibleChunk;
        public List<Chunk> unavaibleChunk;
        Chunk lastChunkLauch;

        [Space(10)]
        public GameObject[] chunkPrefabs;
        public Transform self;

        public float scrollSpeed = 5f;
        public Vector3 scrollDir = Vector3.back;

        public float chunkSize = 10;
        public PoolBlock limit;

#if UNITY_EDITOR
        public bool foldout;
        public int poolSize;
#endif

        private void Start()
        {
            for (int i = 0; i < allChunk.Count; i++)
            {
                avaibleChunk.Add(allChunk[i].GetComponent<Chunk>());
            }

            /*
            Vector3 toLimit = self.position - limit.transform.position;
            for (float i = Mathf.CeilToInt(toLimit.magnitude / chunkSize); i > 0; i --)
            {
                Vector3 _spawnPos = self.position + (-toLimit.normalized * chunkSize * i);
                ChunkLaunch(_spawnPos);
            }*/
            ChunkLaunch(self.position);

        }
        private void Update()
        {
            //Check for NewChunk
            Vector3 toLastChunk = lastChunkLauch.transform.position - self.position;
            if (Mathf.Abs(toLastChunk.magnitude) > chunkSize)
            {
                Vector3 spawnPos = lastChunkLauch.transform.position - (toLastChunk.normalized * chunkSize);
                ChunkLaunch(spawnPos);
            }
        }

        public void ChunkLaunch(Vector3 pos)
        {
            Chunk ch = GetFreeChunk();
            lastChunkLauch = ch;

            avaibleChunk.Remove(ch);
            unavaibleChunk.Add(ch);
            ch.Init(pos, scrollSpeed, scrollDir);
        }
        public void ChunkRelease(Chunk chunk)
        {
            avaibleChunk.Add(chunk);
            unavaibleChunk.Remove(chunk);

            chunk.Reset();
        }
        //
        public Chunk GetFreeChunk()
        {
            if (avaibleChunk.Count > 0)
            {
                return avaibleChunk[Random.Range(0, avaibleChunk.Count - 1)];
            }

            Debug.Log("New Chunk");
            return ExtendPool();
        }
        public Chunk ExtendPool()
        {
            GameObject go = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length - 1)], self);
            allChunk.Add(go);
            Chunk ch = go.GetComponent<Chunk>();
            if (ch)
            {
                return ch;
            }
            else
            {
                go.AddComponent<Chunk>(); ;
                ch = go.GetComponent<Chunk>();

                return ch;
            }
        }
#if UNITY_EDITOR
        public void ExtendPoolChunk(GameObject prefab)
        {
            GameObject go = PrefabUtility.InstantiatePrefab((Object)prefab, self.transform) as GameObject;
            allChunk.Add(go);
            Chunk ch = go.GetComponent<Chunk>();
            if (!ch)
            {
                go.AddComponent<Chunk>(); ;
                ch = go.GetComponent<Chunk>();
            }
        }
        public void ExtendPoolChunkRandomly()
        {
            GameObject go = PrefabUtility.InstantiatePrefab((Object)chunkPrefabs[Random.Range(0, chunkPrefabs.Length - 1)], self.transform) as GameObject;
            allChunk.Add(go);
            Chunk ch = go.GetComponent<Chunk>();
            if (!ch)
            {
                go.AddComponent<Chunk>(); ;
                ch = go.GetComponent<Chunk>();
            }
        }
#endif

    }
}
