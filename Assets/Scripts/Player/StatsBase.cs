using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stats of Objects */
public interface StatsBase
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

    float Health
    {
        get; set;
    }
    float MaxHealth
    {
        get; set;
    }

    float Stamina
    {
        get; set;
    }
    float MaxStamina
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
