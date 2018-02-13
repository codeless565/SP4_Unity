using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    // Singleton
    private static ItemManager instance;
    private ItemManager() { Awake(); }
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ItemManager();
            }
            return instance;
        }
    }


    public List<ItemWeapons> WeaponList = new List<ItemWeapons>();
    public List<ItemUses> UsesList = new List<ItemUses>();

    public ItemUses HPpotion;
    public ItemUses MPpotion;
    public ItemWeapons Sword;

    // Use this for initialization
    void Awake()
    {
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());
        WeaponList.Add(new Sword());


        UsesList.Add(HPpotion);
        UsesList.Add(MPpotion);
    }
}