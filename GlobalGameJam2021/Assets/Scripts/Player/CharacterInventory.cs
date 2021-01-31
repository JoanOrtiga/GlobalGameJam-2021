using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterInventory : MonoBehaviour
{
    public int bookCounter;
    public Image[] books;
    public Color missingColor;
    public Color foundColor;

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        bookCounter = SceneManager.GetActiveScene().buildIndex - 1;

        GameObject[] booksArray = GameObject.FindGameObjectsWithTag("LibrosUI");
        
        for (int i = 0; i < books.Length; i++)
        {
            Image tempImg = booksArray[i].GetComponent<Image>();
            books[i] = tempImg;
            books[i].color = missingColor;
        }

        if (bookCounter > 0)
        {
            for (int i = 0; i < bookCounter; i++)
            {

                print(bookCounter);
                books[i].color = foundColor;
            }
        }
    }

    public void AddBook(int index)
    {
        bookCounter++;
        books[index].color = foundColor;
    }
}