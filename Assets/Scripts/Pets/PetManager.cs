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
        RECOVERY
    }

    // Pet //
    PetState petState;
    private Vector2 currentVelocity = Vector2.zero;
    private float followTimer = 5f;
    private float maxSpeed = 5f;
    private float distanceApart;

    // Stats //
    [SerializeField]
    int petLevel = 0, health = 50, mana = 0;
    [SerializeField]
    float attack = 10, defense = 0, movespeed = 10;

    // Player //
    private GameObject player;
    private Vector2 playerPos;

    // Stats Setter and Getter //
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

    public int Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }


    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;

        // Pet current state to be FOLLOW.
        petState = PetState.FOLLOW;
    }
	
	void Update ()
    {
        // Getting Player Position for Pet.
        playerPos = player.transform.position;

        // Pet States
        switch (petState)
        {
            case PetState.GUARD:
                // Idle, being still when near Player.
                break;
            case PetState.FOLLOW:
                // Follow Player, if its out of the Guard Range.
                PetFollow(playerPos);
                break;
            case PetState.PROTECT:
                // Will cast a shield around Player if Player's Health is 30/100 - ish.
                break;
            case PetState.ATTACK:
                // Will attack the closest enemy when enemy is near player.
                break;
            case PetState.RECOVERY:
                // If Pet health reaches 0, it wil switch to this state where they will follow Player but will not do anything.
                break;
        }
    }

    private void PetFollow(Vector2 _playerPos)
    {
        // Pet will walk to Player smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _playerPos, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Pet.
        distanceApart = Vector2.Distance(transform.position, _playerPos);
        

    }
}
