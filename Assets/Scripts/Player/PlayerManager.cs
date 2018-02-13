using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Player ( Movement, Attack, etc )
public class PlayerManager : MonoBehaviour, StatsBase
{
    //private float rotateAngle;

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
       

        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);
    }

    /* Movement of Player - temporary */
    private void Movement()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * GetMoveSpeed() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * GetMoveSpeed() * Time.deltaTime;
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * GetMoveSpeed() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * GetMoveSpeed() * Time.deltaTime;
        }
    }

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
