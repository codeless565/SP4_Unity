using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatsManager : MonoBehaviour, StatsBase
{
    int m_level;
    float m_exp;
    float m_maxExp;
    float m_health;
    float m_maxHealth;
    float m_stamina;
    float m_maxStamina;
    float m_attack;
    float m_defense;
    float m_movementSpeed;

    public float ExpRewardScaling = 10;
    float expReward;

    LevelingSystem levelingSystem;

    StateMachine m_StateMachine;

    #region StatsGetterNSetter
    public string Name
    {
        get
        {
            return "Boss";
        }

        set
        {
            //it does nothing
        }
    }

    public int Level
    {
        get
        {
            return m_level;
        }

        set
        {
            m_level = value;
        }
    }

    public float EXP
    {
        get
        {
            return m_exp;
        }

        set
        {
            m_exp = value;
        }
    }

    public float MaxEXP
    {
        get
        {
            return m_maxExp;
        }

        set
        {
            m_maxExp = value;
        }
    }

    public float Health
    {
        get
        {
            return m_health;
        }

        set
        {
            m_health = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return m_maxHealth;
        }

        set
        {
            m_maxHealth = value;
        }
    }

    public float Stamina
    {
        get
        {
            return m_stamina;
        }

        set
        {
            m_stamina = value;
        }
    }

    public float MaxStamina
    {
        get
        {
            return m_maxStamina;
        }

        set
        {
            m_maxStamina = value;
        }
    }

    public float Attack
    {
        get
        {
            return m_attack;
        }

        set
        {
            m_attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return m_defense;
        }

        set
        {
            m_defense = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return m_movementSpeed;
        }

        set
        {
            m_movementSpeed = value;
        }
    }

    public float EXPReward
    {
        set { expReward = value; }
    }

    public StateMachine SM
    {
        get { return m_StateMachine; }
    }
    #endregion

    Player2D_StatsHolder m_playerStats;

    float hpCheck;
    bool dead;

    // Use this for initialization
    public void Init(int _level)
    {
        m_playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();

        m_level = _level;
        levelingSystem = GetComponent<LevelingSystem>();
        levelingSystem.Init(this, false);

        /* Initialize State Machine with its states */
        m_StateMachine = new StateMachine();
        m_StateMachine.AddState(new StateBossIdle("StateIdle", gameObject));
        m_StateMachine.AddState(new StateBossAttack("StateAttack", gameObject));
        m_StateMachine.AddState(new StateBossRage("StateRage", gameObject));
        m_StateMachine.SetNextState("StateIdle");

        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpCheck = m_health;
        if (m_health <= 0)
        {
            /* Kill Boss if he has no more hp 
               and spawn royalChests
             */
            DeathAnimation();

            if (!dead)
            {
                GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement("CONGRADULATION!");
                GetComponent<BossDefeatSpawn>().InitSpawn(transform.position);
                dead = true;
            }
            return;
            //Destroy(gameObject);
        }

        m_StateMachine.Update();
    }

    void LateUpdate()
    {
        if (m_health != hpCheck)
        {
            Debug.Log("Boss HP: " + m_health);
            //can do flash red
        }
    }


    void DeathAnimation()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, GetComponent<SpriteRenderer>().color.a - Time.deltaTime);

        if (GetComponent<SpriteRenderer>().color.a <= 0)
            Destroy(gameObject);
    }

}
