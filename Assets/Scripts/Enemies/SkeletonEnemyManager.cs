using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Skeleton Enemy
public class SkeletonEnemyManager : MonoBehaviour, StatsBase
{
    enum EnemySkeletonState
    {
        IDLE,
        WALK,
        CHASE,
        ATTACK,
        DIE,
    }

    // Enemy //
    EnemySkeletonState skeletonState;
    public Animation anim;

    // Vector 3 to store the velocity of the enemy.
    private Vector3 smoothVelocity = Vector3.zero;
    private float minDistanceApart = 1.5f;
    private float chaseDistanceRange = 15f;
    private float distanceApart;
    public float chasingTimer = 4f;

    // Player //
    public PlayerManager player;
    public Transform playerTransform;
    private Vector3 playerPos;
    private Vector3 distanceOffset;
    private Vector3 PlayerDestination;

    // Stats //
    [SerializeField]
    int enemyLevel = 0, health = 10;
    float attack = 10, defense = 10, movespeed = 10;

    // Stats Setter and Getter //
    public int Level
    {
        get
        {
            return enemyLevel;
        }

        set
        {
            enemyLevel = value;
        }
    }

    public string Name
    {
        get
        {
            return "EnemySkeleton";
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return defense;
        }

        set
        {
            defense = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return movespeed;
        }

        set
        {
            movespeed = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        // Setting Skeleton Initial State as IDLE.
        skeletonState = EnemySkeletonState.IDLE;
        anim = GetComponent<Animation>();

        // Setting an Offset.
        distanceOffset.Set(1f, 0f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(playerTransform);
        // Getting Player Position.
        playerPos = player.GetComponent<PlayerManager>().transform.position;
        PlayerDestination = playerPos - distanceOffset;

        if (skeletonState == EnemySkeletonState.IDLE)
        {
            anim.Play("Idle");
        }

        // Changing Enemy States Manually //
        if(Input.GetKey(KeyCode.Alpha1))
        {
            // Change State to Idle
            skeletonState = EnemySkeletonState.IDLE;
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            // Change State to Walk
            skeletonState = EnemySkeletonState.WALK;
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            // Change State to Chase
            skeletonState = EnemySkeletonState.CHASE;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            // Change State to Attack
            skeletonState = EnemySkeletonState.ATTACK;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            // Change State to Death
            skeletonState = EnemySkeletonState.DIE;
        }
        // END //

        // Enemy States
        switch (skeletonState)
        {
            case EnemySkeletonState.IDLE:
                SkeletonIdle(PlayerDestination);
                break;

            case EnemySkeletonState.WALK:
                break;

            case EnemySkeletonState.ATTACK:
                SkeletonAttack(PlayerDestination);
                break;

            case EnemySkeletonState.CHASE:
                SkeletonChase(PlayerDestination, playerPos, distanceOffset);
                break;

            case EnemySkeletonState.DIE:
                StartCoroutine(SkeletonStateToDie());
                break;
        }

        AnimationUpdate();
	}

    // Enemy Idle
    private void SkeletonIdle(Vector3 _playerDesti)
    {
        // Distance between Player and Enemy.
        distanceApart = Vector3.Distance(transform.position, _playerDesti);

        // If Player is within Enemy Chase Range, then change State to CHASE.
        if(distanceApart < chaseDistanceRange)
        {
            skeletonState = EnemySkeletonState.CHASE;
        }
    }

    // Enemy Chase
    private void SkeletonChase(Vector3 _playerDesti, Vector3 _playerPos, Vector3 _distOffset)
    {
        // Enemy will walk to Player Position smoothly.
        transform.position = Vector3.SmoothDamp(transform.position, _playerPos - _distOffset, ref smoothVelocity, chasingTimer);

        // Distance between Player and Enemy.
        distanceApart = Vector3.Distance(transform.position, _playerDesti);

        // If Enemy has reached the destination, change State to Attack.
        if (distanceApart < minDistanceApart)
        {
            skeletonState = EnemySkeletonState.ATTACK;
        }

        // If Player is outside of Enemy Chase Range, then change State to IDLE.
        if(distanceApart > chaseDistanceRange)
        {
            skeletonState = EnemySkeletonState.IDLE;
        }
    }

    // Enemy Attack
    private void SkeletonAttack(Vector3 _playerDesti)
    {
        // Distance between Player and Enemy.
        distanceApart = Vector3.Distance(transform.position, _playerDesti);

        // Minus Player Health (?)

        // If Player has moved and its not within range for Enemy to Attack, change State to Chase.
        if (distanceApart > minDistanceApart)
        {
            // Start Parallel action.
            float _delayTime = anim.GetClip("Attack").length;
            StartCoroutine(SkeletonStateToChase(_delayTime));
        }
    }

    // When Skeleton Die, wait for animation to be done playing before destroying it.
    IEnumerator SkeletonStateToDie()
    {
        float _delayTime = anim.GetClip("Death").length;
        yield return new WaitForSeconds(_delayTime);

        Destroy(gameObject);
    }
    // When Player moved out of Enemy attack range, play finish animation before chasing.
    IEnumerator SkeletonStateToChase(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        // Change State to Chase.
        skeletonState = EnemySkeletonState.CHASE;
    }

    // Animation Changes based on States.
    private void AnimationUpdate()
    {
        switch(skeletonState)
        {
            case EnemySkeletonState.IDLE:
                anim.Play("Idle");
                break;

            case EnemySkeletonState.WALK:
                anim.Play("Walk");
                break;

            case EnemySkeletonState.ATTACK:
                anim.Play("Attack");
                break;

            case EnemySkeletonState.CHASE:
                anim.Play("Run");
                break;

            case EnemySkeletonState.DIE:
                anim.Play("Death");
                break;
        }
    }
}
