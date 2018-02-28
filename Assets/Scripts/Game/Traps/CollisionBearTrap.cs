using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with Bear Traps */
/* Will create stun effect when generated on collision */
public class CollisionBearTrap : MonoBehaviour
{
    /* Damage according to Floor */
    private int m_currLevel;
    /* Timer for destroying it ( since its not gonna be shown )*/
    private float m_fTimer;
    /* Sprite Renderer */
    //private SpriteRenderer m_sprite;
    /* Limit Damage */
    private bool m_isAffected;

    void Start()
    {
        /* The sprite to be not rendered */
        //m_sprite = GetComponent<SpriteRenderer>();
        //m_sprite.enabled = false;
        
        /* After Render */
        m_fTimer = 1.0f;
        m_isAffected = false;
    }

    void Update()
    {
        /* For the Rendering of the Trap */
        if (m_isAffected)
        {
            m_fTimer -= Time.deltaTime;
            if (m_fTimer <= 0)
                Destroy(gameObject); // auto reset
        }
    }

    /* When Enter, Attack Entities ( any Entities )*/
    private void OnTriggerEnter2D(Collider2D otherEntity)
    {
        /* If Only want player to be affected */
        //if (collision.GetComponent<Player2D_StatsHolder>() == null)
        //    return;

        if (m_isAffected)
            return;

        /* Affect all those with stats */
        if (otherEntity.GetComponent<StatsBase>() != null)
        {
            m_isAffected = true;
            //otherEntity.GetComponent<StatsBase>().MoveSpeed = 0.0f;

            float entityMaxHP = otherEntity.GetComponent<StatsBase>().MaxHealth;
            otherEntity.GetComponent<StatsBase>().Health -= entityMaxHP * 0.01f * m_currLevel; // deal by percentage of hp
        }
        //m_sprite.enabled = true;
    }

    /* Setter for level */
    public int CurrentFloor
    {
        set
        {
            m_currLevel = value;
        }
    }
}