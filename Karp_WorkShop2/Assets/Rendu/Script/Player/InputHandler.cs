using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : Singleton<InputHandler>
{
    [Header("Axis")]
    public string axisHori = "Horizontal";
    public string axisVerti = "Vertical";
    [Header("Button")]
    public string buttonClick = "Shoot";
    public string buttonReset = "Reset";
    [Space(10)]
    public UnityEvent onClick;
    public UnityEvent onReset;

    public Vector2 StickDir
    {
        get
        {
            return new Vector2(Input.GetAxis(axisHori), Input.GetAxis(axisVerti)).normalized;
        }
    }


    private void Update()
    {
        if(Input.GetButton(buttonClick))
        {
            onClick?.Invoke();
        }
        if (Input.GetButton(buttonReset))
        {
            onReset?.Invoke();
        }

    }
}
