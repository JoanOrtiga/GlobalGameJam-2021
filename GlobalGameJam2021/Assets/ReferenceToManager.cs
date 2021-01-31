using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceToManager : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.instance.UnPause);      
    }
}
