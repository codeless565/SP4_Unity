using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaviour{
    private static PlayerSaviour instance;
    public static PlayerSaviour Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerSaviour();
            return instance;
        }
    }
    private PlayerSaviour()
    {

    }

    public void SavePref(List<Item> _inventory)
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

    public void SavePref(Player2D_StatsHolder _stats)
    {
        string tempitem = _stats.Name + "," + _stats.Level + "," + _stats.Health + "," + _stats.Attack + "," +
            _stats.Stamina + "," + _stats.Defense + "," + _stats.movespeed + "," + _stats.gold +
            "," + _stats.EXP + "," + _stats.MaxEXP + "," + _stats.MaxHealth + "," + _stats.MaxStamina;

        PlayerPrefs.SetString("Player_Stats", tempitem);
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

    public Player2D_StatsHolder LoadStats()
    {
        Player2D_StatsHolder tempholder = new Player2D_StatsHolder();
        string[] statstring = PlayerPrefs.GetString("Player_Stats").Split(new char[] { ',' });
        foreach(string a in statstring)
        Debug.Log(a);
        tempholder.name = statstring[0];

        float tempvalue = 0.0f;

        float.TryParse(statstring[1], out tempvalue);
        tempholder.Level = (int)tempvalue;
        float.TryParse(statstring[2], out tempvalue);
        tempholder.Health = tempvalue;
        float.TryParse(statstring[3], out tempvalue);
        tempholder.Attack = tempvalue;
        float.TryParse(statstring[4], out tempvalue);
        tempholder.Stamina = tempvalue;
        float.TryParse(statstring[5], out tempvalue);
        tempholder.Defense = tempvalue;
        float.TryParse(statstring[6], out tempvalue);
        tempholder.MoveSpeed = tempvalue;
        float.TryParse(statstring[7], out tempvalue);
        tempholder.gold = (int)tempvalue;
        float.TryParse(statstring[8], out tempvalue);
        tempholder.EXP = tempvalue;
        float.TryParse(statstring[9], out tempvalue);
        tempholder.MaxEXP= tempvalue;
        float.TryParse(statstring[10], out tempvalue);
        tempholder.MaxHealth= tempvalue;
        float.TryParse(statstring[11], out tempvalue);
        tempholder.MaxStamina= tempvalue;

        return tempholder;
    }
}
