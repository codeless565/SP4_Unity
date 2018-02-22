using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    private StatsBase m_Stats;

    public float MaxEXPScaling = 10;
    public float HealthScaling = 10;
    public float StaminaScaling = 10;
    public float AttackScaling = 5;
    public float DefenseScaling = 5;

    private bool isPlayer;

    // Use this for initialization
    public void Init(StatsBase _stats, bool _isPlayer)
    {
        m_Stats = _stats;

<<<<<<< HEAD
        if (_isPlayer)
        {
            if (!LoadStats()) // Check Loadable stats
                CalculateStats(m_Stats.Level);
        }
        else
        {
            CalculateStats(m_Stats.Level);
        }

=======
        //CalculateStats(m_Stats.Level);
>>>>>>> 4f074525bafab6a4149f14b222fbaf162e365410
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (isPlayer)
            if (m_Stats.EXP >= m_Stats.MaxEXP)
            {
                m_Stats.EXP -= m_Stats.MaxEXP;
                ++m_Stats.Level;
                CalculateStats(m_Stats.Level);
            }
    }

    private bool LoadStats()
    {
        //Check if player has stats in save files

        return false;
    }
=======
        if (m_Stats.EXP >= m_Stats.MaxEXP)
        {
            m_Stats.EXP -= m_Stats.MaxEXP;
            ++m_Stats.Level;
            //CalculateStats(m_Stats.Level);
        }
	}
>>>>>>> 4f074525bafab6a4149f14b222fbaf162e365410

    private void CalculateStats(int _Level)
    {
        m_Stats.MaxEXP = _Level * MaxEXPScaling;
        m_Stats.MaxHealth = _Level * HealthScaling;
        m_Stats.MaxStamina = _Level * StaminaScaling;
        m_Stats.Attack = _Level * AttackScaling;
        m_Stats.Defense = _Level * DefenseScaling;
        m_Stats.Health = m_Stats.MaxHealth;
        m_Stats.Stamina = m_Stats.MaxStamina;

        //Debug.Log("Name : " + m_Stats.Name);
        //Debug.Log("Level : " + m_Stats.Level);
        //Debug.Log("EXP : " + m_Stats.EXP);
        //Debug.Log("Max EXP : " + m_Stats.MaxEXP);
        //Debug.Log("HP : " + m_Stats.Health);
        //Debug.Log("Max HP : " + m_Stats.MaxHealth);
        //Debug.Log("Stamina : " + m_Stats.Stamina);
        //Debug.Log("Max Stamina : " + m_Stats.MaxStamina);
        //Debug.Log("Attack : " + m_Stats.Attack);
        //Debug.Log("Defense : " + m_Stats.Defense);
    }
}
