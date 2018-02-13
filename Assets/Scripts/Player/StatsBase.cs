using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stats of Objects */
public interface StatsBase
{
    string Name
    {
        get;
    }
    int Level
    {
        get;set;
    }
    int Health
    {
        get;set;
    }
    float Attack
    {
        get;set;
    }
    float Defense
    {
        get; set;
    }
    float MoveSpeed
    {
        get;set;
    }

}
