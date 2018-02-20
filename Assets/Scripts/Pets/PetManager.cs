using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour, StatsBase
{
    enum PetState
    {
        GUARD,
        FOLLOW,
        PROTECT,
        ATTACK,
        RECOVERY
    }

    // Pet //
    PetState petState;

    // Stats //
    [SerializeField]
    int petLevel = 0, health = 50, mana = 0;
    [SerializeField]
    float attack = 10, defense = 0, movespeed = 10;

    // Player //


    // Stats Setter and Getter //
    public int Level
    {
        get
        {
            return petLevel;
        }

        set
        {
            petLevel = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return defense;
        }

        set
        {
            defense = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return movespeed;
        }

        set
        {
            movespeed = value;
        }
    }

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

    public int Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }


    void Start ()
    {

    }
	
	void Update ()
    {
        // Pet States
        switch (petState)
        {

        }
    }


}
