using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : MonoBehaviour , RestartableObject
{
    private StateMachine<EnemyMachine> stateMachine;
    public StateMachine<EnemyMachine> pStateMachine
    {
        get { return stateMachine; }
    }

    //Restart Variables
    [HideInInspector] public Vector2 initPos { get; set; }
    [HideInInspector] public Quaternion initRot { get; set; }


     [HideInInspector] public float timer = 0.0f;


    [Header("PATROL")]
    public bool staticPatrolling;
    public List<Transform> waypoints = new List<Transform>();
    [HideInInspector] public int currentWaypointID = 0;
    [HideInInspector] public bool untilPathOff;



    //Extern Components;
    [HideInInspector] public Pathfinding.AIDestinationSetter destinationSetter;
    [HideInInspector] public Pathfinding.AIPath aiPath;

    public void InitRestart()
    {
        
    }

    public void Restart()
    {
        
    }


    private void Awake()
    {
        destinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        aiPath = GetComponent<Pathfinding.AIPath>();
    }


    private void Start()
    {
        InitRestart();




        stateMachine = new StateMachine<EnemyMachine>(this);
        stateMachine.ChangeState(EnemyPatrolState.Instance);
    }

    private void Update()
    {
        stateMachine.UpdateMachine();
    }

    
}
