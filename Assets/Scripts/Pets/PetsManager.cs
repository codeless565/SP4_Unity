using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsManager : MonoBehaviour, StatsBase
{
    // StatsBase Variables //
    int m_iPetLevel = 1;
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

    // Leveling Variables //
    LevelingSystem m_LevelingSystem;

    // StateMachine //
    StateMachine m_StateMachine;

    // Pet Variables //
    private Player2D_StatsHolder m_PlayerStats;
    private GameObject m_Player;
    private Vector3 m_PetDestination;
    private float m_fDistanceApart;
    private float m_fFollowRange;
    private float m_fGuardRange;

    #region StatsBaseSetterANDGetter
    // StatsBase Setter & Getter //
    public string Name
    {
        get
        {
            return "Pet";
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
            return m_iPetLevel;
        }

        set
        {
            m_iPetLevel = value;
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
    // Pet
    public void SetPetDestination(Vector3 _pos)
    {
        m_PetDestination = _pos;
    }
    public Vector3 GetPetDestination()
    {
        return m_PetDestination;
    }
    public void SetDistanceApart(float _distance)
    {
        m_fDistanceApart = _distance;
    }
    public float GetDistanceApart()
    {
        return m_fDistanceApart;
    }
    public void SetFollowRange(float _range)
    {
        m_fFollowRange = _range;
    }
    public float GetFollowRange()
    {
        return m_fFollowRange;
    }
    public void SetGuardRange(float _range)
    {
        m_fGuardRange = _range;
    }
    public float GetGuardRange()
    {
        return m_fGuardRange;
    }
    // State Machine
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
        // Check HP if it's 0 or not
        if(m_fHealth <= 0f)
        {
            // Go into Recovery State.
        }
    }
}
