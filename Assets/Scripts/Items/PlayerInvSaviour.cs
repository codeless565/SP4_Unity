using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvSaviour{
    private static PlayerInvSaviour instance;
    public static PlayerInvSaviour Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerInvSaviour();
            return instance;
        }
    }
    private PlayerInvSaviour()
    {

    }

    public void SaveInv(List<Item> _inventory)
    {
        int TotalItems = 0;
        for (int i = 0; i < _inventory.Count;++i)
        {
            TotalItems++;

            string tempitem = _inventory[i].Name + "," + _inventory[i].ItemRarity;
            PlayerPrefs.SetString("item " + i, tempitem);
        }

        PlayerPrefs.SetInt("NumStoredItems", TotalItems);
    }

    public List<Item> LoadInv()
    {
        int TotalItems = PlayerPrefs.GetInt("NumStoredItems");
        
        List<Item> tempInv = new List<Item>();
        if (TotalItems > 0)
        {
            for (int i = 0; i < TotalItems; ++i)
            {
                string[] tempitem = PlayerPrefs.GetString("item " + i).Split(new char[] { ',' });

                Item newItem = ItemDatabase.Instance.getItem(tempitem[0], tempitem[1]);
                tempInv.Add(newItem);
            }
        }

        return tempInv;
    }
}
