using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float time;
    private SpriteRenderer s;
    private float delta;
    void Start()
    {
        delta = time;
        Destroy(gameObject, time);
        s = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        delta -= Time.deltaTime;
        if (delta < 1)
        {
            s.color = new Color(0, 0, 0, delta);
        }
    }
}
