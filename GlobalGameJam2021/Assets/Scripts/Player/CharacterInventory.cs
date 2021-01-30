using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    public int bookCounter;
    public Image[] books;
    public Color missingColor;
    public Color foundColor;

    private void Start()
    {
        for (int i = 0; i < books.Length; i++)
        {
            books[i].color = missingColor;
        }
    }

    public void AddBook(int index)
    {
        bookCounter++;
        books[index].color = foundColor;
    }
}