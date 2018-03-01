using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour {
    public Dictionary<string, AchievementsProperties> PropertiesList = new Dictionary<string, AchievementsProperties>();
    public Dictionary<string, Achievements> AchievementsList = new Dictionary<string, Achievements>();

    // Use this for initialization
    public void Init() {
        if (PlayerPrefs.GetInt("NumStoredAchievements") == 0)
        {
            Load();
        }
        else
        {
            PlayerSaviour.Instance.LoadProperties(PropertiesList);
            PlayerSaviour.Instance.LoadAchievements(AchievementsList);
        }
    }
	// Update is called once per frame
	void Update ()
    {
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
    public AchievementsProperties GetProperty(string _propname)
    {
        if (PropertiesList.ContainsKey(_propname))
            return PropertiesList[_propname];

        return null;
    }

    public void UpdateProperties(string _propname, int value)
    {
        if (!PropertiesList.ContainsKey(_propname.ToUpper()))
            return;

        PropertiesList[_propname.ToUpper()].AddCounter(value);
    }

    void Load()
    {
        TextAsset AchievementFile = Resources.Load<TextAsset>("Achievements");
        string[] achievementsstring = AchievementFile.text.Split(new char[] { '\n' });

        for (int i = 1; i < achievementsstring.Length-1; ++i) 
        {
            string[] tempString = achievementsstring[i].Split(new char[] { ',' });
            
            if (tempString[0] == "")
                break;
            
            bool tempActive=false;
            bool.TryParse(tempString[2], out tempActive);
            int temp = 0;
            int.TryParse(tempString[3], out temp);
            
            Achievements newAchievement = new Achievements(tempString[0], tempString[1], tempActive,temp);

            for (int j = 4; j<tempString.Length;j+=3)
            {
                if (tempString[j] != "")
                {
                    AchievementsProperties ChildProperty = new AchievementsProperties();
                    float tempCount = 0.0f;
                    float.TryParse(tempString[j + 2], out tempCount);
                    ChildProperty.PropertyName = tempString[j];
                    ChildProperty.PropertyDetails = tempString[j + 1];
                    ChildProperty.CompletionCounter = tempCount;
                    newAchievement.AddProperty(ChildProperty);
                    PropertiesList.Add(ChildProperty.PropertyName, ChildProperty);
                }
            }          
            AchievementsList.Add(newAchievement.AchievementName, newAchievement);
        }
    }
}
