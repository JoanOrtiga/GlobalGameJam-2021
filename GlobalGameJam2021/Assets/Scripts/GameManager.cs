using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<RestartableObject> restartables = new List<RestartableObject>();

    public bool paused;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                UnPause();
        }

        if (Input.GetKeyDown(KeyCode.R))
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
        foreach (var item in restartables)
        {
            item.Restart();
        }
    }
}
