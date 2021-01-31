using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDos : MonoBehaviour
{
    public bool pillado = false;
    public bool playerArround = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerArround)
        {
            pillado = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
