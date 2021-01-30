using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool checkpointActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !checkpointActive)
        {
            collision.GetComponent<RestartableObject>().InitRestart();
            checkpointActive = true;
            Debug.Log("Checkpoint");
        }
    }
}