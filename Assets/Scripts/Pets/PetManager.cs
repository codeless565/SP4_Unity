using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour, StatsBase
{
    enum PetState
    {
        GUARD,
        FOLLOW,
        HEAL,
        ATTACK,
        RECOVERY,
        TELEPORT
    }

    // Stats //
    [SerializeField]
    int petLevel = 1;

    float health;
    float maxhealth;
    float stamina;
    float maxStamina;
    float attack;
    float defense;

    [SerializeField]
    float movespeed = 10;

    LevelingSystem levelingSystem;

    // Pet //
    PetState petState;
    private Vector2 currentVelocity = Vector2.zero;
    private Vector3 petDestination;
    private float followTimer;
    private float maxSpeed;
    private float distanceApart;
    private float PetGuardRange;
    private float PetHealRange;
    private float PetFollowRange;
    private float PetAttackRange;
    private float HealCoolDownTimer;
    private float SpriteRenderTimer;
    private float PetRecoverTimer;
    private bool HasPetHeal;
    private bool CanHealCoolDown;
    private bool CanActivateSprite;
    private bool HasPetTeleport;
    private bool CanPetRecover;
    private bool HasPetRecover;
    private bool HasPetAttack;

    GameObject closestEnemy;
    string closestEnemyState;
    public GameObject petRecoveringSprite;

    // Player //
    private Player2D_StatsHolder playerStats;
    private GameObject player;
    private float addPlayerHP;
    public GameObject playerHealingSprite;
    
    #region StatsBaseSetterANDGetter
    // Stats Setter and Getter //
    public string Name
    {
        get
        {
            return "Pet";
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
            return petLevel;
        }

        set
        {
            petLevel = value;
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
    #endregion

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();

        maxSpeed = 30f;
        followTimer = 2f;

        // The amount of HP that a Pet will use to heal Player.
        addPlayerHP = 20f;
        HealCoolDownTimer = 10f;
        SpriteRenderTimer = 3f;
        HasPetHeal = false;
        CanHealCoolDown = false;
        CanActivateSprite = false;

        // Pet Recovery State
        CanPetRecover = false;
        HasPetRecover = false;
        PetRecoverTimer = 20f;

        // Pet current state to be GUARD.
        petState = PetState.FOLLOW;
        // Setting the range for Pet State to be GUARD.
        PetGuardRange = 1f;
        // Setting the range for Pet to heal the Player.
        PetHealRange = 2.5f;
        // Setting the range for Pet State to be TELEPORT.
        PetFollowRange = 5f;
        HasPetTeleport = false;
        // Setting the range for Pet to ATTACK.
        PetAttackRange = 1f;
        HasPetAttack = false;

        //Initialize Stats from the leveling system
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, false);

        // Ignore Collision between Player and Pet.
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    void Update ()
    {
        // Update every 10 frames.
        if (Time.frameCount % 10 == 0)
        {
            // Getting Player Position for Pet.
            petDestination = player.transform.position;

            // If Player's Health is 1/3 of its maximum. Pet will heal Player Health.
            if (playerStats.Health <= (playerStats.MaxHealth * 0.2f))
            {
                if (!HasPetHeal)
                    petState = PetState.HEAL; 
            }

            // Check if Pet need to attack.
            if (playerStats.Health > (playerStats.MaxHealth * 0.2f) && 
                playerStats.Health < (playerStats.MaxHealth * 0.6f))
            {
                closestEnemy = FindClosestEnemy("Enemy");
                closestEnemyState = closestEnemy.GetComponent<SkeletonEnemyManager>().GetState();

                // If Closest Enemy is still alive.
                if(closestEnemy.GetComponent<SkeletonEnemyManager>().Health > 0f)
                {
                    // Change State to ATTACK.
                    if(closestEnemyState == "ATTACK")
                    {
                        petState = PetState.ATTACK;
                    }
                }
            }
        }
        
        // Pet Heal Player Cool Down
        if (CanHealCoolDown)
        {
            // Start Cool Down Timer
            HealCoolDownTimer -= Time.deltaTime;

            if (HealCoolDownTimer <= 0f)
            {
                HealCoolDownTimer = 10f;
                CanHealCoolDown = false;
                HasPetHeal = false;
            }

            // If Pet's HP is 0, change State to Recovery.
            if (health <= 0f)
                petState = PetState.RECOVERY;
        }

        // If Healing Sprite is activated.
        if (CanActivateSprite)
        {
            // Start Sprite Render Timer
            SpriteRenderTimer -= Time.deltaTime;

            if(SpriteRenderTimer <= 0f)
            {
                SpriteRenderTimer = 3f;
                // Deactivate Sprite
                CanActivateSprite = false;
                playerHealingSprite.SetActive(false);
            }
        }

        // If Pet is going to Recover
        if(CanPetRecover)
        {
            // Start Count Down Timer
            PetRecoverTimer -= Time.deltaTime;
            // Heal Pet
            health = maxhealth;

            if(PetRecoverTimer <= 0f)
            {
                HasPetRecover = true;
            }
        }

        // Pet States
        switch (petState)
        {
            case PetState.GUARD:
                PetGuard(petDestination);
                break;
            case PetState.FOLLOW:
                PetFollow(petDestination);
                break;
            case PetState.HEAL:
                PetHeal(petDestination);
                break;
            case PetState.ATTACK:
                PetAttack(closestEnemy);
                break;
            case PetState.RECOVERY:
                PetRecovery(petDestination);
                break;
            case PetState.TELEPORT:
                PetTeleport(petDestination);
                break;
        }
    }

    // Similar to IDLE state of Enemy, Pet will stay still when it's near the Player and "Guard" it.
    private void PetGuard(Vector3 _petDesti)
    {
        // Distance between Player and Pet.
        distanceApart = (transform.position - _petDesti).magnitude;

        // If Player is out of Pet range, change to FOLLOW.
        if (distanceApart > PetGuardRange && distanceApart < PetFollowRange)
        {
            petState = PetState.FOLLOW;
        }
    }

    // If Player moves beyond Pet Guard Range, Pet State will change to Follow, and follow the Player.
    private void PetFollow(Vector3 _petDesti)
    {
        // Pet will walk to Player smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Pet.
        distanceApart = (transform.position - _petDesti).magnitude;

        // If Player is within its GUard Range, change to GUARD.
        if (distanceApart < PetGuardRange)
        {
            petState = PetState.GUARD;
        }
        // If Player is outside its Follow Range, change to TELEPORT.
        if(distanceApart > PetFollowRange)
        {
            petState = PetState.TELEPORT;
        }
    }

    // If Player Health is below x, the Pet will heal the Player to for x seconds.
    private void PetHeal(Vector3 _petDesti)
    {
        if(!HasPetHeal)
        {
            // Pet will walk to Player smoothly.
            transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

            // Distance between Player and Pet.
            distanceApart = (transform.position - _petDesti).magnitude;

            if (distanceApart < PetHealRange)
            {
                // Activate Healing Sprite
                playerHealingSprite.SetActive(true);
                CanActivateSprite = true;
                // Increase Player Health from Pet's HP.
                playerStats.Health += addPlayerHP;
                health -= addPlayerHP;
                HasPetHeal = true;
            }
        }

        if (HasPetHeal)
        {
            // Start the cool down timer.
            CanHealCoolDown = true;
            // Changing to Guard State.
            petState = PetState.GUARD;
        }
    }

    // If there Player Health is 1/2, Pet State will deal damage to the nearest Enemy.
    private void PetAttack(GameObject _closest)
    {
        // Distance between Enemy and Pet.
        distanceApart = (transform.position - _closest.transform.position).magnitude;

        // If Pet is not within Attack Range, walk to Enemy smoothly.
        if(distanceApart > PetAttackRange)
        {
            // Pet will walk to Enemy smoothly.
            transform.position = Vector2.SmoothDamp(transform.position, _closest.transform.position, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);
        }
        // Pet is within its Attack Range.
        else if(distanceApart < PetAttackRange)
        {
            // Minus Enemy Health.
            if (!HasPetAttack)
            {
                _closest.GetComponent<SkeletonEnemyManager>().Health -= (Attack * 0.5f);
                HasPetAttack = true;
            }
            else if (HasPetAttack)
            {
                HasPetAttack = false;
            }

            if(_closest.GetComponent<SkeletonEnemyManager>().Health <= 0f)
            {
                petState = PetState.GUARD;
            }
        }

    }

    // If Pet HP is 0, the Pet will still follow Player but it will not help unless its HP is maximum again.
    private void PetRecovery(Vector3 _petDesti)
    {
        // Pet will walk to Player smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

        // Activate a Sprite, or Change Sprite to show that it's recovering.
        petRecoveringSprite.SetActive(true);
        CanPetRecover = true;

        if(HasPetRecover)
        {
            // Reset
            PetRecoverTimer = 20f;
            petRecoveringSprite.SetActive(false);
            CanPetRecover = false;
            HasPetRecover = false;

            // Change State to FOLLOW after it has recovered finish.
            petState = PetState.FOLLOW;
        }
    }

    // If Pet is beyond x Range, Pet will be teleported to the Player Pos.
    private void PetTeleport(Vector3 _petDesti)
    {
        if(!HasPetTeleport)
        {
            // Teleport Pet to Player
            transform.position = _petDesti;
            HasPetTeleport = true;
        }

        if(HasPetTeleport)
        {
            // Distance between Player and Pet.
            distanceApart = (transform.position - _petDesti).magnitude;
            
            // Change State to GUARD if Player is within its range.
            if(distanceApart < PetGuardRange)
            {
                petState = PetState.GUARD;
                HasPetTeleport = false;
            }
        }
    }

    
    private void SetCurrentLevel(int _currLevel)
    {
        petLevel = _currLevel;
        levelingSystem.Init(this, false);
    }

    // Find the closest enemy in range.
    private GameObject FindClosestEnemy(string tag)
    {
        GameObject[] enemies;
        GameObject closestEnemy = null;
        enemies = GameObject.FindGameObjectsWithTag(tag);

        float distance = Mathf.Infinity;
        Vector3 playerPos = player.transform.position;

        foreach(GameObject enemy in enemies)
        {
            distanceApart = (enemy.transform.position - playerPos).sqrMagnitude;
            if(distanceApart < distance)
            {
                closestEnemy = enemy;
                distance = distanceApart;
            }
        }

        return closestEnemy;
    }

    //// Pet Collision
    //void OnCollisionEnter2D(Collision2D _other)
    //{
    //    if (_other.gameObject.tag == "Player")
    //        return;

    //    Debug.Log("Is not Player.");
    //}
}
