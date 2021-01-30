using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterLight : MonoBehaviour, RestartableObject
{
    [HideInInspector] public Vector3 initPos { get; set; }
    [HideInInspector] public Quaternion initRot { get; set; }

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

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        batteryTimer = batteryTimerMax;
        LightOff();
        isOn = false;
    }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (isOn)
            {
                case true:
                    Debug.Log("OFFFFF");
                    LightOff();
                    break;
                case false:
                    Debug.Log("ONN");
                    LightOn();
                    break;
            }
        }
    }

    public void AddLight()
    {
        batteryCounter = batteryCounterMax;
        batteryTimer = batteryTimerMax;
        LightOn();
    }

    public void LightOn()
    {
        playerLight.enabled = true;
        isOn = true;
    }

    public void LightOff()
    {
        playerLight.enabled = false;
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

    public void InitRestart()
    {

    }

    //Reinicia amb la llum al maxim
    public void Restart()
    {
        AddLight();
    }
}