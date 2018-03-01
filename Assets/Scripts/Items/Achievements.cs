using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements {
    public string AchievementName;
    public string AchievementDetails;
    public bool AchievementCompleted;
    public bool AchievementActive;
    public List<AchievementsProperties> PropertiesList;
    public int CompletedProps;
    public int AchievementReward;

    public Achievements()
    {
        AchievementName = "";
        AchievementDetails = "";
        AchievementCompleted = false;
        AchievementActive = false;
        PropertiesList = new List<AchievementsProperties>();
        CompletedProps = 0;
        AchievementReward = 0;
    }

    public Achievements(string _name,string _details,bool _active,int _reward)
    {
        AchievementName = _name;
        AchievementDetails = _details;
        AchievementCompleted = false;
        AchievementActive = _active;
        PropertiesList = new List<AchievementsProperties>();
        CompletedProps = 0;
        AchievementReward = _reward;
    }

    public Achievements(string _name, string _details, bool _active, bool _completed,int _reward)
    {
        AchievementName = _name;
        AchievementDetails = _details;
        AchievementCompleted = _completed;
        AchievementActive = _active;
        PropertiesList = new List<AchievementsProperties>();
        CompletedProps = 0;
        AchievementReward = _reward;
    }

    // Update is called once per frame
    public void Update () {
        CompletedProps = 0;
        for (int i = 0; i < PropertiesList.Count; ++i)
        {
            if (PropertiesList[i].PropertyComplete)
                CompletedProps++;
        }
        if (CompletedProps == PropertiesList.Count)
        {
            AchievementCompleted = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats().gold += AchievementReward;
        }
	}
    
    public void AddProperty(AchievementsProperties _property)
    {
        _property.PropertyName = _property.PropertyName.ToUpper();
        PropertiesList.Add(_property);
    }
}
