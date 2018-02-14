using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Player ( Movement, Attack, etc )
public class PlayerManager : MonoBehaviour, StatsBase
{
    /* Animation */
    public Animation anim;
    enum PlayerState
    {
        IDLE,
        WALK,
        SWISH, //attack1
        DOUBLE, //attack2
        HACK, //attack3
        HIT,
        DIE,
    };
    PlayerState playerState;

    /* Stats */
    [SerializeField]
    int playerLevel = 1;
    [SerializeField]
    int health = 100;
    [SerializeField]
    float attack = 10;
    [SerializeField]
    float defense = 10;
    [SerializeField]
    float movespeed = 10;

    public bool canMove;

    /* List storing Player equipment */
    public List<ItemWeapons> Equipment = new List<ItemWeapons>();

    /* Setters and Getters */
    public int Level
    {
        get
        {
            return playerLevel;
        }

        set
        {
            playerLevel = value;
        }
    }

    public string Name
    {
        get
        {
            return "Player";
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
    void Start()
    {
        //DebugPlayerStats();
        //Cursor.lockState = CursorLockMode.Locked;

        //Test
        Equipment.Add(new Sword());
        Equipment.Add(new Sword());
        Equipment.Add(new Sword());
        Equipment.Add(new Sword());
        Equipment.Add(new Sword());
        Equipment.Add(new Sword());

        foreach (ItemWeapons weapon in Equipment)
        {
            weapon.isEquipped = true;
        }

        gameObject.GetComponent<InventoryBar>().DisplayPlayerEQ();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.IDLE)
        {
            anim.Play("Idle_1");
        } //Player's default animation

        if (!canMove)
            return;

        Movement();
        //UnlockCursor();
        PlayerAttacks();
        AnimationUpdate();
        EquipmentUpdate();
    }

    /* Movement of Player */
    private void Movement()
    {
        playerState = PlayerState.IDLE;

        //Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.WALK;
            //Vector3 movement = new Vector3(0.0f, 0.0f, transform.position.z);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), /*0.15F*/ 1);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.WALK;
            //Vector3 movement = new Vector3(0.0f, 0.0f, -transform.position.z);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement),/* 0.15F*/ 1);
            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.WALK;

            /* 1) Camera Fixed, Player Rotates */
            //Vector3 movement = new Vector3(-transform.position.x, 0.0f, 0.0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), /*0.15F*/ 1);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            /* 2) Normal Rotation with Camera */
            transform.Rotate(new Vector3(0, -90 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerState = PlayerState.WALK;

            /* 1) Camera Fixed, Player Rotates */
            //Vector3 movement = new Vector3(transform.position.x, 0.0f, 0.0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), /*0.15F*/ 1);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            /* 2) Normal Rotation with Camera */
            transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }


        /* 3) Rotate Camera By Mouse */
        //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 120.0F);
    }

    /* Attack of Player */
    private void PlayerAttacks()
    {
        if (Input.GetMouseButton(0))
        {
            playerState = PlayerState.SWISH;
        }
        else if (playerState != PlayerState.WALK)
            playerState = PlayerState.IDLE;
    }

    /* Unlocking Mouse from Screen */
    static bool isPressed = false;
    private void UnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPressed)
        {
            isPressed = true;
            if (Cursor.lockState.Equals(CursorLockMode.Locked))
                Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.R) && isPressed)
        {
            isPressed = false;
            if (Cursor.lockState.Equals(CursorLockMode.None))
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /* Updates in Animation HERE */
    private void AnimationUpdate()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                anim.Play("Idle_1");
                break;

            case PlayerState.WALK:
                anim.Play("RunCycle");
                break;

            case PlayerState.SWISH:
                anim.Play("Attack_1");
                break;

            case PlayerState.DOUBLE:
                anim.Play("Attack_2");
                break;

            case PlayerState.HACK:
                anim.Play("Attack_3");
                break;

            case PlayerState.HIT:
                anim.Play("GetHit");
                break;

            case PlayerState.DIE:
                anim.Play("Die");
                break;
        }
    }

    /* Updates in Equipments HERE */
    void EquipmentUpdate()
    {
        foreach (ItemWeapons weapon in Equipment)
        {
            if (weapon.isEquipped)
            {
                Attack += weapon.Attack;
            }
        }
    }

    /* Change Weapons */
    private void ChangeWeapon()
    {
        if (Input.GetKey(KeyCode.C))
        {
        }
    }

    /* Print Debug Information */
    void DebugPlayerStats()
    {
        Debug.Log("Name : " + Name);
        Debug.Log("playerHealth : " + Health);
        Debug.Log("Att : " + Attack);
        Debug.Log("MoveSpeed : " + MoveSpeed);
    }
}