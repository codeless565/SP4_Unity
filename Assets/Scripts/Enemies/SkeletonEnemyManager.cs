using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Skeleton Enemy
public class SkeletonEnemyManager : MonoBehaviour, StatsBase
{
    SpriteManager e_spriteManager;

    enum EnemySkeletonState
    {
        IDLE,
        WALK,
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
    private float minDistanceApart = 1.5f;
    private float chaseDistanceRange = 15f;
    private float distanceApart;
    private float EnemyAttackTimer;
    private bool canCountAttackTimer = false;
    public float chasingTimer = 4f;
    public float maxSpeed = 5f;

    // Player //
    private Vector2 playerPos;
    private Vector2 distanceOffset;
    private Vector2 PlayerDestination;
    private GameObject player;
    private Player2D_StatsHolder playerStats;

    // Pet //
    private GameObject pet;
    private int PetDamagedCounter;

    // Stats Setter and Getter //
    public string Name
    {
        get
        {
            return "EnemySkeleton";
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
        // Enemy Animation
        e_spriteManager = GetComponent<SpriteManager>();
        // set default equipments(will be moved to savefile)
        e_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_HAT);
        e_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_DEFAULT);
        e_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_DEFAULT);
        e_spriteManager.SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_DEFAULT);
        e_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.DAGGER);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        if (GameObject.FindGameObjectWithTag("Pet") != null)
            pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>().gameObject;

        PetDamagedCounter = 0;

        // Setting Skeleton Initial State as IDLE.
        skeletonState = EnemySkeletonState.IDLE;

        // Setting an Offset.
        distanceOffset.Set(1f, 1f);
        // Setting Enemy Attack Timer to 0.8f
        EnemyAttackTimer = 0.5f;
        //Initialize Stats from the leveling system
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, false);
    }
	
	// Update is called once per frame
	void Update ()
    {   
        // Getting Player Position.
        playerPos = player.transform.position;
        PlayerDestination = playerPos - distanceOffset;

        //if (skeletonState == EnemySkeletonState.IDLE)
        //{
        //    anim.Play("Idle");
        //}

        //// Changing Enemy States Manually //
        //if(Input.GetKey(KeyCode.Alpha1))
        //{
        //    // Change State to Idle
        //    skeletonState = EnemySkeletonState.IDLE;
        //}
        //if(Input.GetKey(KeyCode.Alpha2))
        //{
        //    // Change State to Walk
        //    skeletonState = EnemySkeletonState.WALK;
        //}
        //if(Input.GetKey(KeyCode.Alpha3))
        //{
        //    // Change State to Chase
        //    skeletonState = EnemySkeletonState.CHASE;
        //}
        if (Input.GetKey(KeyCode.Alpha4))
        {
            // Change State to Attack
            skeletonState = EnemySkeletonState.ATTACK;
        }
        //if (Input.GetKey(KeyCode.Alpha5))
        //{
        //    // Change State to Death
        //    skeletonState = EnemySkeletonState.DIE;
        //}
        //// END //

        //Enemy States
        switch (skeletonState)
        {
            case EnemySkeletonState.IDLE:
                SkeletonIdle(PlayerDestination);
                break;

            case EnemySkeletonState.WALK:
                SkeletonWalk();
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

        //AnimationUpdate();

        // If AttackTimer can be counted down.
        if (canCountAttackTimer)
        {
            EnemyAttackTimer -= Time.deltaTime;
        }

        PlayerHitEnemy();
    }

    // Enemy Idle
    private void SkeletonIdle(Vector2 _playerDesti)
    {
        // Distance between Player and Enemy.
        distanceApart = Vector2.Distance(transform.position, _playerDesti);

        // If Player is within Enemy Chase Range, then change State to CHASE.
        if(distanceApart < chaseDistanceRange)
        {
            skeletonState = EnemySkeletonState.CHASE;
        }
    }

    // Enemy Walk - waypoint to waypoint
    private void SkeletonWalk()
    {
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
            Vector3 dir = (m_Waypoint[m_currWaypointID] - GetComponent<Transform>().position ).normalized;
            GetComponent<Transform>().position += dir * movespeed * Time.deltaTime / maxSpeed;
        }
    }


    // Enemy Chase
    private void SkeletonChase(Vector2 _playerDesti, Vector2 _playerPos, Vector2 _distOffset)
    {
        // Set Enemey Walk
        e_spriteManager.SetPlayerMoving(true);

        // Enemy will walk to Player Position smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _playerPos - _distOffset, ref currentVelocity, chasingTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Enemy.
        distanceApart = Vector2.Distance(transform.position, _playerDesti);

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
    private void SkeletonAttack(Vector2 _playerDesti)
    {
        // Set Enemey Walk
        e_spriteManager.SetPlayerMoving(false);

        // Distance between Player and Enemy.
        distanceApart = Vector2.Distance(transform.position, _playerDesti);

        // Set CountAttackTimer to true.
        canCountAttackTimer = true;

        if(EnemyAttackTimer <= 0f)
        {
            EnemyAttackTimer = 0.5f;
            player.GetComponent<Player2D_StatsHolder>().Health -= (int)Attack;

            // Play Slash Animation
            e_spriteManager.SetSlash(true);
        }

        // If Player has moved and its not within range for Enemy to Attack, change State to Chase.
        if (distanceApart > minDistanceApart)
        {
            // Set CountAttackTimer to false
            canCountAttackTimer = false;

            e_spriteManager.SetSlash(false);

            // Start Parallel action.
            float _delayTime = /*anim.GetClip("Attack").length*/ 0.5F;
            StartCoroutine(SkeletonStateToChase(_delayTime));
        }
    }

    // When Skeleton Die, wait for animation to be done playing before destroying it.
    IEnumerator SkeletonStateToDie()
    {
        float _delayTime = /*anim.GetClip("Death").length*/ 0.5F;
        yield return new WaitForSeconds(_delayTime);

        /* Add EXP to Player when Die */
        playerStats.EXP += expReward;
        Debug.Log(playerStats.EXP);

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
                //anim.Play("Idle");
                break;

            case EnemySkeletonState.WALK:
               // anim.Play("Walk");
                break;

            case EnemySkeletonState.ATTACK:
                //anim.Play("Attack");
                break;

            case EnemySkeletonState.CHASE:
               // anim.Play("Run");
                break;

            case EnemySkeletonState.DIE:
               // anim.Play("Death");
                break;
        }
    }

    // Handles Player Attack Enemy 
    private void PlayerHitEnemy()
    {
        if (GetComponent<CollisionPlayerMelee>().Attacked)
        {
            health -= (int)playerStats.Attack;
            GetComponent<CollisionPlayerMelee>().Attacked = false;
        }

        //Debug.Log("Enemy HP" + health);
        //Debug.Log("Enemy Lvl" + enemyLevel);

        if (health <= 0)
        {
            // Play Die Animation
            e_spriteManager.SetDie(true);
            skeletonState = EnemySkeletonState.DIE;
        }
    }
    
}
