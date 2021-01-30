using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    public Color unselectedCol;
    public Color selectedCol;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        text.color = selectedCol;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = unselectedCol;
    }
}
