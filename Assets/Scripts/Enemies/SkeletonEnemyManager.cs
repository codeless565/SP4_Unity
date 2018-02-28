using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Skeleton Enemy
public class SkeletonEnemyManager : MonoBehaviour, StatsBase
{
    enum EnemySkeletonState
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DIE,
    }

    // Stats //
    [SerializeField]
    int enemyLevel = 1;

    float health;
    float maxhealth;
    float stamina;
    float maxStamina;
    float attack;
    float defense;

    [SerializeField]
    float movespeed = 10;

    LevelingSystem levelingSystem;

    // EXP Reward for kill
    public float EXPRewardScaling = 5;
    private float expReward = 1;

    // Waypoint
    private Vector3[] m_Waypoint;
    private int m_currWaypointID;

    // Enemy //
    EnemySkeletonState skeletonState;
    private Vector2 currentVelocity = Vector2.zero;
    private Vector3 enemyDestination;
    private float distanceApart;
    private float enemyChaseRange;
    private float enemyAttackRange;
    private float enemyAttackTimer;
    private float enemyToPatrolTimer;
    private bool canCountDownAttackTimer;
    private bool canAttack;
    private bool canCountDownPatrolTimer;
    private bool canPatrol;

    public float chasingTimer;
    public float maxSpeed;

    // Player //
    private GameObject player;
    private Player2D_StatsHolder playerStats;

    SpriteManager e_spriteManager;

    // Stats Setter and Getter //
    public string Name
    {
        get
        {
            return "Enemy";
        }

        set
        {
            return;
        }
    }

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

    public float EXP
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }

    public float MaxEXP
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }

    public float Health
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

    public float MaxHealth
    {
        get
        {
            return maxhealth;
        }

        set
        {
            maxhealth = value;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public float MaxStamina
    {
        get
        {
            return maxStamina;
        }

        set
        {
            maxStamina = value;
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
    
    public float EXPReward
    {
        set { expReward = value; }
    }

    public Vector3[] Waypoint
    {
        set { m_Waypoint = value; }
    }

    public int CurrWaypointID
    {
        set { m_currWaypointID = value; }
    }

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();

        e_spriteManager = GetComponent<SpriteManager>();
        // set default equipments(will be moved to savefile)
        e_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_NULL);
        e_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_NULL);
        e_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_NULL);
        e_spriteManager.SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_NULL);
        e_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.DAGGER);

        chasingTimer = 4f;
        maxSpeed = 5f;

        // Setting Skeleton Initial State as IDLE.
        skeletonState = EnemySkeletonState.IDLE;
        enemyToPatrolTimer = 5f;
        canCountDownPatrolTimer = false;
        canPatrol = false;
        // Setting the range for Enemy State to be CHASE.
        enemyChaseRange = 5f;
        // Setting the range for Enemy State to be ATTACk.
        enemyAttackRange = 1f;
        enemyAttackTimer = 1f;
        canCountDownAttackTimer = false;
        canAttack = false;

        //Initialize Stats from the leveling system
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, false);
    }
	
	// Update is called once per frame
	void Update ()
    {   
        // Update every 10 frames
        if(Time.frameCount % 10 == 0)
        {
            // Getting Player Position.
            enemyDestination = player.transform.position;
        }

        // Enemy Attack Timer
        if(canCountDownAttackTimer)
        {
            // Start counting down.
            enemyAttackTimer -= Time.deltaTime;
            if(enemyAttackTimer <= 0f)
            {
                canAttack = true;
            }
        }
        
        // Enemy Going To Patrol Timer
        if(canCountDownPatrolTimer)
        {
            // Start counting down.
            enemyToPatrolTimer -= Time.deltaTime;
            if(enemyToPatrolTimer <= 0f)
            {
                canPatrol = true;
            }
        }

        //Enemy States
        switch (skeletonState)
        {
            case EnemySkeletonState.IDLE:
                SkeletonIdle(enemyDestination);
                break;

            case EnemySkeletonState.PATROL:
                SkeletonPatrol(enemyDestination);
                break;

            case EnemySkeletonState.ATTACK:
                SkeletonAttack(enemyDestination);
                break;

            case EnemySkeletonState.CHASE:
                SkeletonChase(enemyDestination);
                break;

            case EnemySkeletonState.DIE:
                StartCoroutine(SkeletonStateToDie());                
                break;
        }

        PlayerHitEnemy();
    }

    // Enemy Idle
    private void SkeletonIdle(Vector3 _enemyDesti)
    {
        // Set Enemey Walk to FALSE
        e_spriteManager.SetMoving(false);
        // Distance between Player and Enemy.
        distanceApart = (transform.position - _enemyDesti).magnitude;
        // Change State to CHASE if Player is near.
        if(distanceApart < enemyChaseRange)
        {
            skeletonState = EnemySkeletonState.CHASE;
        }

        // Change State to PATROL if Player is not near, and x secs has passed.
        canCountDownPatrolTimer = true;
        // If Enemy Can Patrol
        if(canPatrol)
        {
            // Reset
            enemyToPatrolTimer = 5f;
            canCountDownPatrolTimer = false;
            canPatrol = false;

            skeletonState = EnemySkeletonState.PATROL;
        }
    }

    // Enemy Walk - waypoint to waypoint
    private void SkeletonPatrol(Vector3 _enemyDesti)
    {
        // Set Enemey Walk to TRUE
        e_spriteManager.SetMoving(true);
        // Distance between Player and Enemy.
        distanceApart = (transform.position - _enemyDesti).magnitude;

        // Change State to CHASE if Player is within its range.
        if (distanceApart < enemyChaseRange)
        {
            skeletonState = EnemySkeletonState.CHASE;
        }

        // Patrolling
        // if reached currwaypointID reached, set nextID to currID
        float dist2waypoint = (GetComponent<Transform>().position - m_Waypoint[m_currWaypointID]).magnitude;
        if (dist2waypoint <= movespeed * Time.deltaTime / maxSpeed) //if it is possible to reach the waypoint by this frame
        {
            ++m_currWaypointID;
            m_currWaypointID %= m_Waypoint.Length;
        }
        else
        {
            // else move towards waypoint
            Vector3 dir = (m_Waypoint[m_currWaypointID] - GetComponent<Transform>().position).normalized;
            GetComponent<Transform>().position += dir * movespeed * Time.deltaTime / maxSpeed;
        }
    }


    // Enemy Chase
    private void SkeletonChase(Vector3 _enemyDesti)
    {
        // Set Enemey Walk to TRUE
        e_spriteManager.SetMoving(true);
        // Enemy will walk to Player Position smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _enemyDesti, ref currentVelocity, chasingTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Enemy.
        distanceApart = (transform.position - _enemyDesti).magnitude;
        
        // Change State to ATTACK if Player is within range.
        if(distanceApart < enemyAttackRange)
        {
            skeletonState = EnemySkeletonState.ATTACK;
        }
        // Change State to IDLE if Player is outside of Chase Range.
        if(distanceApart > enemyChaseRange)
        {
            skeletonState = EnemySkeletonState.IDLE;
        }
    }

    // Enemy Attack
    private void SkeletonAttack(Vector3 _enemyDesti)
    {
        // Set Enemey Walk to FALSE
        e_spriteManager.SetMoving(false);

        // Distance between Player and Enemy.
        distanceApart = (transform.position - _enemyDesti).magnitude;
        // Change State to CHASE if Player is outside of Attack Range.
        if(distanceApart > enemyAttackRange)
        {
            skeletonState = EnemySkeletonState.CHASE;
        }

        // Enemy Attack
        // Start CountDownTimer, so that Enemy will deal damage to Player for every x secs.
        canCountDownAttackTimer = true;
        // If Enemy can Attack
        if (canAttack)
        {
            // Reset 
            enemyAttackTimer = 3f;
            canCountDownAttackTimer = false;
            canAttack = false;

            // Start to attack.
            player.GetComponent<Player2D_StatsHolder>().Health -= (int)Attack;
            e_spriteManager.SetAttack(true);
        }
    }

    // When Enemy is Dead, wait for a few secs before destroying it.
    IEnumerator SkeletonStateToDie()
    {
        // Set Enemey Walk to FALSE
        e_spriteManager.SetMoving(false);

        float _delayTime = 1f;
        yield return new WaitForSeconds(_delayTime);

        /* Add EXP to Player when Die */
        playerStats.EXP += expReward;
        Debug.Log(playerStats.EXP);
        Debug.Log("add achievementsmanager to gamescript");
        GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().UpdateProperties("KILL_ENEMY", 1);
        Destroy(gameObject);
    }

    // Handles Player Attack Enemy 
    private void PlayerHitEnemy()
    {
        if (GetComponent<CollisionPlayerMelee>().Attacked)
        {
            health -= (int)playerStats.Attack;
            GetComponent<CollisionPlayerMelee>().Attacked = false;
        }

        if (health <= 0)
            skeletonState = EnemySkeletonState.DIE;
    }

    // Get SkeletonState
    public string GetState()
    {
        return skeletonState.ToString();
    }
}
