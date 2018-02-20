using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ItemBase : StatsBase
{
    Sprite ItemImage
    {
        get;set;
    }
    //string ItemRarity
    //{
    //    get;set;
    //}
    string ItemType
    {
        get;set;
    }
    
    int ItemCost
    {
        get;set;
    }
}

public class Item : ItemBase
{
    string _itemname;
    string _itemtype;
    int _itemlevel;
    //string _itemrarity;
    int _itemcost;
    int _health;
    int _mana;
    float _attack;
    float _defense;
    float _movespeed;
    Sprite _sprite;

    public string _spritename;

    public string Name
    {
        get
        {
            return _itemname;
        }

        set
        {
            _itemname = value;
        }
    }

    public string ItemType
    {
        get
        {
            return _itemtype;
        }

        set
        {
            _itemtype = value;
        }
    }  

    public int Level
    {
        get
        {
            return _itemlevel;
        }

        set
        {
            _itemlevel = value;
        }
    }

    //public string ItemRarity
    //{
    //    get
    //    {
    //        return _itemrarity;
    //    }

    //    set
    //    {
    //        _itemrarity = value;
    //    }
    //}

    public int ItemCost
    {
        get
        {
            return _itemcost;
        }

        set
        {
            _itemcost = value;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public int Mana
    {
        get
        {
            return _mana;
        }

        set
        {
            _mana = value;
        }
    }

    public float Attack
    {
        get
        {
            return _attack;
        }

        set
        {
            _attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return _defense;
        }

        set
        {
            _defense = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return _movespeed;
        }

        set
        {
            _movespeed = value;
        }
    }

    public Sprite ItemImage
    {
        get
        {
            return _sprite;
        }

        set
        {
            _sprite = value;
        }
    }

    public void getImage()
    {
        ItemImage = GameObject.FindGameObjectWithTag("ItemObjectHolder").GetComponent<ItemObjectHolder>().getSprite(_spritename);
    }
}