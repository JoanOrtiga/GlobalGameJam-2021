using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItem : MonoBehaviour
{
    public int bookIndex;

    public bool playerArround = false;
    private CharacterInventory character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerArround)
        {
            character.AddBook(bookIndex);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            character = collision.gameObject.GetComponent<CharacterInventory>();
            playerArround = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerArround = false;
        }
    }
}
