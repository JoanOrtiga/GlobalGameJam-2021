using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<RestartableObject> restartables = new List<RestartableObject>();

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
        //Esto se tiene que quitar al final
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (RestartableObject restartable in restartables)
            {
                restartable.Restart();
            }
        }
    }
}
