using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour, StatsBase
{
    enum PetState
    {
        GUARD,
        FOLLOW,
        PROTECT,
        ATTACK,
        RECOVERY,
        TELEPORT
    }

    // Pet //
    PetState petState;
    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 petDestination;
    private Vector2 destinationOffset;
    private float followTimer = 2f;
    private float maxSpeed = 20f;
    private float distanceApart;
    private float changeToGuardDistance;

    // Stats //
    [SerializeField]
    int petLevel = 0;

    float health = 50;
    float maxhealth = 0;
    float stamina = 0;
    float maxStamina = 0;
    float attack = 10;
    float defense = 0;

    [SerializeField]
    float movespeed = 10;

    // Player //
    private Player2D_StatsHolder playerStats;
    private GameObject player;
    private Vector2 playerPos;

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


    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();

        // Pet current state to be FOLLOW.
        petState = PetState.FOLLOW;
        // Setting an Offset.
        destinationOffset.Set(1f, 1f);
        // Setting the range for Pet State to be GUARD.
        changeToGuardDistance = 2f;
        //Initialize Stats from the leveling system
        GetComponent<LevelingSystem>().Init(this);
    }

    void Update ()
    {
        // Getting Player Position for Pet.
        playerPos = player.transform.position;
        petDestination = playerPos - destinationOffset;

        // Pet States
        switch (petState)
        {
            case PetState.GUARD:
                PetGuard();
                break;
            case PetState.FOLLOW:
                PetFollow(petDestination, destinationOffset);
                break;
            case PetState.PROTECT:
                PetProtect();
                break;
            case PetState.ATTACK:
                PetAttack();
                break;
            case PetState.RECOVERY:
                PetRecovery();
                break;
            case PetState.TELEPORT:
                PetTeleport();
                break;
        }
    }

    // Similar to IDLE state of Enemy, Pet will stay still when it's near the Player and "Guard" it.
    private void PetGuard()
    {

    }

    // If Player moves beyond Pet Guard Range, Pet State will change to Follow, and follow the Player.
    private void PetFollow(Vector2 _petDesti, Vector2 _distOffset)
    {
        // Pet will walk to Player smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Pet.
        distanceApart = Vector2.Distance(transform.position, _petDesti);

        // If there is any enemy within its range, change to ATTACK.

        // If there is no enemy within its range, change to GUARD.
        //if(distanceApart <= changeToGuardDistance)
        //{
        //    petState = PetState.GUARD;
        //}
    }
    
    // If Player Health is below x, the Pet will casts a shield around Player to protect Player for x seconds.
    private void PetProtect()
    {

    }

    // If there are Enemies near the Player, Pet State will change to Attack and deal damage to the nearest Enemy.
    private void PetAttack()
    {

    }

    // If Pet HP is 0, the Pet will still follow Player but it will not help unless its HP is maximum again.
    private void PetRecovery()
    {

    }

    // If Pet is beyond x Range, Pet will be teleported to the Player Pos.
    private void PetTeleport()
    {

    }
}
