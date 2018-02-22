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
    string ItemRarity
    {
        get; set;
    }
    string ItemType
    {
        get;set;
    }
    
    int ItemCost
    {
        get;set;
    }
    int Quantity { get; set; }
}

public class Item : ItemBase
{
    string _itemname;
    string _itemtype;
    int _itemlevel;
    string _itemrarity;
    int _itemcost;
    int _health;
    int _mana;
    float _attack;
    float _defense;
    float _movespeed;
    Sprite _sprite;
    int _quantity;

    public Item()
    {
        _itemname = "";
        _itemtype = "";
        _itemlevel = 0;
        _itemrarity = "";
        _itemcost = 0;
        _health = 0;
        _mana = 0;
        _attack = 0.0f;
        _defense = 0.0f;
        _movespeed = 0.0f;
        _sprite = null;
        _quantity = 1;
    }

    public Item(Item copy)
    {
        _itemname = copy.Name;
        _itemtype = copy.ItemType;
        _itemlevel = copy.Level;
        _itemrarity = copy.ItemRarity;
        _itemcost = copy.ItemCost;
        _health = copy.Health;
        _mana = copy.Mana;
        _attack = copy.Attack;
        _defense = copy.Defense;
        _movespeed = copy.MoveSpeed;
        _sprite = copy._sprite;
        _quantity = 1;
    }

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

    public string ItemRarity
    {
        get
        {
            return _itemrarity;
        }

        set
        {
            _itemrarity = value;
        }
    }

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

    public int Quantity
    {
        get
        {
            return _quantity;
        }

        set
        {
            _quantity = value;
        }
    }

    public void getImage()
    {
        ItemImage = GameObject.FindGameObjectWithTag("Holder").GetComponent<ItemObjectHolder>().getSprite(_spritename);
    }
}