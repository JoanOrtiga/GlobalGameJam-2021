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

    bool delay;

    private void Update()
    {
        if (GameManager.instance.paused)
            return;

        if (throwObject == null)
            return;

        if (!delay)
            return;

        RaycastHit2D raycast;

        if (Input.GetKeyDown(pickObjects))
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
            delay = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(pickObjects) && throwObject == null)
        {
            if (collision.CompareTag("PickeableObjects"))
            {
                collision.gameObject.SetActive(false);
                collision.GetComponent<Collider2D>().enabled = false;

                throwObject = collision.gameObject;

                StartCoroutine(CoolDown());
            }
        }
    }

    IEnumerator CoolDown()
    {
        delay = false;
        yield return new WaitForSeconds(0.1f);
        delay = true;
    }

    public void Reset()
    {
        if (throwObject != null)
        {
            throwObject.SetActive(true);

            throwObject = null;
        }

        delay = false;
    }
}