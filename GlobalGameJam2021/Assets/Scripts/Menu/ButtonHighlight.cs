using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public Color unselectedCol;
    public Color selectedCol;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        image.color = selectedCol;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
        image.color = unselectedCol;
    }
}