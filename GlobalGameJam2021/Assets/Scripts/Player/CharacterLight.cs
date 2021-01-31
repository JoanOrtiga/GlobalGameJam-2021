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

    public int batteryRecharge;

    public Image[] uiBatteries;
    public Text uiCounter;

    public GameObject particles;

    public bool test = false;

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        if (test)
            batteryTimer = 10;
        else
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
                case true: LightOff(); break;
                case false: LightOn(); break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && batteryRecharge > 0)
        {
            batteryRecharge--;
            Restart();
        }
    }

    public void AddLight()
    {
        batteryRecharge++;
    }

    public void LightOn()
    {
        playerLight.enabled = true;
        isOn = true;
        particles.SetActive(true);
    }

    public void LightOff()
    {
        playerLight.enabled = false;
        isOn = false;
        particles.SetActive(false);
    }

    public void UpdateBatteryUI()
    {
        uiCounter.text = "x" + batteryRecharge;

        float batteryPerc = 100 / batteryCounterMax;
        float timerPerc = batteryTimer * 100 / batteryTimerMax;

        for (int i = 0; i < uiBatteries.Length; i++)
        {
            if (i < timerPerc / batteryPerc)
            {
                uiBatteries[i].enabled = true;
                batteryCounter = i + 1;
            }
            else uiBatteries[i].enabled = false;
        }
    }

    public void InitRestart()
    {

    }

    //Reinicia amb la llum al maxim
    public void Restart()
    {
        if (batteryCounter <= 1)
        {
            batteryCounter = 2;
        }

        batteryTimer = batteryTimerMax;
        LightOff();
    }
}