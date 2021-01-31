using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDosCanvas : MonoBehaviour
{
    public TutorialDos item;
    public GameObject tutorialCanvas;
    public bool tutorialActive = false;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (item.pillado && !tutorialActive)
        {
            tutorialCanvas.SetActive(true);
            tutorialActive = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && tutorialActive)
        {
            tutorialCanvas.SetActive(false);
        }
    }
}