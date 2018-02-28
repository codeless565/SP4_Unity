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

    /* Save Equipment */
    public void SavePref(Item[] _equipment)
    {
        int TotalItems = 0;
        for (int i = 0; i < _equipment.Length; ++i)
        {
            TotalItems++;
            string tempitem = "";
            if (_equipment[i] != null)
                tempitem = _equipment[i].Name + "," + _equipment[i].ItemRarity;

            PlayerPrefs.SetString("equipment " + i, tempitem);
        }

        PlayerPrefs.SetInt("NumStoredEquipments", TotalItems);
    }

    /* Save Inventory */
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

    /* Save Stats */
    public void SavePref(Player2D_StatsHolder _stats)
    {
        string tempstring = _stats.Name    + "," + _stats.Level   + "," + _stats.Health    + "," + _stats.Attack + "," 
                          + _stats.Stamina + "," + _stats.Defense + "," + _stats.movespeed + "," + _stats.gold   + "," 
                          + _stats.EXP     + "," + _stats.MaxEXP  + "," + _stats.MaxHealth + "," + _stats.MaxStamina;

        //Debug.Log(tempstring);

        PlayerPrefs.SetString("Player_Stats", tempstring);
    }


    /* Save Controls */
    public void SavePref(KeyCode[] _controls)
    {
        string tempstring =  _controls[0] + "," +
                             _controls[1] + "," +
                             _controls[2] + "," +
                             _controls[3] + "," +
                             _controls[4] + "," +
                             _controls[5];

        PlayerPrefs.SetString("Player_Controls", tempstring);
    }

    /* Load Equipment*/
    public void LoadEquipment(Item[] _equipment)
    {
        int TotalItems = PlayerPrefs.GetInt("NumStoredEquipments");

        if (TotalItems > 0)
        {
            for (int i = 0; i < TotalItems; ++i)
            {
                string[] tempitem = PlayerPrefs.GetString("equipment " + i).Split(new char[] { ',' });
                Item newItem = null;
                if (tempitem[0] != "")
                    newItem = ItemDatabase.Instance.getItem(tempitem[0], tempitem[1]);
                _equipment[i] = newItem;
            }
        }
    }


    /* Load Inventory */
    public void LoadInv(List<Item> _inventory)
    {
        int TotalItems = PlayerPrefs.GetInt("NumStoredItems");
        
        if (TotalItems > 0)
        {
            for (int i = 0; i < TotalItems; ++i)
            {
                string[] tempitem = PlayerPrefs.GetString("item " + i).Split(new char[] { ',' });

                Item newItem = ItemDatabase.Instance.getItem(tempitem[0], tempitem[1]);
                _inventory.Add(newItem);
            }
        }
    }

    /* Load Stats */
    public void LoadPlayerStats(Player2D_StatsHolder _stats)
    {
        string[] statstring = PlayerPrefs.GetString("Player_Stats").Split(new char[] { ',' });

        _stats.Name = statstring[0];
        float tempvalue = 0.0f;

        float.TryParse(statstring[1], out tempvalue);
        _stats.Level = (int)tempvalue;
        float.TryParse(statstring[2], out tempvalue);
        _stats.Health = tempvalue;
        float.TryParse(statstring[3], out tempvalue);
        _stats.Attack = tempvalue;
        float.TryParse(statstring[4], out tempvalue);
        _stats.Stamina = tempvalue;
        float.TryParse(statstring[5], out tempvalue);
        _stats.Defense = tempvalue;
        float.TryParse(statstring[6], out tempvalue);
        _stats.MoveSpeed = tempvalue;
        float.TryParse(statstring[7], out tempvalue);
        _stats.gold = (int)tempvalue;
        float.TryParse(statstring[8], out tempvalue);
        _stats.EXP = tempvalue;
        float.TryParse(statstring[9], out tempvalue);
        _stats.MaxEXP= tempvalue;
        float.TryParse(statstring[10], out tempvalue);
        _stats.MaxHealth= tempvalue;
        float.TryParse(statstring[11], out tempvalue);
        _stats.MaxStamina= tempvalue;
    }

    /* Load Controls */
    public void LoadControls(KeyCode[] _controls)
    {
        string[] controlstring = PlayerPrefs.GetString("Player_Controls").Split(new char[] { ',' });

        for (int i = 0; i < _controls.Length; ++i)
        {
            _controls[i] = GameObject.FindGameObjectWithTag("GameScript").GetComponent<ControlsManager>().ReturnKey(controlstring[i]);
        }
    }
}
