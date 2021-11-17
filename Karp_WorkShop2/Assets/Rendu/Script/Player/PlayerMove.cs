using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public InputHandler input;
    public MoveArea2D moveArea;
    public Transform self;

    [Header("Parameters")]
    public float runSpeed = 10;
    public AnimationCurve accCurve;

    [Header("Stats Movement")]
    public Vector3 moveDir = Vector3.zero;
    [HideInInspector] public Vector3 lastDir = Vector2.zero;
    public float accTime = 0f;
    [Range(-1, 1)] public float accThreshold = 0.3f;

    void Start()
    {
        
    }

    void Update()
    {
        moveDir = new Vector3(input.StickDir.x,0, input.StickDir.y).normalized;
        self.position = moveArea.ClampIn(self.position);

        //MOVE
        if (moveDir == Vector3.zero)
        {
            accTime = 0f;
            return;
        }
        else
        {
            moveDir = moveDir.normalized;
        }

        //Process Acc
        if (Vector3.Dot(moveDir, lastDir) > accThreshold)
        {
            accTime = Time.time;
        }
        accTime += Time.deltaTime;
        float currentAcc = accCurve.Evaluate(accTime);

        //Translation
        self.Translate(moveDir * currentAcc * runSpeed * Time.deltaTime, Space.World);

        //Save last fr value
        lastDir = moveDir;
    }
}
