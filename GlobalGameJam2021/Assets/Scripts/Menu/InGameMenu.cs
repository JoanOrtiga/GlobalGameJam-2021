using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject book;

    private void Start()
    {
        book.SetActive(false);
    }

    public void OpenBook()
    {
        book.SetActive(true);
    }
    
    public void CloseBook()
    {
        book.SetActive(false);
    }
}
