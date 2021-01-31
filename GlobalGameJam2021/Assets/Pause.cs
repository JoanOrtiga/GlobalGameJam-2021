using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private FadeScript fade;
    private CursorHide cursor;

    private void Start()
    {
        cursor = GetComponent<CursorHide>();
        fade = FindObjectOfType<FadeScript>();
    }

    void Update()
    {
        if (GameManager.instance.paused)
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                cursor.ShowCursor();
            } 
        }
        else
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                cursor.HideCursor();
            }
        }
    }

    public void GoMenu()
    {
        fade.FadeIn();
        StartCoroutine(BackToMenu());
        cursor.ShowCursor();
    }

    public void HidePause()
    {
        GameManager.instance.UnPause();
        pauseMenu.SetActive(false);
        cursor.HideCursor();
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.UnPause();
        SceneManager.LoadScene(0);
    }
}
