using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHide : MonoBehaviour
{
    void Start()
    {
        HideCursor();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }
}