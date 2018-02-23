using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with Bear Traps - Minus Health */
/* A Simple Trap */
public class CollisionBearTrap : MonoBehaviour, CollisionBase
{
    private Player2D_StatsHolder m_hold;
    private float playerHealth;

    void Start()
    {
        m_hold = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        playerHealth = m_hold.MaxHealth;
    }
    
    public void CollisionResponse(string _tag)
    {
        /* Only if Its player, then it will have effect */
        if (_tag != "Player")
            return;

        /* Decrease Player Health - Based on level, Minus 2% of Health */
        m_hold.Health -= m_hold.Level * m_hold.Health * 0.02f;
        gameObject.SetActive(false);
        Debug.Log("PLayer Health: " + GameObject.FindGameObjectWithTag(_tag).GetComponent<Player2D_StatsHolder>().Health);
    }
}