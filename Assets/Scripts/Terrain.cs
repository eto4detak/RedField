using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChechClickRightMouse();
    }

    private void OnMouseDown()
    {
        ClickLeftMouse();
    }
    private void ChechClickRightMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClickRightMouse();
        }
    }
    private void ClickLeftMouse()
    {
        Debug.Log("OnMouseDown");
        MouseManager.LeftClick(gameObject, Input.mousePosition);
    }
    private void ClickRightMouse()
    {
        Debug.Log("Right ChechClickRightMouse");

        MouseManager.RightClick(gameObject, Input.mousePosition);

    }
}
