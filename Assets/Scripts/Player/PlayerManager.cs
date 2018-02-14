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
    public float animTimer;
    public bool attackClicked;

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
    [SerializeField]
    public int gold = 9999999;

    public bool canMove = true;

    /* List storing Player equipment */
    public List<ItemBase> Inventory = new List<ItemBase>();
    //enum EQTYPE
    //{
    //    HELMET,
    //    WEAPON,
    //    TOTAL
    //}
    //bool[] EquipmentList = new bool[(int)EQTYPE.WEAPON];

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
        animTimer = 2.0f;
        attackClicked = false;

        anim = GetComponent<Animation>();

        //for (int i = 0; i < EquipmentList.Length; ++i)
        //    EquipmentList[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.IDLE && animTimer > 0.0f)
        {
            anim.Play("Idle_1");
        } //Player's default animation

        if (!canMove)
            return;

        Movement();
        //UnlockCursor();
        PlayerAttacks();
        AnimationUpdate();

        if (Input.GetKey(KeyCode.O))
        {
            EquipWeapon(Inventory[0]);
            DebugPlayerStats();
        }
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
            attackClicked = true;
            animTimer--;
            //playerState = PlayerState.SWISH;
        }
        else if (playerState != PlayerState.WALK && animTimer > 0)
            playerState = PlayerState.IDLE;

        if(attackClicked)
        {
            playerState = PlayerState.SWISH;
        }
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
                if(animTimer <= 0.0f)
                {
                    anim.Play("Attack_1");
                    animTimer = 2.0f;
                    attackClicked = false;
                }
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

    public void AddItem(ItemBase newitem)
    {
        Inventory.Add(newitem);
        
        gameObject.GetComponent<InventoryBar>().AddPlayerHotBar(newitem);
        Debug.Log("TO BE IMPLEMENTED");
        // to move to when player put into hotbar
    }

    public void EquipWeapon(ItemBase _weapon)
    {
        Debug.Log("TO BE IMPLEMENTED");
        //Equipment'List' to check  stuff

        ItemWeapons newWeapon;
        if (Inventory.Contains(_weapon))
            newWeapon = (ItemWeapons)_weapon;
        else
            return;

        attack += newWeapon.Attack;
        //if (!EquipmentList[(int)EQTYPE.WEAPON]) // nothing equipped
        //{
        //    EquipmentList[(int)EQTYPE.WEAPON] = true;
        //}
        //foreach (ItemBase item in Inventory)
        //{
        //    if (item.getType() != "Weapons")
        //        continue;

        //    ItemWeapons thisWeapon = (ItemWeapons)item;
        //    if(thisWeapon.isEquipped)
        //    {
        //        thisWeapon.isEquipped = false;
        //        newWeapon.isEquipped = true;
        //        Attack -= thisWeapon.Attack;
        //        Attack += newWeapon.Attack;
        //    }
        //}
    }
}