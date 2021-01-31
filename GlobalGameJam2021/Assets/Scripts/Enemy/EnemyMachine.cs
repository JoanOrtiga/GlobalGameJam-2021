using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : MonoBehaviour, RestartableObject
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

    public float attackRange = 1f;


    [Header("HEARING")]
    public float hearNormalSteps = 10f;
    public float hearCrouchSteps = 4f;
    public float hearRandomSounds = 5f;
    public Transform lastHeardTransform;
    public float timeToReturn = 1f;

    [Header("SPEED")]
    public float patrolSpeed = 1;
    public float chaseSpeed = 1;
    public float heardSomethingSpeed = 1;

    [Header("CONTROL")]
    public float maxLengthPath = 20f;
    public GameObject questionMark;
    public GameObject exclamationMark;

    [Header("DEBUG")]
    public bool drawGizmos = false;

    //Extern Components;
    [HideInInspector] public Pathfinding.AIDestinationSetter destinationSetter;
    [HideInInspector] public Pathfinding.AIPath aiPath;
    [HideInInspector] public Pathfinding.Seeker seeker;

    [HideInInspector] public Transform player;
    [HideInInspector] public CharacterLight playerLight;
    [HideInInspector] public Rigidbody2D playerRB;
    [HideInInspector] public CharacterMovement playerMovement;

    [HideInInspector] public bool cMakingPathCheckLength;
    [HideInInspector] public bool pathLengthOk = false;

    public int pathCheckTimes = 0;
    public int maxPathCheckTimes = 3;
    public float cooldownTimerPathCheck = 3f;


    private bool playerLastHide;

    public void InitRestart()
    {
        GameManager.instance.restartables.Add(this);

        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void Restart()
    {
        currentWaypointID = 0;
        stateMachine.ChangeState(EnemyPatrolState.Instance);

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


        if (player != null & enemyEyes != null)
        {
            Gizmos.DrawLine(enemyEyes.position, enemyEyes.position + (player.position - enemyEyes.position));
        }

        Gizmos.DrawWireSphere(transform.position, hearNormalSteps);
        Gizmos.DrawWireSphere(transform.position, hearCrouchSteps);
        Gizmos.DrawWireSphere(transform.position, hearRandomSounds);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


    private void Awake()
    {
        destinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        aiPath = GetComponent<Pathfinding.AIPath>();
        seeker = GetComponent<Pathfinding.Seeker>();

        playerMovement = FindObjectOfType<CharacterMovement>();
        player = playerMovement.transform;
        playerLight = player.GetComponent<CharacterLight>();
        playerRB = player.GetComponent<Rigidbody2D>();
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

    private void LateUpdate()
    {
        playerLastHide = playerMovement.hiding;
    }


    public bool SeesPlayer()
    {
        Vector2 direction = player.position - enemyEyes.position;

        if (playerMovement.hiding && playerLastHide == playerMovement.hiding)
            return false;

        if (direction.magnitude < closeRange)
            return true;

        if ((direction.magnitude > coneRange && playerLight.isOn) || (direction.magnitude > coneRangeWhenLightOff && !playerLight.isOn))
            return false;

        bool isOnCone = Vector2.Angle(enemyEyes.up, direction.normalized) < coneAngle / 2f;

        if (isOnCone && !Physics.Linecast(enemyEyes.position, player.position, enemyMask.value))
        {
            return true;
        }

        return false;
    }

    public bool HeardSomething()
    {
        if (playerMovement.crouch)
        {
            return CheckMovingSound(hearCrouchSteps);
        }
        else
        {
            return CheckMovingSound(hearNormalSteps);
        }

        // return false;
    }

    public bool CheckMovingSound(float hearSteps)
    {
        if(pathCheckTimes >= maxPathCheckTimes)
        {
            if (!cMakingPathCheckLength)
            {
                StartCoroutine(CooldownPathLength());
            }

            return false;
        }

        Vector2 direction = player.position - enemyEyes.position;

        float magnitude = direction.magnitude;

        if (magnitude < hearSteps)
        {
            bool movingX = playerRB.velocity.x > 0.0001f || playerRB.velocity.x < -0.0001f;
            bool movingY = playerRB.velocity.y > 0.0001f || playerRB.velocity.y < -0.0001f;

            if (movingX || movingY)
            {
                lastHeardTransform.position = player.position;
                destinationSetter.target = lastHeardTransform;
                return true;
            }
        }

        return false;
    }

    public void HeardRandomSound(Vector2 position)
    {
        lastHeardTransform.position = position;

        stateMachine.ChangeState(HearedSomethingState.Instance);
    }

    public bool CheckMaxPathLength()
    {
        bool x = aiPath.GetPathLength() < maxLengthPath;

        if (!x)
            pathCheckTimes++;

        return x;
    }

    IEnumerator CooldownPathLength()
    {
        cMakingPathCheckLength = true;

        yield return new WaitForSeconds(cooldownTimerPathCheck);

        pathCheckTimes = 0;

        cMakingPathCheckLength = false;
    }
}
