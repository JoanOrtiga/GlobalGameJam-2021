using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundForEnemy : MonoBehaviour
{
    public float maxHearRange = 15f;

    public LayerMask enemyLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Collider2D item in Physics2D.OverlapCircleAll(transform.position, maxHearRange, enemyLayer))
            {
                EnemyMachine em = item.GetComponent<EnemyMachine>();

                if ((em.transform.position - transform.position).magnitude < em.hearRandomSounds)
                {
                    em.HeardRandomSound(transform.position);
                }
            }
        }
    }
}
