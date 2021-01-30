using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escondite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer outline;
    public Color transparent;
    public Color opaque;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        outline = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(outline.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is here");
            sprite.color = transparent;
            outline.color = transparent;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is not here");
            sprite.color = opaque;
            outline.color = opaque;
        }
    }
}
