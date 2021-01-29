using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterLight : MonoBehaviour
{
    public Light2D playerLight;

    public bool isOn = true;

    public float batteryTimer;
    public float batteryTimerMax;

    public int batteryCounter;
    public int batteryCounterMax;

    public Image[] uiBatteries;
    public Sprite[] sprites;
    //0 - empty
    //1 - full

    private void Update()
    {
        UpdateBatteryUI();

        if (isOn && batteryTimer > 0)
        {
            batteryTimer -= Time.deltaTime;
            LightOn();
        }
        else
        {
            LightOff();
        }

        if (Input.GetKeyDown(KeyCode.E)) //Cambiar por GetButtonDown("CambiarLuz")
        {
            if (isOn) LightOff();
            else LightOn();
        }
    }

    public void AddLight()
    {
        batteryCounter = batteryCounterMax;
        batteryTimer = batteryTimerMax;
        LightOn();
    }

    void LightOn()
    {
        playerLight.pointLightOuterRadius = 5;
        isOn = true;
    }

    void LightOff()
    {
        playerLight.pointLightOuterRadius = 1;
        isOn = false;
    }

    public void UpdateBatteryUI()
    {
        float batteryPerc = 100 / batteryCounterMax;
        float timerPerc = batteryTimer * 100 / batteryTimerMax;

        for (int i = 0; i < uiBatteries.Length; i++)
        {
            if (i < timerPerc / batteryPerc)
            {
                uiBatteries[i].sprite = sprites[1];
                batteryCounter = i + 1;
            }
            else uiBatteries[i].sprite = sprites[0];
        }
    }
}