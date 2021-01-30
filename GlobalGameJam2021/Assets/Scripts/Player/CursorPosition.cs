using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    public enum INPUT_MODE { MOUSE, CONTROLLER};
    public INPUT_MODE inputMode;
    private Vector2 mousePosition;
    private Vector2 cursorPosition;
    public float speed;

    void Update()
    {
        //tipo de input
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y") > 0)
        {
            inputMode = INPUT_MODE.MOUSE;
        }
        if (Input.GetAxis("MouseHorizontal") < 0 || Input.GetAxis("MouseHorizontal") > 0 || Input.GetAxis("MouseVertical") < 0 || Input.GetAxis("MouseVertical") > 0)
        {
            inputMode = INPUT_MODE.CONTROLLER;
        }

        //movimiento del cursor
        if (inputMode == INPUT_MODE.MOUSE)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;
        }
        if (inputMode == INPUT_MODE.CONTROLLER)
        {
            cursorPosition = new Vector2(Input.GetAxis("MouseHorizontal"), Input.GetAxis("MouseVertical"));
            transform.Translate(cursorPosition * speed * Time.deltaTime);
        }
    }
}