using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour , RestartableObject
{
    public bool playerArround = false;
    private CharacterLight character;

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerArround)
        {
            character.AddLight();
            gameObject.SetActive(false);
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

    private void Start()
    {
        InitRestart();
    }

    public void InitRestart()
    {
        GameManager.instance.restartables.Add(this);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
    }
}
