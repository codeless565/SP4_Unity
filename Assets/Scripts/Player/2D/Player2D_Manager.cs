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
            /* In which direction and axis will player be moving in */
            //movement = new Vector3(0, transform.position.y, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerForward), 0.55F);

            transform.position += transform.up * MoveSpeed * Time.deltaTime;

            //Debug.Log("Position : " + transform.position);
            //Debug.Log("Rotation : " + transform.rotation);
        }
        if (Input.GetKey(KeyCode.S))
        {
            /* In which direction and axis will player be moving in */
            //movement = new Vector3(0, transform.position.y, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerForward, -movement), 0.55F);

            transform.position -= transform.up * MoveSpeed * Time.deltaTime;

            //Debug.Log("Position : " + transform.position);
            //Debug.Log("Rotation : " + transform.rotation);
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            /* In which direction and axis will player be moving in */
            //movement = new Vector3(transform.position.x + 1, 0.0F, 0.0F);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerForward, movement), /*0.15F*/ 1);

            //transform.Rotate(0, 0, 90);
            //transform.Rotate(Vector3.back);
            //transform.LookAt(transform.position - Vector3.right);

            transform.position -= transform.right * MoveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            /* In which direction and axis will player be moving in */
            //movement = new Vector3(transform.position.x, 0.0F, 0.0F);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerForward, movement), /*0.15F*/ 1);

            //transform.Rotate(0, 0, -90 * Time.deltaTime);
            transform.position += transform.right * MoveSpeed * Time.deltaTime;



        }


    }

    /* Interaction with Objects */
    public void CollisionResponse()
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
