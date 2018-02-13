using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public List<ItemBase> items = new List<ItemBase>();
    public List<ItemWeapons> WeaponList = new List<ItemWeapons>();
    public List<ItemUses> UsesList = new List<ItemUses>();

    public ItemUses HPpotion;
    public ItemUses MPpotion;
    public ItemWeapons Sword;

    // Use this for initialization
    void Awake()
    {
        WeaponList.Add(Sword);


        UsesList.Add(HPpotion);
        UsesList.Add(MPpotion);
    }

    public ItemWeapons AddingWeapon(ItemWeapons weapon)
    {
        if (WeaponList.Contains(weapon))
            return weapon;

        return null;
    }
}