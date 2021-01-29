using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    public Vector2 finalPos;

    private float t = 5f;

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, finalPos, t * Time.deltaTime);
    }
}
