    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip[] footsteps;
    private int currentIndex = 0;
    private float cooldown;
    [SerializeField] private float timeBetweenSteps;
    private float setted;
    private Rigidbody2D rb;
    private void Start()
    {
        cooldown = timeBetweenSteps;
        currentIndex = 0;
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            setted = timeBetweenSteps;
            timeBetweenSteps *= 2;
            source.volume /= 3;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            timeBetweenSteps = setted;
            source.volume *= 3;
        }
        PlayFootStepts();
    }
    private void PlayFootStepts()
    {
        if (((rb.velocity.x > 0.2f || rb.velocity.y > 0.2f) || (rb.velocity.x < -0.2f || rb.velocity.y < -0.2f)) && cooldown <= 0)
        {
            source.pitch = Random.Range(0.7f, 1.3f);
            cooldown = timeBetweenSteps;
            currentIndex = (currentIndex + 1) % footsteps.Length;
            source.PlayOneShot(footsteps[currentIndex]);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
