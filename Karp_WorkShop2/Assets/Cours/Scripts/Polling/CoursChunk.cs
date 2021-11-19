using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursChunk : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public Transform self;

    public CrChunkProfile profile;

    public void Awake()
    {
        StopMoving();
    }

    public void loadProfile(CrChunkProfile profile)
    {

    }
    public void StartMoving(Vector3 pos)
    {
        self.localPosition = pos;
        gameObject.SetActive(true);
    }

    public void StopMoving()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        self.Translate(Vector3.back * scrollSpeed * Time.deltaTime);
    }
}