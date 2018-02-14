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

    // Player //
    public PlayerManager player;
    public Transform playerTransform;
    private Vector3 playerPos;

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(playerTransform);
        // Getting Player Position.
        playerPos = player.GetComponent<PlayerManager>().transform.position;

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

        // If Enemy State is CHASE
        if(skeletonState == EnemySkeletonState.CHASE)
        {
            // Enemy will walk to Player Position smoothly.
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref smoothVelocity, 10f);
        }

        AnimationUpdate();
	}

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
