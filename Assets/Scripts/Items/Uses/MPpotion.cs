﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPpotion : MonoBehaviour, ItemUses
{
    Sprite ItemImage = GameObject.Find("UsesObjectHolder").GetComponent<UsesObjectHolder>().MPpotion;
    bool used;

    public bool isUsed
    {
        get
        {
            return used;
        }

        set
        {
            used = value;
        }
    }

    public int getCost()
    {
        return 500;
    }

    public Sprite getItemImage()
    {
        return ItemImage;
    }

    public string getType()
    {
        return "Uses";
    }



    public int Level
    {
        get
        {
            return 0;
        }

        set
        {
            Level = value;
        }
    }

    public string Name
    {
        get
        {
            return "MPpotion";
        }
    }

    public int Health
    {
        get
        {
            return Health;
        }

        set
        {
            Health = value;
        }
    }

    public float Attack
    {
        get
        {
            return Attack;
        }

        set
        {
            Attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return Defense;
        }

        set
        {
            Defense = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return MoveSpeed;
        }

        set
        {
            MoveSpeed = value;
        }
    }
}