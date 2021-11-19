using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    public Transform head;
    public Camera cam;

    RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.point != null)
                {
                    Debug.DrawLine(head.position, hit.point, Color.red);
                }
            }
        }
    }
}
