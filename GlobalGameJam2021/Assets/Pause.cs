using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            if (!pauseMenu.activeSelf)
                pauseMenu.SetActive(true);
        }
        else
        {
            if (pauseMenu.activeSelf)
                pauseMenu.SetActive(false);
        }
    }

    public void GoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void HidePause()
    {
        pauseMenu.SetActive(false);
    }
}
