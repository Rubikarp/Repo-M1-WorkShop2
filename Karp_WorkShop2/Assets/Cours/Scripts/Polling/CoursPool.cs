using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursPool : MonoBehaviour
{
    public List<CoursChunk> chunks;
    public List<CoursChunk> activeChunks;

    public GameObject chunkPrefab;
    public Transform self;

#if UNITY_EDITOR
    public bool foldout;
    public int poolSize;

#endif

    private void Update()
    {

        //Check in
        bool inRange = false;
        for (int i = 0; i < activeChunks.Count; i++)
        {
            Vector3 toChunk = activeChunks[i].transform.position - self.position;
            if (Mathf.Abs(toChunk.z) < 10)
            {
                inRange = true;
            }
        }
        if (!inRange)
        {
            SendNewChunk();
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

    public void SendNewChunk()
    {
        CoursChunk ch = GetFreeChunk();
        activeChunks.Add(ch);
        ch.StartMoving();
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
