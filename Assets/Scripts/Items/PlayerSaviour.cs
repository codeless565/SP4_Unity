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
        foreach (Item item in _inventory)
        {
            string tempitem = item.Name + "," + item.ItemRarity;
            PlayerPrefs.SetString("item " + TotalItems, tempitem);
            TotalItems++;
        }

        PlayerPrefs.SetInt("NumStoredItems", TotalItems);
    }

    /* Save Achievements */
    public void SavePref(Dictionary<string,Achievements> _achievements)
    {
        int TotalAchievements = 0;

        foreach (KeyValuePair<string,Achievements> ach in _achievements)
        {
            string temppropname="";
            foreach(AchievementsProperties prop in ach.Value.PropertiesList)
            {
                temppropname += prop.PropertyName+",";
            }

            string tempachievement = ach.Value.AchievementName + "," + ach.Value.AchievementDetails + "," + ach.Value.AchievementActive + "," + ach.Value.AchievementCompleted+"," + ach.Value.AchievementReward+","+temppropname;
            PlayerPrefs.SetString("achievement " + TotalAchievements, tempachievement);
            TotalAchievements++;
        }

        PlayerPrefs.SetInt("NumStoredAchievements", TotalAchievements);
    }

    /* Save Achievements Properties */
    public void SavePref(Dictionary<string, AchievementsProperties> _achievements)
    {
        int TotalProperties = 0;

        foreach (KeyValuePair<string, AchievementsProperties> ach in _achievements)
        {
            string tempproperty = ach.Value.PropertyName + "," + ach.Value.PropertyDetails + "," + ach.Value.Counter + "," + ach.Value.CompletionCounter + "," + ach.Value.PropertyComplete;
            PlayerPrefs.SetString("property " + TotalProperties, tempproperty);
            TotalProperties++;
        }

        PlayerPrefs.SetInt("NumStoredProperties", TotalProperties);
    }

    /* Save Stats */
    public void SavePref(Player2D_StatsHolder _stats)
    {
        string tempstring = _stats.Name    + "," + _stats.Level   + "," + _stats.Health    + "," + _stats.Attack + "," 
                          + _stats.Stamina + "," + _stats.Defense + "," + _stats.movespeed + "," + _stats.gold   + "," 
                          + _stats.EXP     + "," + _stats.MaxEXP  + "," + _stats.MaxHealth + "," + _stats.MaxStamina;

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

        float.TryParse(statstring[1], out tempvalue); //Level
        _stats.Level = (int)tempvalue;
        float.TryParse(statstring[2], out tempvalue); //HP
        _stats.Health = tempvalue;
        float.TryParse(statstring[3], out tempvalue); //Attack
        _stats.Attack = tempvalue;
        float.TryParse(statstring[4], out tempvalue); //Stamina
        _stats.Stamina = tempvalue;
        float.TryParse(statstring[5], out tempvalue); //Defense
        _stats.Defense = tempvalue;
        float.TryParse(statstring[6], out tempvalue); //MovementSpeed
        _stats.MoveSpeed = tempvalue;
        float.TryParse(statstring[7], out tempvalue); //gold
        _stats.gold = (int)tempvalue;
        float.TryParse(statstring[8], out tempvalue); //EXP
        _stats.EXP = tempvalue;
        float.TryParse(statstring[9], out tempvalue); //MaxEXP
        _stats.MaxEXP= tempvalue;
        float.TryParse(statstring[10], out tempvalue); //MaxHP
        _stats.MaxHealth= tempvalue;
        float.TryParse(statstring[11], out tempvalue); //MaxStamina
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
    
    /* Load Achievements */
    public void LoadAchievements(Dictionary<string,Achievements> _achievement)
    {
        int TotalAchievements = PlayerPrefs.GetInt("NumStoredAchievements");

        if (TotalAchievements > 0)
        {
            for (int i = 0; i < TotalAchievements; ++i)
            {
                string[] tempitem = PlayerPrefs.GetString("achievement " + i).Split(new char[] { ',' });

                bool tempb;
                bool.TryParse(tempitem[2], out tempb);
                bool tempb2;
                bool.TryParse(tempitem[3], out tempb2);
                int temp;
                int.TryParse(tempitem[4], out temp);

                Achievements newAch = new Achievements(tempitem[0],tempitem[1],tempb,tempb2,temp);

                if (tempitem[5] != "")
                {
                    for (int j=5;j<tempitem.Length;++j)
                    {
                        if (tempitem[j] == "")
                            break;

                        AchievementsProperties achprop = GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(tempitem[j]);
                        newAch.AddProperty(achprop);
                    }
                }

                _achievement.Add(newAch.AchievementName,newAch);
            }
        }
    }

    /* Load Properties */
    public void LoadProperties(Dictionary<string, AchievementsProperties> _properties)
    {
        int TotalProperties = PlayerPrefs.GetInt("NumStoredProperties");

        if (TotalProperties > 0)
        {
            for (int i = 0; i < TotalProperties; ++i)
            {
                string[] tempitem = PlayerPrefs.GetString("property " + i).Split(new char[] { ',' });

                
                float temp2 = 0.0f;
                float.TryParse(tempitem[2], out temp2);
                float temp3 = 0.0f;
                float.TryParse(tempitem[3], out temp3);
                bool tempb;
                bool.TryParse(tempitem[4], out tempb);

                AchievementsProperties newProp = new AchievementsProperties(tempitem[0],tempitem[1],temp2,temp3,tempb);

                _properties.Add(newProp.PropertyName, newProp);
            }
        }
    }
}
