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
    [HideInInspector] public Vector3 initPos { get; set; }
    [HideInInspector] public Quaternion initRot { get; set; }


     [HideInInspector] public float timer = 0.0f;


    [Header("PATROL")]
    public bool staticPatrolling;
    public List<Transform> waypoints = new List<Transform>();
    [HideInInspector] public int currentWaypointID = 0;
    [HideInInspector] public bool untilPathOff;

    [Header("SEE PLAYER")]
    public float coneAngle = 90;
    public float coneRange = 3f;
    public float coneRangeWhenLightOff = 1.5f;
    public LayerMask enemyMask;
    public Transform enemyEyes;
    /// <summary>
    /// Temps des de que deix de veure el jugador per deixar de perseguir.
    /// </summary>
    [Tooltip("Temps des de que deix de veure el jugador per deixar de perseguir.")] public float maxTimeWithoutSeeing;
    [Tooltip("Distancia en la qual el enemic et veurà si o si")] public float closeRange;



    [Header("DEBUG")]
    public bool drawGizmos = false;

    //Extern Components;
    [HideInInspector] public Pathfinding.AIDestinationSetter destinationSetter;
    [HideInInspector] public Pathfinding.AIPath aiPath;

    [HideInInspector] public Transform player;
    [HideInInspector] public CharacterLight playerLight;
    
    

    public void InitRestart()
    {


        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void Restart()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }


    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        float halfFOV = coneAngle / 2f;
        float coneDirection = 90;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * transform.right * coneRange;
        Vector3 downRayDirection = downRayRotation * transform.right * coneRange;

        Gizmos.DrawRay(transform.position, upRayDirection);
        Gizmos.DrawRay(transform.position, downRayDirection);
        Gizmos.DrawLine(transform.position + downRayDirection, transform.position + upRayDirection);


        if(player != null & enemyEyes != null)
        {
            Gizmos.DrawLine(enemyEyes.position, enemyEyes.position + (player.position - enemyEyes.position).normalized * 5);
            Gizmos.DrawLine(enemyEyes.transform.position, enemyEyes.transform.position + transform.up * 5);
        }
        
    }


    private void Awake()
    {
        destinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        aiPath = GetComponent<Pathfinding.AIPath>();

        player = FindObjectOfType<CharacterMovement>().transform;
        playerLight = FindObjectOfType<CharacterLight>();

     
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


    public bool SeesPlayer()
    {
        Vector2 direction = player.position - enemyEyes.position;

        if (direction.magnitude < closeRange)
            return true;

        if((direction.magnitude > coneRange && playerLight.isOn) || (direction.magnitude > coneRangeWhenLightOff && !playerLight.isOn))
            return false;

        bool isOnCone = Vector2.Angle(enemyEyes.up, direction.normalized) < coneAngle/2f;

        if (isOnCone && !Physics.Linecast(enemyEyes.position, player.position, enemyMask.value))
        {
            return true;
        }

        return false;
    }

    //public bool HeardSomething()
    //{
    //    Vector2 direction = player.position - enemyEyes.position;
    //}
}
