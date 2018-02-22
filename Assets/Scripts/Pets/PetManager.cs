﻿using System.Collections;
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
    int petLevel = 0;

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
    private Vector2 petDestination;
    private Vector2 destinationOffset;
    private float followTimer = 2f;
    private float maxSpeed = 20f;
    private float distanceApart;
    private float PetGuardRange;
    private float PetFollowRange;
    private float PetHealRange;
    private bool HasPetHeal;

    // Player //
    private Player2D_StatsHolder playerStats;
    private GameObject player;
    private float addPlayerHP;
    public GameObject playerHealingSprite;

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

        // The amount of HP that a Pet will use to heal Player.
        addPlayerHP = 20f;
        HasPetHeal = false;

        // Pet current state to be GUARD.
        petState = PetState.GUARD;
        // Setting an Offset.
        destinationOffset.Set(1f, 1f);
        // Setting the range for Pet State to be GUARD.
        PetGuardRange = 2f;
        // Setting the range for Pet to heal the Player.
        PetHealRange = 2.5f;
        //Initialize Stats from the leveling system
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, false);
    }

    void Update ()
    {
        // Getting Player Position for Pet.
        petDestination = player.transform.position;

        // Checks for Player's Health, if it's below 1/3. Pet will heal Player Health.
        if (playerStats.Health <= (playerStats.MaxHealth * 0.3f))
        {
            if (!HasPetHeal)
                petState = PetState.HEAL;
        }

        // Pet States
        switch (petState)
        {
            case PetState.GUARD:
                PetGuard(petDestination);
                break;
            case PetState.FOLLOW:
                PetFollow(petDestination, destinationOffset);
                break;
            case PetState.HEAL:
                PetHeal(petDestination);
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

       // Debug.Log("player HP: " + playerStats.Health);
    }

    // Similar to IDLE state of Enemy, Pet will stay still when it's near the Player and "Guard" it.
    private void PetGuard(Vector2 _petDesti)
    {
        // Distance between Player and Pet.
        distanceApart = Vector2.Distance(transform.position, _petDesti);

        // If Player is out of Pet range, change to FOLLOW.
        if (distanceApart > PetGuardRange)
        {
            petState = PetState.FOLLOW;
        }
    }

    // If Player moves beyond Pet Guard Range, Pet State will change to Follow, and follow the Player.
    private void PetFollow(Vector2 _petDesti, Vector2 _distOffset)
    {
        // Pet will walk to Player smoothly.
        transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

        // Distance between Player and Pet.
        distanceApart = Vector2.Distance(transform.position, _petDesti);

        // If Player is within its GUard Range, change to GUARD.
        if (distanceApart < PetGuardRange)
        {
            petState = PetState.GUARD;
        }
    }

    // If Player Health is below x, the Pet will heal the Player to for x seconds.
    private void PetHeal(Vector2 _petDesti)
    {
        if(!HasPetHeal)
        {
            // Pet will walk to Player smoothly.
            transform.position = Vector2.SmoothDamp(transform.position, _petDesti, ref currentVelocity, followTimer, maxSpeed, Time.deltaTime);

            // Distance between Player and Pet.
            distanceApart = Vector2.Distance(transform.position, _petDesti);

            if (distanceApart < PetHealRange)
            {
                // Increase Player Health.
                playerStats.Health += addPlayerHP;
                HasPetHeal = true;
            }
        }

        if (HasPetHeal)
        {
            //Debug.Log("Not Healing Anymore.... Changing to Guard.");
            // Start the cool down timer.
            // Changing to Guard State.
            petState = PetState.GUARD;
        }
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
