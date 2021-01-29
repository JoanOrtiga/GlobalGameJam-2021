using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cartel : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private Text textObject;
    [SerializeField] private Animator canvas;
    private bool input = false;
    private bool isIn = false;
    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.E))
        {
            input = !input;
            canvas.SetBool("Show", input);
            textObject.text = text;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
            input = false;
            canvas.SetBool("Show", input);
        }
    }
}
