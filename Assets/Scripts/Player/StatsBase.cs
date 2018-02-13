using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stats of Objects */
public interface StatsBase
{
    int Level
    {
        get;set;
    }
    string Name
    {
        get;
    }
    int Health
    {
        get;set;
    }
    float Attack
    {
        get;set;
    }
    float MoveSpeed
    {
        get;set;
    }

}
