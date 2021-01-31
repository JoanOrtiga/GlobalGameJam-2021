using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int index = 0;
    [SerializeField] private GameObject[] elements;
    private bool hint = false;
    public InGameMenu book;

    void Start()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetActive(false);
        }
    }
    void Update()
    {
        if (index == 0) //jump
        {
            if (book.bookOpened == false)
            {
                elements[index].SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    elements[index].SetActive(false);
                    index++;
                }
            }
        }
        else if (index == 1)
        {
            elements[index].SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                elements[index].SetActive(false);
                index++;
            }
        }
        else if (index == 2 && hint)
        {
            elements[index].SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                elements[index].SetActive(false);
                index++;
            }
        }
        else if (index == 3)
        {
            elements[index].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                elements[index].SetActive(false);
                index++;
            }
        }
    }
    public void Start2()
    {
        hint = true;
    }
}
