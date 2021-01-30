using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Escondite : MonoBehaviour , RestartableObject
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

    BoxCollider2D colliderBox;

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private void Start()
    {
        InitRestart();

        sRenderer = GetComponent<SpriteRenderer>();
        sRendererOutline = transform.GetChild(0).GetComponent<SpriteRenderer>();

        colliderBox = GetComponents<BoxCollider2D>()[1];


        playerMovement = FindObjectOfType<CharacterMovement>();
        playerTransform = playerMovement.transform;
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
                colliderBox.enabled = true;
                GetComponent<ShadowCaster2D>().enabled = true;
                playerMovement.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                sRenderer.sprite = closed;
                sRendererOutline.sprite = closedOutline;
                sRenderer.color = transparent;
                sRendererOutline.color = transparent;
                playerTransform.position = hideSpot.position;
                playerMovement.hiding = true;
                colliderBox.enabled = false;
                GetComponent<ShadowCaster2D>().enabled = false;
                playerMovement.rb.constraints = RigidbodyConstraints2D.FreezeAll;

            }
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

    public void InitRestart()
    {
        GameManager.instance.restartables.Add(this);
    }

    public void Restart()
    {
        if (playerMovement.hiding)
        {
            sRenderer.sprite = opened;
            sRendererOutline.sprite = openedOutline;
            sRenderer.color = opaque;
            sRendererOutline.color = opaque;
            playerTransform.position = spawnSpot.position;
            playerMovement.hiding = false;
            colliderBox.enabled = true;
            playerMovement.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
