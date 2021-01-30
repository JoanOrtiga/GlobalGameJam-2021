using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : State<EnemyMachine>
{
    public static EnemyPatrolState Instance { get; private set; }

    static EnemyPatrolState()
    {
        Instance = new EnemyPatrolState();
    }

    public override void Enter(EnemyMachine entity)
    {
        entity.aiPath.maxSpeed = entity.patrolSpeed;

        if (!entity.staticPatrolling) MoveToNextPatrolPosition(entity);
    }

    public override void Execute(EnemyMachine entity)
    {
        if (!entity.staticPatrolling)
            Move(entity);

        if (entity.SeesPlayer())
        {
            entity.pStateMachine.ChangeState(EnemyChaseState.Instance);
        }
        else if (entity.HeardSomething())
        {
          /*  entity.CheckMaxPathLength();

            if (entity.pathLengthOk)
            {*/
                entity.pStateMachine.ChangeState(HearedSomethingState.Instance);
            //}
        }
    }

    public override void Exit(EnemyMachine entity)
    {

    }

    private void MoveToNextPatrolPosition(EnemyMachine entity)
    {
        entity.destinationSetter.target = entity.waypoints[entity.currentWaypointID];
    }

    private void UpdateWaypointID(EnemyMachine entity)
    {
        entity.currentWaypointID++;

        if (entity.currentWaypointID >= entity.waypoints.Count)
        {
            entity.currentWaypointID = 0;
        }
    }

    private void Move(EnemyMachine entity)
    {
        if (entity.aiPath.reachedEndOfPath)
        {
            if (!entity.untilPathOff)
            {
                entity.untilPathOff = true;

                UpdateWaypointID(entity);


                MoveToNextPatrolPosition(entity);
            }
        }
        else
        {
            entity.untilPathOff = false;
        }
    }
}
