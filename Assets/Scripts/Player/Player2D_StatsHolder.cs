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
    float health;
    float attack;
    float stamina;
    float defense;
    public float movespeed;
    public int gold = 9999999;

    private string m_name = "player2D";
    private float m_EXP;
    private float m_MaxEXP;
    private float m_MaxHealth;
    private float m_MaxStamina;

    LevelingSystem levelingSystem;

    float hpcheck;

    private float m_timer = 5.0f;

    /* Setters and Getters */
    #region StatsSetterAndGetter

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

    #endregion

    /* Initializing of Stats */
    void Awake()
    {
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, true);

        /* Stats will be updated accordingly with the leveling system with function <LevelingSystem.Update()> */
    }
    
    void Update()
    {
        hpcheck = health;
        levelingSystem.UpdateStats(this); //if player levelsup, it will refresh hp, for now

        /* If Stamina is not full, regen some Stamina over time */
        if (stamina <= m_MaxStamina)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0.0f)
            {
                ++stamina;
                m_timer = 5.0f;
            }
        }
    }

    void LateUpdate()
    {
        /* Check if player has received damaged in this frame, if so, play animation on profile and flash Red */
        if (health < hpcheck)
        {
            if (GameObject.FindGameObjectWithTag("PlayerProfileDamage") == null)
            {
                GameObject Profile = GameObject.FindGameObjectWithTag("PlayerProfileHUD");
                if (Profile != null)
                {
                    GameObject Aliment = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ProfileAlimentDamage;
                    Aliment = Instantiate(Aliment, Profile.transform);
                    Aliment.GetComponent<PlayerProfileStatusAliment>().Init(1);
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("PlayerProfileDamage").GetComponent<PlayerProfileStatusAliment>().KillTime = 1;
            }
        }
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
