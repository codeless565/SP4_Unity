using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Holding the Stats of the Player */
public class Player2D_StatsHolder : MonoBehaviour, PlayerStatsBase
{
	/* Player Stats */
    int playerLevel = 1;
    int health = 100;
    float attack = 10;
    int mana = 10;
    float defense = 10;
    float movespeed = 10;
    public int gold = 9999999;

    private string m_name = "player2D";
    private float m_EXP = 0;
    private float m_MaxEXP = 2;
    private int m_MaxHealth = 100;
    private int m_MaxMana = 10;

    /* Setters and Getters */
    public string Name
    {
        get
        {
            return m_name;
        }
        set
        {
            m_name = value;
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

    public float EXP
    {
        get
        {
            return m_EXP;
        }

        set
        {
            m_EXP = value;
        }
    }

    public float MaxEXP
    {
        get
        {
            return m_MaxEXP;
        }
        set
        {
            m_MaxEXP = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return m_MaxHealth;
        }
        set
        {
            m_MaxHealth = value;
        }
    }

    public int MaxMana
    {
        get
        {
            return m_MaxMana;
        }
        set
        {
            m_MaxMana = value;
        }
    }


    /* Print Debug Information */
    public void DebugPlayerStats()
    {
        Debug.Log("Name : " + Name);
        Debug.Log("Level : " + Level);
        Debug.Log("playerHealth : " + Health);
        Debug.Log("playerMaxHealth : " + MaxHealth);
        Debug.Log("exp : " + m_EXP);
        Debug.Log("max exp : " + m_MaxEXP);
        Debug.Log("mp : " + mana);
        Debug.Log("max mp : " + m_MaxMana);
        Debug.Log("Att : " + Attack);
        Debug.Log("Def : " + Defense);
        Debug.Log("MoveSpeed : " + MoveSpeed);
        Debug.Log("Gold : " + gold.ToString());
    }
}
