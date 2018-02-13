using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Player ( Movement, Attack, etc )
public class PlayerManager : MonoBehaviour, StatsBase
{
    //private float rotateAngle;
    /*Jenny's changes from here */
    public Animation anim;
    PlayerState playerState;
    int playerLevel;

    List<ItemWeapons> Equipment = new List<ItemWeapons>();

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
            return Health;
        }

        set
        {
            Health = value;
        }
    }

    public float Attack
    {
        get
        {
            return Attack;
        }

        set
        {
            Attack = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return MoveSpeed;
        }

        set
        {
            MoveSpeed = value;
        }
    }

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
    /*to here*/

    // Use this for initialization
    void Start ()
    {
        DebugPlayerStats();

        //Test
        Equipment.Add(new Sword());
        foreach (ItemWeapons weapon in Equipment)
        {
            weapon.isEquipped = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        AnimationUpdate();

        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);
        EquipmentUpdate();
    }

    /* Movement of Player - temporary */
    private void Movement()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.WALK;

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.WALK;

            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
        }

        // Left / Right
        else if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.WALK;

            transform.position -= transform.right * MoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerState = PlayerState.WALK;

            transform.position += transform.right * MoveSpeed * Time.deltaTime;
        }
        else
        playerState = PlayerState.IDLE;
    }
    /*Jenny's changes from here*/
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
    /* to here */

    void EquipmentUpdate()
    {
        foreach(ItemWeapons weapon in Equipment)
        {
            if (weapon.isEquipped)
            {
                Attack += weapon.Attack;
            }
        }
    }

    void DebugPlayerStats()
    {
        Debug.Log("Name : " + Name);
        Debug.Log("playerHealth : " + Health);
        Debug.Log("Att : " + Attack);
        Debug.Log("MoveSpeed : " + MoveSpeed);
    }
}
