using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, RestartableObject
{
    public Transform target;
    private float posX;
    private float posY;
    private Vector3 velocity;
    public float smoothValor;
    public float shakeDuration;
    public float shakeAmount;
    Vector3 originalPos;

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        InitRestart();
    }

    private float RoundTwo(float num)
    {
        return num = Mathf.Round(num * 100) / 100;
    }

    void FixedUpdate()
    {
        if (target)
        {
            posX = Mathf.SmoothDamp(transform.position.x, RoundTwo(target.position.x), ref velocity.x, smoothValor);
            posY = Mathf.SmoothDamp(transform.position.y, RoundTwo(target.position.y), ref velocity.y, smoothValor);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }

        //TESTEO
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine("Shake");
        }
    }

    public IEnumerator Shake()
    {
        originalPos = transform.localPosition;
        for (float t = 0; t < shakeDuration; t+=Time.deltaTime)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            Debug.Log("Shake");
            yield return null;
        }
    }

    public void InitRestart()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void Restart()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }
}