using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private FadeScript fade;

    private void Start()
    {
        fade = FindObjectOfType<FadeScript>();
    }

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
        fade.FadeIn();
        StartCoroutine(BackToMenu());
    }

    public void HidePause()
    {
        GameManager.instance.UnPause();
        pauseMenu.SetActive(false);
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.UnPause();
        SceneManager.LoadScene(0);
    }
}
