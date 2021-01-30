using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escondite : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    private SpriteRenderer sRendererOutline;

    public Sprite opened;
    public Sprite openedOutline;
    public Sprite closed;
    public Sprite closedOutline;

    public Color transparent;
    public Color opaque;

    public Transform hideSpot;
    public Transform spawnSpot;

    private Transform playerTransform;
    private CharacterMovement playerMovement;
    public bool playerArround = false;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRendererOutline = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerArround)
        {
            if (playerMovement.hiding)
            {
                sRenderer.sprite = opened;
                sRendererOutline.sprite = openedOutline;
                sRenderer.color = opaque;
                sRendererOutline.color = opaque;
                playerTransform.position = spawnSpot.position;
                playerMovement.hiding = false;
            }
            else
            {
                sRenderer.sprite = closed;
                sRendererOutline.sprite = closedOutline;
                sRenderer.color = transparent;
                sRendererOutline.color = transparent;
                playerTransform.position = hideSpot.position;
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