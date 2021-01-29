using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, RestartableObject
{
    public float speed, crouchSpeed;
    private bool crouch = false;
    private float GetSpeed
    {
        get
        {
            if (crouch)
            {
                return crouchSpeed;
            }
            else
            {
                return speed;
            }
        }
    }

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private Vector3 movement;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Movimiento
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //Animaciones
        //animator.SetFloat("MoveX", movement.x);
        //animator.SetFloat("MoveY", movement.y);
        //animator.SetFloat("Magnitude", movement.magnitude);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * GetSpeed;
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