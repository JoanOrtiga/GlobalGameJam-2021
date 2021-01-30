using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, RestartableObject
{
    public float speed, crouchSpeed, hideSpeed;
    public bool hiding = false;
    public bool crouch = false;

    private float GetSpeed
    {
        get
        {
            if (crouch) return crouchSpeed;
            else if (hiding) return hideSpeed;
            else return speed;
        }
    }

    public Transform cursor;
    private Vector3 direction;

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private RestartableObject restartableCam;
    private Vector3 movement;
    private Animator animator;
    [HideInInspector] public Rigidbody2D rb;

    private CharacterLight lightManager;

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        restartableCam = Camera.main.GetComponent<RestartableObject>();
        lightManager = GetComponent<CharacterLight>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        bool movingX = rb.velocity.x > 0.0001f || rb.velocity.x < -0.0001f;
        bool movingY = rb.velocity.y > 0.0001f || rb.velocity.y < -0.0001f;

        if (movingX || movingY) animator.SetBool("Walking", true);
        else animator.SetBool("Walking", false);

        if (Input.GetKeyDown(KeyCode.LeftShift)) crouch = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) crouch = false;

        direction = cursor.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if (hiding) lightManager.LightOff();
        //else lightManager.LightOn();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * GetSpeed;
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
        hiding = false;
        crouch = false;
    }
}