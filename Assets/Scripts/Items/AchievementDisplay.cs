using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour {
    public GameObject MainUICanvas;
    public GameObject DisplayCanvas;
    private GameObject TextPrefab;
    bool CanvasActive;

    GameObject[] TextLayout;

	// Use this for initialization
	void Start () {
        CanvasActive = false;
        TextLayout = new GameObject[GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().AchievementsList.Count];

        TextPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().TextPrefab;
        for(int i =0;i<TextLayout.Length;++i)
        {
            TextLayout[i] = Instantiate(TextPrefab, DisplayCanvas.transform);
            TextLayout[i].GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        }
	}
	
	// Update is called once per frame
	void Update () {
        MainUICanvas.SetActive(CanvasActive);
        DisplayCanvas.SetActive(CanvasActive);
	}

    public void Reset()
    {
        for (int j = 0; j < TextLayout.Length; ++j)
        {
            TextLayout[j].GetComponent<Text>().text = "";
        }
    }
    public void ShowAchievements()
    {
        Reset();
        foreach (KeyValuePair<string,Achievements> i in GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().AchievementsList)
        {
            if (i.Value.AchievementActive)
            {
                string temp = "";
                for (int k=0;k<i.Value.PropertiesList.Count;++k)
                {
                    string tempp = " \t" + GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(i.Value.PropertiesList[k].PropertyName).PropertyDetails + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" +
                                GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(i.Value.PropertiesList[k].PropertyName).Counter + "/" +
                                GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(i.Value.PropertiesList[k].PropertyName).CompletionCounter + "\n";
                    temp += tempp;   
                }

                
                for (int j = 0; j < TextLayout.Length; ++j)
                {
                    if (TextLayout[j].GetComponent<Text>().text == "")
                    {
                        TextLayout[j].GetComponent<Text>().text = i.Value.AchievementName + "\n \t"
                                                                  + i.Value.AchievementDetails + "\n"
                                                                  + temp;
                        
                        break;
                    }
                }
            }
        }

    }

    public void OpenCanvas()
    {
        CanvasActive = true;
        ShowAchievements();
    }
    public void CloseCanvas()
    {
        CanvasActive = false;
    }
}
