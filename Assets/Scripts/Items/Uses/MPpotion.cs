using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPpotion : MonoBehaviour, ItemUses
{
    Sprite ItemImage;
    bool used;

    public MPpotion()
    {
        ItemImage = GameObject.FindGameObjectWithTag("UsesObjectHolder").GetComponent<UsesObjectHolder>().MPpotion;
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
            return 0.0f;
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
            return 0.0f;
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