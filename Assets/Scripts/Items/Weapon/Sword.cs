using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : ItemWeapons
{
    Sprite ItemImage;
    bool equipped;

    public Sword()
    {
        ItemImage = GameObject.FindGameObjectWithTag("WeaponsObjectHolder").GetComponent<WeaponObjectHolder>().Sword;
    }

    public bool isEquipped
    {
        get
        {
            return equipped;
        }

        set
        {
            equipped = value;
        }
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
        return "Weapons";
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
            return "Sword";
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
            return 100;
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
            return MoveSpeed;
        }

        set
        {
            MoveSpeed = value;
        }
    }

}
