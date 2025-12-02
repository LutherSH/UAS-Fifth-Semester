using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyMelee : MonoBehaviour, ISpookable
{
    private TheStateMelee currentState;
    public Collider hitCollider;
    public GameObject hitBoxParent;
    public NavMeshAgent nAgent;
    public Transform player;
    public LayerMask theGround, thePlayer;
    public bool isSpooked = false;
    public Animator eMAnimator;

    ///////////////////////////////////////////////////////////////////////
    ///// Property For Patrol

    [Header("Patrol Settings")]
    public Vector3 walkPoint;
    public bool setAWalkPoint;
    public float rangeOfWalkpoint;
    public Vector3 distanceToWalkPoint;

    ///////////////////////////////////////////////////////////////////////
    //// Property For Idle

    [Header("Idle Settings")] 
    public float idleDuration = 5f;

    ///////////////////////////////////////////////////////////////////////
    ///// Property For Chase

    [Header("Chase Settings")]
    public bool playerInSightRange;
    public float sightRange;

    ///////////////////////////////////////////////////////////////////////
    //// Property For Attack

    [Header("Attack Settings")]
    public bool playerInAttackRange;
    public Collider weaponCollider;
    public bool triggerHitboxAttacking = false;
    public float attackRange;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    public float nextAttackTime = 0f;
    [HideInInspector]public float nextFireTime = 0f;

    ///////////////////////////////////////////////////////////////////////
    //// Property For Vision

    [Header("Vision Settings")]
    private float visionRange = 10f;
    public float fov = 90f;
    public float defaultFov;
    public LayerMask theWall;
    
    ///////////////////////////////////////////////////////////////////////
    //// Property For Sounds

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip shootClip;
    ///////////////////////////////////////////////////////////////////////
    /// START

    private void Awake()
    {
        player = GameObject.Find("PlayerTrue").transform;
        nAgent = GetComponent<NavMeshAgent>();
        hitCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        //eMAnimator = GetComponent<Animator>();

        weaponCollider.enabled = false;
    }

    void Start()
    {
        SwitchState(new IdleStateMelee(this)); // IDLE
        visionRange = sightRange;
        defaultFov = fov;
        triggerHitboxAttacking = false;
    }
    ///////////////////////////////////////////////////////////////////////
    /// UPDATE
    void Update()
    {
        //if (playerBehaviour.isDead) return;

        // Attack check (still sphere-based)
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);

        // Vision check (cone FOV)
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Is player within sight radius?
        if (distanceToPlayer <= sightRange)
        {
            // Is player within FOV cone?
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fov / 2f)
            {
                // Raycast to check for walls
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, theWall))
                {
                    playerInSightRange = true;   // ✅ Player is visible
                }
                else
                {
                    playerInSightRange = false;  // ❌ Blocked by wall
                }
            }
            else
            {
                playerInSightRange = false;      // ❌ Outside cone
            }
        }

        else
        {
            playerInSightRange = false;          // ❌ Too far away
            playerInAttackRange = false;
        }

        // STATE UPDATER
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void SearchWalkPoint()
    {
        float randomizedZ = Random.Range(-rangeOfWalkpoint, rangeOfWalkpoint);
        float randomizedX = Random.Range(-rangeOfWalkpoint, rangeOfWalkpoint);

        walkPoint = new Vector3(transform.position.x + randomizedX, transform.position.y, transform.position.z + randomizedZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, theGround))
        {
            setAWalkPoint = true;
        }
    }
    ///////////////////////////////////////////////////////////////////////
    /// STATE SWITCHER
    public void SwitchState(TheStateMelee newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void PermaDeath()
    {
        currentState.Exit();
        hitCollider.enabled = false;
        hitBoxParent.SetActive(false);
        SwitchState(new DeadStateMelee(this));
        return;
    }

    public void Spooked()
    {
        fov = 720f;

        if (!playerInSightRange)
        {
            isSpooked = true;
        }
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    ///////////////////////////////////////////////////////////////////////
    /// ANIMATOR CALL
    public void StartAttackWindow()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = true;
            PlayAttackSound();
    }

    public void EndAttackWindow()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(shootClip);
    }
    ///////////////////////////////////////////////////////////////////////
    /// DRAW GIZMIOZ
    void OnDrawGizmosSelected()
    {
        // Draw sight radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        // Draw patrol range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeOfWalkpoint);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw FOV lines
        Vector3 fovLine1 = Quaternion.Euler(0, fov / 2, 0) * transform.forward * sightRange;
        Vector3 fovLine2 = Quaternion.Euler(0, -fov / 2, 0) * transform.forward * sightRange;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}