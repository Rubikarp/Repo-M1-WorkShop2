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
        enabled = false;
    }

    public void loadProfile(CrChunkProfile profile)
    {

    }
    public void StartMoving()
    {
        self.localPosition = Vector3.zero;
        enabled = true;
    }

    public void StopMoving()
    {
        self.localPosition = Vector3.zero;
        enabled = false;
    }

    private void Update()
    {
        self.Translate(Vector3.back * scrollSpeed * Time.deltaTime);
    }
}