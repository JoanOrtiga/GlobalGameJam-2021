    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public float footsteptsSpeed;
    private bool isColliding, isMoving;
    private AudioSource source;
    [SerializeField] private AudioClip[] footsteps;
    private int currentIndex = 0;
    private float cooldown;
    [SerializeField] private float timeBetweenSteps;
    private void Start()
    {
        cooldown = timeBetweenSteps;
        currentIndex = 0;
        source = GetComponent<AudioSource>(); 
    }
    private void FixedUpdate()
    {
        PlayFootStepts();
    }
    private void PlayFootStepts()
    {
        if (isMoving && !isColliding && cooldown <0)
        {
            cooldown = timeBetweenSteps;
            currentIndex = (currentIndex + 1) % footsteps.Length;
            source.clip = footsteps[currentIndex];
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
