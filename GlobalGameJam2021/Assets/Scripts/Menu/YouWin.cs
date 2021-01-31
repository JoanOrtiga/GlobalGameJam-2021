using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    private CharacterInventory characterInv;
    public GameObject WinPanel;
    private CursorHide cursor;

    void Start()
    {
        characterInv = FindObjectOfType<CharacterInventory>();
        cursor = GetComponent<CursorHide>();
        WinPanel.SetActive(false);
    }

    void Update()
    {
        if (characterInv.bookCounter == 4)
        {
            WinPanel.SetActive(true);
            cursor.ShowCursor();
        }
    }
}
