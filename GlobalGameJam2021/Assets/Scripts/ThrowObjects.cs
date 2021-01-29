using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjects : MonoBehaviour
{

    public GameObject throwObject;
    public LayerMask throwMask;

    private void Update()
    {
        float range = 10f;
        RaycastHit2D raycast;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 toMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            raycast = Physics2D.Raycast(transform.position, toMouse, range, throwMask.value);

            GameObject throwing = Instantiate(throwObject, transform.position, Quaternion.identity);

            if (raycast)
            {
                print("hola");
                throwing.GetComponent<ThrownObject>().finalPos = raycast.point;
            }
            else
            {
                throwing.GetComponent<ThrownObject>().finalPos = toMouse * range;
            }
        }
    }
}
