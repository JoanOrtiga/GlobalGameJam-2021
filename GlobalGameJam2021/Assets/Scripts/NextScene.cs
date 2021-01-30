using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public int sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneToLoad);      
    }
}
