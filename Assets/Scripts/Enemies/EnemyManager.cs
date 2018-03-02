using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, StatsBase
{
    // StatsBase Variables //
    int m_iEnemyLevel = 1;
    float m_fHealth;
    float m_fMaxHealth;
    float m_fStamina;
    float m_fMaxStamina;
    float m_fAttack;
    float m_fDefense;
    float m_fMoveSpeed = 10;

    // EXP Variables //
    public float EXPRewardScaling = 5;
    public float expReward = 1;

    // Waypoint Variables //
    public Vector3[] m_Waypoint;
    public int m_currWaypointID;

    // Leveling Variables //
    LevelingSystem m_LevelingSystem;

    // StateMachine //
    StateMachine m_StateMachine;

    // Enemy Variables //
    private Player2D_StatsHolder m_PlayerStats;
    private GameObject m_Player;
    private SpriteManager m_SpriteManager;
    private Vector3 m_EnemyDestination;
    private float m_fDistanceApart;
    private float m_fChaseRange;
    private float m_fAttackRange;
	private float m_fChangeStateTimer;
    private float m_fAttackTimer;
    private bool m_bCanAttack;
    private bool m_bCanPatrol;

    #region StatsBaseSetterANDGetter
    // StatsBase Setter & Getter //
    public string Name
    {
        get
        {
            return "Enemy";
        }

        set
        {
            return;
        }
    }
    public int Level
    {
        get
        {
            return m_iEnemyLevel;
        }

        set
        {
            m_iEnemyLevel = value;
        }
    }
    public float EXP
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }
    public float MaxEXP
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }
    public float Health
    {
        get
        {
            return m_fHealth;
        }

        set
        {
            m_fHealth = value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return m_fMaxHealth;
        }

        set
        {
            m_fMaxHealth = value;
        }
    }
    public float Stamina
    {
        get
        {
            return m_fStamina;
        }

        set
        {
            m_fStamina = value;
        }
    }
    public float MaxStamina
    {
        get
        {
            return m_fMaxStamina;
        }

        set
        {
            m_fMaxStamina = value;
        }
    }
    public float Attack
    {
        get
        {
            return m_fAttack;
        }

        set
        {
            m_fAttack = value;
        }
    }
    public float Defense
    {
        get
        {
            return m_fDefense;
        }

        set
        {
            m_fDefense = value;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return m_fMoveSpeed;
        }

        set
        {
            m_fMoveSpeed = value;
        }
    }
    #endregion

    // EXP Setter //
    public float EXPReward
    {
        set { expReward = value; }
    }
    public Vector3[] Waypoint
    {
        set { m_Waypoint = value; }
    }
    public int CurrWaypointID
    {
        set { m_currWaypointID = value; }
    }

    #region VariablesSetterANDGetter
    // Variables Setter & Getter //
    // Player
    public void SetPlayerStats(Player2D_StatsHolder _statsHolder)
    {
        m_PlayerStats = _statsHolder;
    }
    public Player2D_StatsHolder GetPlayerStats()
    {
        return m_PlayerStats;
    }
    public void SetPlayer(GameObject _player)
    {
        m_Player = _player;
    }
    public GameObject GetPlayer()
    {
        return m_Player;
    }
    // Sprite Manager
    public void SetSpriteManager(SpriteManager _sprite)
    {
        m_SpriteManager = _sprite;
    }
    public SpriteManager GetSpriteManager()
    {
        return m_SpriteManager;
    }
    // Enemy
    public void SetEnemyDestination(Vector3 _pos)
    {
        m_EnemyDestination = _pos;
    }
    public Vector3 GetEnemyDestination()
    {
        return m_EnemyDestination;
    }
    public void SetDistanceApart(float _distance)
    {
        m_fDistanceApart = _distance;
    }
    public float GetDistanceApart()
    {
        return m_fDistanceApart;
    }
    public void SetChaseRange(float _range)
    {
        m_fChaseRange = _range;
    }
    public float GetChaseRange()
    {
        return m_fChaseRange;
    }
    public void SetAttackRange(float _range)
    {
        m_fAttackRange = _range;
    }
    public float GetAttackRange()
    {
        return m_fAttackRange;
    }
	public void SetChangeStateTimer(float _timer)
	{
        m_fChangeStateTimer = _timer;
	}
    public float GetChangeStateTimer()
    {
        return m_fChangeStateTimer;
    }
    public void SetAttackTimer(float _timer)
    {
        m_fAttackTimer = _timer;
    }
    public float GetAttackTimer()
    {
        return m_fAttackTimer;
    }
    public void SetBoolCanAttack(bool _can)
    {
        m_bCanAttack = _can;
    }
    public bool GetBoolCanAttack()
    {
        return m_bCanAttack;
    }
    public void SetBoolCanPatrol(bool _can)
    {
        m_bCanPatrol = _can;
    }
    public bool GetBoolCanPatrol()
    {
        return m_bCanPatrol;
    }
    public void SetStateMachine(StateMachine _machine)
    {
        m_StateMachine = _machine;
    }
    public StateMachine GetStateMachine()
    {
        return m_StateMachine;
    }
    #endregion

    public void Init()
    {
        m_LevelingSystem = GetComponent<LevelingSystem>();
        m_LevelingSystem.Init(this, false);
    }

    void Update()
    {
        // Check HP if it's 0 or not.
        if(m_fHealth <= 0f)
        {
            m_PlayerStats.GetComponent<Player2D_StatsHolder>().EXP += expReward;
            GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().UpdateProperties("enemy_kill", 1);
            // Kill it if there's 0 HP
            Destroy(gameObject);
        }
    }
}
