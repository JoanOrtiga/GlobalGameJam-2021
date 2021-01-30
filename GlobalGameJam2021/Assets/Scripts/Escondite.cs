using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escondite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer outline;
    public Color transparent;
    public Color opaque;
    public Transform hideSpot;
    public Transform spawnSpot;
    private Transform playerTransform;
    private CharacterMovement playerMovement;
    public bool playerArround = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        outline = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerArround)
        {
            if (playerMovement.hiding)
            {
                sprite.color = opaque;
                outline.color = opaque;
                playerTransform.position = spawnSpot.position;
                playerMovement.canMove = true;
                playerMovement.hiding = false;
            }
            else
            {
                sprite.color = transparent;
                outline.color = transparent;
                playerTransform.position = hideSpot.position;
                playerMovement.canMove = false;
                playerMovement.hiding = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            playerMovement = collision.gameObject.GetComponent<CharacterMovement>();
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
