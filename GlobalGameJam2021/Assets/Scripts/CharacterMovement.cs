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
    }

    private void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}