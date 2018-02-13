using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour , ItemWeapons
{
    Image ItemImage;
    bool equipped;

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

    public Image getItemImage()
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
