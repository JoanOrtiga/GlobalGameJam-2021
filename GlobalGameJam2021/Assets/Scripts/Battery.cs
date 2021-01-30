using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public bool playerArround = false;
    private CharacterLight character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerArround)
        {
            character.AddLight();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            character = collision.gameObject.GetComponent<CharacterLight>();
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
