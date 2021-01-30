using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, RestartableObject
{
    public float speed, crouchSpeed;
    public bool canMove = true;
    public bool hiding = false;
    public bool crouch = false;

    private float GetSpeed
    {
        get
        {
            if (crouch) return crouchSpeed;
            else return speed;
        }
    }

    public Transform aim;
    public Transform cursor;
    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private RestartableObject restartableCam;
    private Vector3 movement;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        restartableCam = Camera.main.GetComponent<RestartableObject>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (Input.GetKeyDown(KeyCode.LeftShift)) crouch = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) crouch = false;
    }

    private void FixedUpdate()
    {
        if (canMove) rb.velocity = movement * GetSpeed;
    }

    public void InitRestart()
    {
        restartableCam.InitRestart();
        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void Restart()
    {
        restartableCam.Restart();
        transform.position = initPos;
        transform.rotation = initRot;
    }
}