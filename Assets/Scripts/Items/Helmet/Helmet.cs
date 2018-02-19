using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helmet : ItemBase
{
    Sprite ItemImage;
    bool equipped;

    public Helmet()
    {
        ItemImage = GameObject.FindGameObjectWithTag("WeaponsObjectHolder").GetComponent<WeaponObjectHolder>().Helmet;
    }

    public int getCost()
    {
        return 1000;
    }

    public Sprite getItemImage()
    {
        return ItemImage;
    }

    public string getType()
    {
        return "Helmets";
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
            return "Helmet";
        }
    }

    public int Health
    {
        get
        {
            return 30;
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
            return 300;
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
            return 0;
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
            return 0.0f;
        }

        set
        {
            MoveSpeed = value;
        }
    }

}
