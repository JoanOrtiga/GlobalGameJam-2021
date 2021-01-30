using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjects : MonoBehaviour
{

    public GameObject throwObject;
    public LayerMask throwMask;
    public Transform mouse;

    public float range = 10f;

    public KeyCode pickObjects;

    private void Update()
    {
        if (GameManager.instance.paused)
            return;

        if (throwObject == null)
            return;

        RaycastHit2D raycast;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 toMouse = (mouse.position - transform.position).normalized;

            raycast = Physics2D.Raycast(transform.position, toMouse, range, throwMask.value);

            throwObject.SetActive(true);
            throwObject.transform.position = transform.position;

            if (raycast)
            {
                throwObject.GetComponent<ThrownObject>().finalPos = raycast.point;
            }
            else
            {
                throwObject.GetComponent<ThrownObject>().finalPos = toMouse * range + (Vector2)transform.position;
            }

            throwObject.GetComponent<ThrownObject>().enabled = true;

            throwObject = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(pickObjects))
        {
            if (collision.CompareTag("PickeableObjects"))
            {
                collision.gameObject.SetActive(false);
                collision.GetComponent<Collider2D>().enabled = false;
                throwObject = collision.gameObject;
            }
        }
    }
}