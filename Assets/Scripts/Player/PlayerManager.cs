using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Player ( Movement, Attack, etc )
public class PlayerManager : MonoBehaviour
{
    /* Stats */
   static private string playerName;
   static private int playerHealth;
   static private float playerAtt;
   static private float playerDef;
   static private float moveSpeed;

    //private float rotateAngle;

	// Use this for initialization
	void Start ()
    {
        moveSpeed = 5f;
        playerName = "player";
        playerHealth = 100;
        playerAtt = 10f;
        playerDef = 10f;

        Debug.Log("Name : " + playerName);
        Debug.Log("playerHealth : " + playerHealth);
        Debug.Log("Att : " + playerAtt);
        Debug.Log("Def : " + playerDef);
        Debug.Log("MoveSpeed : " + moveSpeed);
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
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    /* Setters */
    static public void SetPlayerName(string _name)
    {
        playerName = _name;
    }
    static public void SetPlayerHealth(int _health)
    {
        playerHealth = _health;
    }
    static public void SetPlayerAttack(float _att)
    {
        playerAtt = _att;
    }
    static public void SetPlayerDefense(float _def)
    {
        playerDef = _def;
    }

    /* Getters */
    static public string GetPlayerName()
    {
        return playerName;
    }
    static public int GetPlayerHealth()
    {
        return playerHealth;
    }
    static public float GetPlayerAttack()
    {
        return playerAtt;
    }
    static public float GetPlayerDefense()
    {
        return playerDef;
    }
}
