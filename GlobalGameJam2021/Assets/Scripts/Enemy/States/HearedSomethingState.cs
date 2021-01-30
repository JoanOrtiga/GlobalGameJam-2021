using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearedSomethingState : State<EnemyMachine>
{
    public static HearedSomethingState Instance { get; private set; }

    static HearedSomethingState()
    {
        Instance = new HearedSomethingState();
    }

    public override void Enter(EnemyMachine entity)
    {
        // entity.destinationSetter.target = entity.lastHeardTransform;
        entity.timer = entity.timeToReturn;

        entity.aiPath.maxSpeed = entity.heardSomethingSpeed;

        
    }

    public override void Execute(EnemyMachine entity)
    {
        if (entity.aiPath.reachedEndOfPath)
        {
            entity.timer -= Time.deltaTime;

            if (entity.timer < 0)
            {
                entity.pStateMachine.ChangeState(EnemyPatrolState.Instance);
            }
        }

        if (entity.SeesPlayer())
        {
            entity.pStateMachine.ChangeState(EnemyChaseState.Instance);
        }

        if (entity.HeardSomething())
        {
            entity.timer = entity.timeToReturn;

        }

        if (!entity.CheckMaxPathLength())
        {
            entity.pStateMachine.ChangeState(EnemyPatrolState.Instance);
        }

        
    }

    public override void Exit(EnemyMachine entity)
    {

    }
}
