using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<RestartableObject> restartables = new List<RestartableObject>();

    public bool paused;

    public bool dying;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                UnPause();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void Die()
    {
        if (!dying)
            StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        dying = true;

        FindObjectOfType<FadeScript>().FadeIn();

        yield return new WaitForSeconds(0.8f);

        Time.timeScale = 0;

        foreach (var item in restartables)
        {
            item.Restart();
        }

        FindObjectOfType<FadeScript>().FadeOut();

        yield return new WaitForSecondsRealtime(0.25f);

        Time.timeScale = 1;

        dying = false;
    }

    public void NextScene(int sceneToLoad)
    {
        FindObjectOfType<FadeScript>().FadeIn();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        restartables.Clear();
        FindObjectOfType<FadeScript>().FadeOut();
    }
}
