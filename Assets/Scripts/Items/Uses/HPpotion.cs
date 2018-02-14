using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPpotion : MonoBehaviour, ItemUses
{
    Sprite ItemImage;
    bool used;

    public HPpotion()
    {
        ItemImage = GameObject.FindGameObjectWithTag("UsesObjectHolder").GetComponent<UsesObjectHolder>().HPpotion;
    }



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
            return 0;
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
            return 0;
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

    public Sprite getItemImage()
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