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
      

        Debug.Log("Name : " + GetName());
        Debug.Log("playerHealth : " + GetHealth());
        Debug.Log("Att : " + GetAttack());
        Debug.Log("MoveSpeed : " + GetMoveSpeed());
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        AnimationUpdate();

        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);
    }

    /* Movement of Player - temporary */
    private void Movement()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.WALK;

            transform.position += transform.forward * GetMoveSpeed() * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.WALK;

            transform.position -= transform.forward * GetMoveSpeed() * Time.deltaTime;
        }

        // Left / Right
        else if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.WALK;

            transform.position -= transform.right * GetMoveSpeed() * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerState = PlayerState.WALK;

            transform.position += transform.right * GetMoveSpeed() * Time.deltaTime;
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
    public string GetName()
    {
        return "player";
    }

    public int GetHealth()
    {
        return 100;
    }

    public float GetAttack()
    {
        return 10f; ;
    }

    public float GetMoveSpeed()
    {
        return 20f;
    }
}
