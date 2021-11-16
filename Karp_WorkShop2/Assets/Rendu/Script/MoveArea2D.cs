using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea2D : MonoBehaviour
{

    [SerializeField] Transform CornerA;
    [SerializeField] Transform CornerB;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        CornerA.position = Vector3.Scale(new Vector3(1, 0, 1), CornerA.position);
        CornerB.position = Vector3.Scale(new Vector3(1, 0, 1), CornerB.position);

        Vector3 A2B = CornerB.position - CornerA.position;

        Debug.DrawRay(CornerA.position, A2B, Color.red);
        Debug.DrawRay(CornerB.position - Vector3.forward * A2B.z, -Vector3.Scale(new Vector3(1, 0, -1), A2B), Color.red);

        Debug.DrawRay(CornerA.position, Vector3.forward * A2B.z, Color.green);
        Debug.DrawRay(CornerB.position, Vector3.forward * -A2B.z, Color.green);
        Debug.DrawRay(CornerA.position, Vector3.right * A2B.x, Color.green);
        Debug.DrawRay(CornerB.position, Vector3.right * -A2B.x, Color.green);

    }
}
