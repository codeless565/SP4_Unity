using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with Bear Traps - Minus Health */
/* A Simple Trap - Maybe for Tutorial */
public class CollisionBearTrap : MonoBehaviour, CollisionBase
{
    private Player2D_StatsHolder m_hold;
    private float playerHealth;

    /* Timer for destroying it ( since its not gonna be shown )*/
    private float m_fTimer, maxTime;
    private bool isRendered;
    private int count; // bad way to do this

    // testing
    private SpriteRenderer test;

    void Start()
    {
        /* Player Stats */
        m_hold = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        playerHealth = m_hold.MaxHealth;
        
        /* The sprite to be not rendered */
        test = GetComponent<SpriteRenderer>();
        test.enabled = false;

        /* After Render */
        m_fTimer = 0.0f;
        maxTime = 3.0f;
        isRendered = false;
        count = 0;
    }

    void Update()
    {
        /* CoolDown to dissapear */
        if (test.enabled)
        {
            isRendered = true; // deduct health 
            m_fTimer += Time.deltaTime;
            if (m_fTimer > maxTime)
            {
                test.enabled = false;
                isRendered = false;
                Destroy(gameObject);
                m_fTimer -= maxTime;

                //count = 0;
            }
        }

        /* Decrease Player Health - Based on level, Minus 20% of Health */
        if (isRendered && count == 0)
        {
            ++count;
            m_hold.Health -= m_hold.Level * m_hold.Health * 0.2f;
            isRendered = false;
        }

        //Debug.Log(m_fTimer);
        //Debug.Log(isRendered);
        Debug.Log("Health: " + m_hold.Health);
    }

    //static private bool m_isCheckedBefore = false;
    public void CollisionResponse(string _tag)
    {
        /* Do not let Attack affect the traps - Still editing doesnt work */
        //if (_tag != "Player")
        //{
        //m_isCheckedBefore = true;
        //    return;
        //}
        /* Check for Player inside due to player having script too*/
        //if (m_isCheckedBefore)
        //return;

        /* When Collide, set to true */
        test.enabled = true;
    }
}