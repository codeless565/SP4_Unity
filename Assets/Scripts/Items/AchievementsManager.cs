﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour {
    public Dictionary<string, AchievementsProperties> PropertiesList;
    public Dictionary<string, Achievements> AchievementsList;

    // Use this for initialization
    void Start () {
        PropertiesList = new Dictionary<string, AchievementsProperties>();
        AchievementsList = new Dictionary<string, Achievements>();

        //Achievements ach1 = new Achievements("testachievement","testingonlyrelak",false);
        //AchievementsList.Add(ach1.AchievementName, ach1);
        //ach1.AchievementActive = true;

        //AchievementsProperties achprop1 = new AchievementsProperties("Kill_Enemy", 3);
        //AddProperties("testachievement", achprop1);
        Load();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<string,Achievements> ach in AchievementsList)
        {
            if (ach.Value.AchievementActive)
            {
                ach.Value.Update();
                foreach(AchievementsProperties props in ach.Value.PropertiesList)
                {
                    props.Update();
                }

                if (ach.Value.AchievementCompleted)
                {
                    ach.Value.AchievementActive = false;
                    GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement(ach.Value.AchievementName + " has been completed!");
                }
            }

        }        
    }

    public void AddProperties(string achievement,AchievementsProperties achievementproperty)
    {
        if (!AchievementsList.ContainsKey(achievement))
            return;

        if (PropertiesList.ContainsKey(achievementproperty.PropertyName))
        {
            AchievementsList[achievement].AddProperty(achievementproperty);           
        }
        else
        {
            PropertiesList.Add(achievementproperty.PropertyName, achievementproperty);
            AchievementsList[achievement].AddProperty(achievementproperty);
        }
    }

    public void UpdateProperties(string _propname, int value)
    {
        if (!PropertiesList.ContainsKey(_propname))
            return;

        PropertiesList[_propname].AddCounter(value);
    }

    void Load()
    {
        TextAsset AchievementFile = Resources.Load<TextAsset>("Achievements");
        string[] achievementsstring = AchievementFile.text.Split(new char[] { '\n' });

        for (int i = 0; i < achievementsstring.Length; ++i) 
        {
            string[] tempString = achievementsstring[i].Split(new char[] { ',' });
            bool tempActive;
            bool.TryParse(tempString[2], out tempActive);
            Achievements newAchievement = new Achievements(tempString[0], tempString[1], tempActive);
            float tempCount;
            float.TryParse(tempString[4], out tempCount);
            AchievementsProperties ChildProperty = new AchievementsProperties(tempString[3],tempCount);
            newAchievement.AddProperty(ChildProperty);

            AchievementsList.Add(newAchievement.AchievementName, newAchievement);
            PropertiesList.Add(ChildProperty.PropertyName, ChildProperty);
        }
    }
}
