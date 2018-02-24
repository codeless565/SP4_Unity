using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with Bear Traps */
/* Will create effect when generated on collision */
public class CollisionBearTrap : MonoBehaviour
{
    /* Damage according to Level */
    private int m_currLevel;
    /* Timer for destroying it ( since its not gonna be shown )*/
    private float m_fTimer, maxTime;
    /* Sprite Renderer */
    private SpriteRenderer m_sprite;
    /* Limit Damage */
    private bool m_isAffected;

    void Start()
    {
        /* The sprite to be not rendered */
        m_sprite = GetComponent<SpriteRenderer>();
        m_sprite.enabled = false;
        
        /* After Render */
        m_fTimer = maxTime = 3.0f;
        m_isAffected = false;
    }

    void Update()
    {
        /* For the Rendering of the Trap */
        if (m_sprite.enabled)
        {
            m_fTimer -= Time.deltaTime;
            if (m_fTimer <= 0)
                Destroy(gameObject); // auto reset
        }
    }

    /* When Enter, Attack Entities ( any Entities )*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* If Only want player to be affected */
        //if (collision.GetComponent<Player2D_StatsHolder>() == null)
        //    return;

        if (m_isAffected)
            return;

        /* Affect all those with stats */
        if (collision.GetComponent<StatsBase>() != null)
        {
            m_isAffected = true;
            collision.GetComponent<StatsBase>().Health -= m_currLevel * 0.75f;
            Debug.Log("HP : " + collision.GetComponent<StatsBase>().Health);
        }
        m_sprite.enabled = true;
    }

    /* Setter for level */
    public int CurrentLevel
    {
        set
        {
            m_currLevel = value;
        }
    }
}