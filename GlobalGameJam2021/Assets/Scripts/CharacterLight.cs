using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterLight : MonoBehaviour
{
    public Light2D playerLight;

    public float batteryTimer;
    public int batteryCounter;
    public int batteryCounterMax;

    public List <Battery> batteries;

    private void Start()
    {
        batteries = new List<Battery>();
    }

    private void Update()
    {
        if (batteryCounter > 0) LightOn();
        else LightOff();

        foreach (Battery b in batteries)
        {
            StartCoroutine("BatteryDecrease");
        }
    }

    public void AddLight(float time)
    {
        batteryCounter++;
        batteryTimer = time;
    }

    void LightOn()
    {
        playerLight.pointLightOuterRadius = 5;
    }

    void LightOff()
    {
        playerLight.pointLightOuterRadius = 1;
    }

    public IEnumerator BatteryDecrease()
    {
        yield return new WaitForSeconds(batteryTimer);
        batteryCounter--;
    }
}