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
    public float animTimer; // countdown timer
    private float m_fAniTime; // value to countdown from

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
    enum EQTYPE
    {
        HELMET,
        WEAPON,
        TOTAL
    }
    ItemBase[] EquipmentList = new ItemBase[(int)EQTYPE.TOTAL];

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
        animTimer = 0.0f;
        m_fAniTime = 1.0f;
        attackClicked = false;

        anim = GetComponent<Animation>();

        for (int i = 0; i < EquipmentList.Length; ++i)
            EquipmentList[i] = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.IDLE)
        {
            anim.Play("Idle_1");
        } //Player's default animation

        if (canMove)
            Movement();

        //UnlockCursor();
        PlayerAttacks();
        AnimationUpdate();

        if (Input.GetKey(KeyCode.O))
        {
            Debug.Log("MOVE THESE TO ON CLICK WITH INVENTORY/UI");
            EquipWeapon(Inventory[0]);
            DebugPlayerStats();
        }

        if (Input.GetKey(KeyCode.P))
        {
            EquipWeapon(Inventory[1]);
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

        /* When Clicked down */
        if (Input.GetMouseButtonDown(0) && !attackClicked)
            attackClicked = true;

        // Change Animation
        if (attackClicked)
        {
            canMove = false;
            animTimer += Time.deltaTime;
            playerState = PlayerState.SWISH;
            if (animTimer >= m_fAniTime)
            {
                attackClicked = false;
                canMove = true;
                animTimer -= m_fAniTime;
            }
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
                if (animTimer < m_fAniTime)
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
        if (!Inventory.Contains(_weapon))
            return;

        if (_weapon.getType() == "Weapons")
        {
            if (EquipmentList[(int)EQTYPE.WEAPON] == null)
            {
                EquipmentList[(int)EQTYPE.WEAPON] = _weapon;
                attack += _weapon.Attack;
            }
            else
            {
                attack -= EquipmentList[(int)EQTYPE.WEAPON].Attack;
                EquipmentList[(int)EQTYPE.WEAPON] = _weapon;
                attack += _weapon.Attack;
            }
        }
    }

    public void AddGold(int Amount)
    {
        gold += Amount;
    }
}