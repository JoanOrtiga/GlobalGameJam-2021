using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject book;
    private CursorHide cursor;

    private void Start()
    {
        cursor = GetComponent<CursorHide>();
        //book.SetActive(false);
    }

    public void OpenBook()
    {
        book.SetActive(true);
        cursor.ShowCursor();
    }
    
    public void CloseBook()
    {
        book.SetActive(false);
        cursor.HideCursor();
    }
}
