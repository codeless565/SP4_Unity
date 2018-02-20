using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2D_Manager : MonoBehaviour, StatsBase, CollisionBase
{
    /* Animation */
   public GameObject Hair;
    private Animator anim, HairAnim;

    private bool PlayerMoving;
    private Vector2 lastMove;

    SpriteManager p_spriteManager = new SpriteManager();

    enum PlayerState
    {
        IDLE,
        WALK,
        SLASH, //attack1
        SPELL, //attack2
        THRUST, //attack3
        BOW, //attack4
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
        Down = 0,
        Up,
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
        HairAnim = Hair.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
<<<<<<< HEAD
        Movement2D();
=======
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

        /* Checking of Player Health */
        if(Health <= 0)
        {
            playerState = PlayerState.DIE;
        }

        /* Player States */
        switch(playerState)
        {
            case PlayerState.DIE:
                PlayerDeath();
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
>>>>>>> 92bb237364313d9db4812094195fd073e3065467
    }

    void KeyMove()
    {
        PlayerMoving = false;

        //move left/right
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime, 0f, 0f));
            PlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.RIGHT;
            }
            else
            {
                p_spriteManager.direction = SpriteManager.S_Dir.LEFT;
            }

            p_spriteManager.hori = lastMove.x;
            
        }
        //move up/down
        if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
        {
            transform.Translate(new Vector3(0f,Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime, 0f));
            PlayerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        HairAnim.SetFloat("MoveX", lastMove.x);
        HairAnim.SetFloat("MoveY", lastMove.y);
        anim.SetBool("PlayerMoving", PlayerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        
    }

    void AccMove()
    {
        //values from accelerometer;
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;
    }

    /* Death of Player */
    void PlayerDeath()
    {
        Destroy(gameObject);

        // Generate a LoseScreen .etc.
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
