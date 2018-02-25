using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
/* Holding the Stats of the Player */
public class Player2D_StatsHolder : MonoBehaviour, StatsBase
{
	/* Player Stats */
    public int playerLevel = 1;
    float health = 100;
    float attack = 10;
    float stamina = 10;
    float defense = 10;
    public float movespeed = 10;
    public int gold = 9999999;

    private string m_name = "player2D";
    private float m_EXP = 0;
    private float m_MaxEXP = 10;
    private float m_MaxHealth = 100;
    private float m_MaxStamina = 10;

    LevelingSystem levelingSystem;


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

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
			//healthBar.Value = health;
        }
    }
    public float MaxHealth
    {
        get
        {
            return m_MaxHealth;
        }
        set
        {
            m_MaxHealth = value;
			//healthBar.MaxValue = m_MaxHealth;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }
    public float MaxStamina
    {
        get
        {
            return m_MaxStamina;
        }
        set
        {
            m_MaxStamina = value;
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
		

    /* Initializing of Stats */
    void Awake()
    {
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, true);
		this.MaxHealth = m_MaxHealth;
		this.Health = health;
        /* Stats will be updated accordingly with the leveling system with function <LevelingSystem.Update()> */
    }
    
    void Update()
    {
        m_EXP += Time.deltaTime;
        levelingSystem.UpdateStats(this);
    }

    /* Print Debug Information */
    public void DebugPlayerStats()
    {
        Debug.Log("Name : " + Name + "\n" 
                 + "Level : " + Level + "\n" 
                 + "playerHealth : " + Health + "\n" 
                 + "playerMaxHealth : " + MaxHealth + "\n"
                 + "exp : " + m_EXP + "\n"
                 + "Max EXP : " + m_MaxEXP + "\n" 
                 + "stamina : " + stamina + "\n"
                 + "max stamina : " + m_MaxStamina + "\n" 
                 + "Att : " + Attack + "\n"
                 + "Def : " + Defense + "\n"
                 + "MoveSpeed : " + MoveSpeed + "\n"
                 + "Gold : " + gold.ToString());
    }
}
