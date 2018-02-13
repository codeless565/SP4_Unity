using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPpotion : MonoBehaviour, ItemUses
{
    Image ItemImage;
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
            return "HPpotion";
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

    public Image getItemImage()
    {
        return ItemImage;
    }

    public string getType()
    {
        return "Uses";
    }

    public int getCost()
    {
        return 500;
    }

}