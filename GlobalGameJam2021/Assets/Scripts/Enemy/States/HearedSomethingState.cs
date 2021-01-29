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
       
    }

    public override void Execute(EnemyMachine entity)
    {
        
    }

    public override void Exit(EnemyMachine entity)
    {

    }
}
