using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour , RestartableObject
{
    public Vector2 finalPos;

    private float t = 5f;

    public float margin = 0.3f;

    bool thrown = false;

    public float maxHearRange = 15f;

    public LayerMask enemyLayer;

    public bool drawGizmos;

    Collider2D colliderThrown;

    public Vector3 initPos { get; set; }
    public Quaternion initRot { get; set; }

    private void Awake()
    {
        colliderThrown = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameManager.instance.restartables.Add(this);
        InitRestart();

        this.enabled = false;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, finalPos, t * Time.deltaTime);

        if (((Vector2)transform.position - finalPos).magnitude < margin)
        {
            GetComponent<AudioSource>().Play();
            foreach (Collider2D item in Physics2D.OverlapCircleAll(transform.position, maxHearRange, enemyLayer))
            {
                EnemyMachine em = item.GetComponent<EnemyMachine>();

                if ((em.transform.position - transform.position).magnitude < em.hearRandomSounds)
                {
                    print(em);
                    em.HeardRandomSound(transform.position);

                }
            }

            colliderThrown.enabled = true;
            this.enabled = false;

        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Gizmos.DrawWireSphere(transform.position, maxHearRange);
    }

    public void InitRestart()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void Restart()
    {
        gameObject.SetActive(true);

        transform.position = initPos;
        transform.rotation = initRot;

        colliderThrown.enabled = true;
        this.enabled = false;
    }
}
