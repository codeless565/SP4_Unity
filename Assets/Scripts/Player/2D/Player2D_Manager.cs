using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2D_Manager : MonoBehaviour, StatsBase, CollisionBase
{
    /* Animation */
    private Animator anim;
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

    /* Player Stats */
    [SerializeField]
    int playerLevel = 1;
    [SerializeField]
    int health = 100;
    [SerializeField]
    int mana = 100;
    [SerializeField]
    float attack = 10;
    [SerializeField]
    float defense = 10;
    [SerializeField]
    float movespeed = 10;
    [SerializeField]
    public int gold = 9999999;

    /* Direction Player will face */
    enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right,
    };
    Direction toMove = 0;

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
            return "player2D";
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


    // Use this for initialization
    void Start ()
    {
        //DebugPlayerStats();
        anim = GetComponent<Animator>();
        anim.SetBool("MoveDown", true);
        toMove = Direction.Down;

    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(toMove)
        {
            case Direction.Up:
                anim.SetBool("MoveUp", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveRight", false);
                break;
            case Direction.Down:
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", true);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveRight", false);
                break;
            case Direction.Left:
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", true);
                anim.SetBool("MoveRight", false);
                break;
            case Direction.Right:
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveRight", true);
                break;
        }

        Movement2D();
	}

    void moveLeft()
    {
        //if (toMove != Direction.Left)
        //{
        //    switch (toMove)
        //    {
        //        case Direction.Down:
        //            //transform.Rotate(0, 0, -90);
        //            anim.SetBool("MoveDown", true);
        //            break;

        //        case Direction.Right:
        //            transform.Rotate(0, 0, 180);
        //            break;

        //        case Direction.Up:
        //            //transform.Rotate(0, 0, 90);
        //            anim.SetBool("MoveUp", true);
        //            break;
        //    }
        //    toMove = Direction.Left;
        //}
        toMove = Direction.Left;
        transform.position -= transform.right * MoveSpeed * Time.deltaTime;
    }

    void moveRight()
    {
        //if (toMove != Direction.Right)
        //{
        //    switch (toMove)
        //    {
        //        case Direction.Down:
        //            //transform.Rotate(0, 0, -90);
        //            anim.SetBool("MoveDown", true);
        //            break;

        //        case Direction.Left:
        //            transform.Rotate(0, 0, 180);
        //            break;

        //        case Direction.Up:
        //           // transform.Rotate(0, 0, -90);
        //            anim.SetBool("MoveUp", true);
        //            break;
        //    }
        //    toMove = Direction.Right;
        //}
        toMove = Direction.Right;
        transform.position += transform.right * MoveSpeed * Time.deltaTime;
    }

    void moveUp()
    {
        // Sprite not facing up 
        //if (toMove != Direction.Up)
        //{
        //    switch (toMove)
        //    {
        //        case Direction.Down:
        //            //transform.Rotate(0, 0, -90);
        //            anim.SetBool("MoveUp", true);
        //            anim.SetBool("MoveDown", false);
        //            break;

        //        case Direction.Right:
        //            transform.Rotate(0, 0, 90);
        //            break;

        //        case Direction.Left:
        //            transform.Rotate(0, 0, -90);
        //            break;
        //    }
        //    toMove = Direction.Up;
        //}

        toMove = Direction.Up;

        // Movement
        transform.position += transform.up * MoveSpeed * Time.deltaTime;
    }

    void moveDown()
    {
        //if (toMove != Direction.Down)
        //{
        //    switch (toMove)
        //    {
        //        case Direction.Up:
        //            //transform.Rotate(0, 0, 180);
        //            anim.SetBool("MoveDown", true);
        //            anim.SetBool("MoveUp", false);
        //            break;

        //        case Direction.Right:
        //            transform.Rotate(0, 0, -90);
        //            break;

        //        case Direction.Left:
        //            transform.Rotate(0, 0, 90);
        //            break;
        //    }
        //    toMove = Direction.Down;
        //}
        toMove = Direction.Down;
        transform.position -= transform.up * MoveSpeed * Time.deltaTime;
    }

    void KeyMove()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            moveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }
    }

    void AccMove()
    {
        //values from accelerometer;
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;

        if(x < 0) //move left
        {
            moveLeft();
        }
        else //move right
        {
            moveRight();
        }

        if(y < 0)
        {
            moveUp();
        }
        else
        {
            moveDown();
        }
    }

    /* Movement of Player - Camera is Fixed, Player will move according to its direction */
    void Movement2D()
    {
        KeyMove();
        //AccMove();
    }

    /* Interaction with Objects */
    public void CollisionResponse(string _tag)
    {
        // Player - Traps 
        // When hit by traps, change state to be "playerInjuried"
        // in state do everything when injuried before coming back to this state


        // Player - Merchant
        // When near merchant, press e to go into buying state
        // in state , dont update anything until when the player press back button , then change script back to this


        // Player- Wooden box



    }

    /* Print Debug Information */
    void DebugPlayerStats()
    {
        Debug.Log("Name : " + Name);
        Debug.Log("Level : " + Level);
        Debug.Log("playerHealth : " + Health);
        Debug.Log("Att : " + Attack);
        Debug.Log("Def : " + Defense);
        Debug.Log("MoveSpeed : " + MoveSpeed);
        Debug.Log("Gold : " + gold.ToString());
    }
}
