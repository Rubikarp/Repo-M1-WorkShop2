using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursPool : MonoBehaviour
{
    public List<CoursChunk> chunks;
    public List<CoursChunk> activeChunks;

    public GameObject chunkPrefab;
    public Transform self;

    CoursChunk lastChunk;
    Vector3 spawnPos;
    public float chunkSize = 10;
    public float zLimit = -10;

#if UNITY_EDITOR
    public bool foldout;
    public int poolSize;

#endif

    private void Start()
    {
        for (float i = 0; i < self.position.z - zLimit; i += chunkSize)
        {
            Vector3 _spawnPos = Vector3.back * i;
            SendNewChunk(_spawnPos);
        }
    }

    private void Update()
    {

        //Check in
        bool inRange = false;
        Vector3 toChunk = lastChunk.transform.position - self.position;
        if (Mathf.Abs(toChunk.z) < self.position.z - chunkSize)
        {
            spawnPos = lastChunk.transform.position - toChunk.normalized * chunkSize;
            inRange = true;
        }
        if (!inRange)
        {
            SendNewChunk(spawnPos);
            inRange = false;
        }

        //Check Out
        for (int i = 0; i < activeChunks.Count; i++)
        {
            if (activeChunks[i].transform.position.z < - 10)
            {
                EndChunk(activeChunks[i]);
            }
        }
    }

    public void SendNewChunk(Vector3 pos)
    {
        CoursChunk ch = GetFreeChunk();
        activeChunks.Add(ch);
        lastChunk = ch;
        ch.StartMoving(pos);
    }
    public void EndChunk(CoursChunk chunk)
    {
        activeChunks.Remove(chunk);
        chunk.StopMoving();
    }

    public CoursChunk GetFreeChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            if (!chunks[i].enabled)
            {
                return chunks[i];
            }
        }
        Debug.LogError("No chunck available");

        return ExtendPool();
    }
    public CoursChunk ExtendPool()
    {
        GameObject go = Instantiate(chunkPrefab, self);
        CoursChunk ch = go.GetComponent<CoursChunk>();
        if (ch)
        {
            return ch;
        }
        else
        {
            go.AddComponent<CoursChunk>(); ;
            ch = go.GetComponent<CoursChunk>();
            return ch;
        }
    }
}
