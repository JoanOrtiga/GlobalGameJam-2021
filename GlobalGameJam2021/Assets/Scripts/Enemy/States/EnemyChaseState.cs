using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State<EnemyMachine>
{
    public static EnemyChaseState Instance { get; private set; }

    static EnemyChaseState()
    {
        Instance = new EnemyChaseState();
    }
    public override void Enter(EnemyMachine entity)
    {
        entity.timer = 0.0f;

        entity.destinationSetter.target = entity.player.transform;

        entity.aiPath.maxSpeed = entity.chaseSpeed;

        entity.timer = entity.maxTimeWithoutSeeing;
    }

    public override void Execute(EnemyMachine entity)
    {
        if (!entity.SeesPlayer())
        {
            if (entity.HeardSomething())
            {
                /*  entity.CheckMaxPathLength();

                  if (entity.pathLengthOk)*/
                entity.pStateMachine.ChangeState(HearedSomethingState.Instance);
            }

            entity.timer -= Time.deltaTime;

            if (entity.timer <= 0)
            {
                entity.pStateMachine.ChangeState(EnemyPatrolState.Instance);
            }
        }
        else
        {
            entity.timer = entity.maxTimeWithoutSeeing;
        }

        CheckDeath(entity);
    }



    public override void Exit(EnemyMachine entity)
    {

    }

    public void CheckDeath(EnemyMachine entity)
    {
        if ((entity.player.position - entity.transform.position).magnitude < entity.attackRange)
            GameManager.instance.Die();
    }
}
