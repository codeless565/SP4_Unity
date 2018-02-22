using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem
{
    private StatsBase m_Stats;

    public float MaxEXPScaling = 10;
    public float HealthScaling = 10;
    public float StaminaScaling = 10;
    public float AttackScaling = 5;
    public float DefenseScaling = 5;

    // Use this for initialization
    public void Init (StatsBase _stats) {
        m_Stats = _stats;

        CalculateStats(m_Stats.Level);
    }

    // Update is called once per frame
    void Update ()
    {
        m_Stats.EXP += Time.deltaTime;

        if (m_Stats.EXP >= m_Stats.MaxEXP)
        {
            m_Stats.EXP -= m_Stats.MaxEXP;
            ++m_Stats.Level;
            CalculateStats(m_Stats.Level);
        }
	}

    private void CalculateStats(int _Level)
    {
        m_Stats.MaxEXP      = _Level * MaxEXPScaling;
        m_Stats.MaxHealth   = _Level * HealthScaling;
        m_Stats.MaxStamina  = _Level * StaminaScaling;
        m_Stats.Attack      = _Level * AttackScaling;
        m_Stats.Defense     = _Level * DefenseScaling;
        m_Stats.Health      = m_Stats.MaxHealth;
        m_Stats.Stamina     = m_Stats.MaxStamina;
    }
}
