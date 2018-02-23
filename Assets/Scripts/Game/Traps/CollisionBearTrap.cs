using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with Bear Traps - Minus Health */
/* A Simple Trap - Maybe for Tutorial */
public class CollisionBearTrap : MonoBehaviour, CollisionBase
{
    private Player2D_StatsHolder m_hold;

    /* Timer for destroying it ( since its not gonna be shown )*/
    private float m_fTimer, maxTime;
    private bool isRendered;
    private int count; // bad way to do this
    private SpriteRenderer test;

    void Start()
    {
        /* Player Stats */
        m_hold = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        
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
        /* For the Health Effect on Player */
        if (test.enabled && count == 0)
        {
            m_hold.Health -= m_hold.Level * m_hold.Health * 0.02f;
            count = 1;
        }

        /* For the Rendering of the Trap */
        if (test.enabled)
        {
            m_fTimer += Time.deltaTime;
            if (m_fTimer > maxTime)
            {
                Destroy(gameObject);
                m_fTimer -= maxTime;
            }
        }
    }
    
    public void CollisionResponse(string _tag)
    {
        /* Further checking for player only */
        if (_tag != "Player")
            return;

        isRendered = true;
        test.enabled = true;
    }
}