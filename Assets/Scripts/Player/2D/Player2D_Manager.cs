using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2D_Manager : MonoBehaviour, StatsBase, CollisionBase
{
    /* Player Stats */
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
    public string Name
    {
        get
        {
            return "player2D";
        }
    }

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


    // Use this for initialization
    void Start ()
    {
        //DebugPlayerStats();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement2D();
	}

    /* Movement of Player - Camera is Fixed, Player will move according to its direction */
    void Movement2D()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            // Sprite not facing up 
            if (toMove != Direction.Up)
            {
                switch (toMove)
                {
                    case Direction.Down:
                        transform.Rotate(0, 0, 180);
                        break;

                    case Direction.Right:
                        transform.Rotate(0, 0, 90);
                        break;

                    case Direction.Left:
                        transform.Rotate(0, 0, -90);
                        break;
                }
                toMove = Direction.Up;
            }

            // Movement
            transform.position += transform.up * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (toMove != Direction.Down)
            {
                switch (toMove)
                {
                    case Direction.Up:
                        transform.Rotate(0, 0, 180);
                        break;

                    case Direction.Right:
                        transform.Rotate(0, 0, -90);
                        break;

                    case Direction.Left:
                        transform.Rotate(0, 0, 90);
                        break;
                }
                toMove = Direction.Down;
            }

            transform.position += transform.up * MoveSpeed * Time.deltaTime;
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            if (toMove != Direction.Left)
            {
                switch (toMove)
                {
                    case Direction.Down:
                        transform.Rotate(0, 0, -90);
                        break;

                    case Direction.Right:
                        transform.Rotate(0, 0, 180);
                        break;

                    case Direction.Up:
                        transform.Rotate(0, 0, 90);
                        break;
                }
                toMove = Direction.Left;
            }

            transform.position += transform.up * MoveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (toMove != Direction.Right)
            {
                switch (toMove)
                {
                    case Direction.Down:
                        transform.Rotate(0, 0, 90);
                        break;

                    case Direction.Left:
                        transform.Rotate(0, 0, 180);
                        break;

                    case Direction.Up:
                        transform.Rotate(0, 0, -90);
                        break;
                }
                toMove = Direction.Right;
            }

            transform.position += transform.up * MoveSpeed * Time.deltaTime;
        }
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
