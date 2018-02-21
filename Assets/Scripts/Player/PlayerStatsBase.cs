using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stats of Objects */
public interface PlayerStatsBase
{
    string Name
    {
        get; set;
    }
    int Level
    {
        get; set;
    }
    float EXP
    {
        get; set;
    }
    float MaxEXP
    {
        get; set;
    }
    int Health
    {
        get; set;
    }
    int MaxHealth
    {
        get; set;
    }
    int Mana
    {
        get; set;
    }
    int MaxMana
    {
        get; set;
    }
    float Attack
    {
        get; set;
    }
    float Defense
    {
        get; set;
    }
    float MoveSpeed
    {
        get; set;
    }
}
