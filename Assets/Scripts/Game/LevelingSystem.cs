using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    public float MaxEXPScaling = 10;
    public float HealthScaling = 10;
    public float StaminaScaling = 10;
    public float AttackScaling = 5;
    public float DefenseScaling = 5;

    private bool isPlayer;

    // Use this for initialization
    public void Init(StatsBase _stats, bool _isPlayer = false)
    {
        isPlayer = _isPlayer;

        if (_isPlayer)
        {
            if (!LoadStats()) // Check Loadable stats
                CalculateStats(_stats);
        }
        else
        {
            CalculateStats(_stats);
        }
    }

    // Update is called once per frame
    public void UpdateStats(StatsBase _stats)
    {
        if (isPlayer)
            if (_stats.EXP >= _stats.MaxEXP)
            {
                _stats.EXP -= _stats.MaxEXP;
                ++_stats.Level;
                CalculateStats(_stats);
            }
    }

    private bool LoadStats()
    {
        /* Check if there is an available save in PlayerPref to load stats from. */
        return PlayerPrefs.GetString("Player_Stats") != "";
    }

    private void CalculateStats(StatsBase _stats)
    {
        _stats.MaxEXP = _stats.Level * MaxEXPScaling;
        _stats.MaxHealth = _stats.Level * HealthScaling;
        _stats.MaxStamina = _stats.Level * StaminaScaling;
        _stats.Attack = _stats.Level * AttackScaling;
        _stats.Defense = _stats.Level * DefenseScaling;
        _stats.Health = _stats.MaxHealth;
        _stats.Stamina = _stats.MaxStamina;

        //Debug.Log("Name : " + m_Stats.Name);
        //Debug.Log("Level : " + _stats.Level);
        //Debug.Log("EXP : " + _stats.EXP);
        //Debug.Log("Max EXP : " + _stats.MaxEXP);
        //Debug.Log("HP : " + m_Stats.Health);
        //Debug.Log("Max HP : " + _stats.MaxHealth);
        //Debug.Log("Stamina : " + m_Stats.Stamina);
        //Debug.Log("Max Stamina : " + m_Stats.MaxStamina);
        //Debug.Log("Attack : " + m_Stats.Attack);
        //Debug.Log("Defense : " + m_Stats.Defense);
    }
}
