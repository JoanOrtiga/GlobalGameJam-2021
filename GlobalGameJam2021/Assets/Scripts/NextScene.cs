using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private CharacterInventory inv;
    public int sceneToLoad;
    private void Start()
    {
        inv = FindObjectOfType<CharacterInventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && inv.bookCounter == SceneManager.GetActiveScene().buildIndex)
        {
            GameManager.instance.NextScene(sceneToLoad);
        }

    }
}
