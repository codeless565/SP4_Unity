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
        TextLayout = new GameObject[8];

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
                string temp =   GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(i.Value.PropertiesList[0].PropertyName).Counter + "/" +
                                GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().GetProperty(i.Value.PropertiesList[0].PropertyName).CompletionCounter;
                for (int j = 0; j < TextLayout.Length; ++j)
                {
                    if (TextLayout[j].GetComponent<Text>().text == "")
                    {
                        TextLayout[j].GetComponent<Text>().text = i.Value.AchievementName + "\n \t"
                                                                  + i.Value.AchievementDetails + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"
                                                                  + temp; ;
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
