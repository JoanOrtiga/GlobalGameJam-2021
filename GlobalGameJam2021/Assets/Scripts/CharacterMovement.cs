using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    private Vector3 movement;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Movimiento
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //Animaciones
        //animator.SetFloat("MoveX", movement.x);
        //animator.SetFloat("MoveY", movement.y);
        //animator.SetFloat("Magnitude", movement.magnitude);

        ThrowObjects();
    }

    private void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }

    public GameObject throwObject;
    public LayerMask throwMask;

    private void ThrowObjects()
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