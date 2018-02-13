using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stats of Player ( Health, Att Power, Def Power etc )
public class PlayerStatsMenu : MonoBehaviour
{
    //[SerializeField]
    //public Canvas statsMenu;

    [SerializeField]
    public Text sampleText;

    void SetPlayerStats()
    {
        //sampleText.text = "Name : " + PlayerManager.GetPlayerName() + "\n" + 
        //                    "Health : " + PlayerManager.GetPlayerHealth() + "\n" + 
        //                     "Att : " + PlayerManager.GetPlayerAttack() + "\n" + 
        //                     "Def : " + PlayerManager.GetPlayerDefense() + "\n";

        sampleText.text = "Name : " + GetComponent<PlayerManager>().GetName() + "\n";
    }


    // Use this for initialization
    void Start ()
    {
        SetPlayerStats();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Opening of Stats Menu
    //void OpenMenu()
    //{

    //}

    //void CloseMenu()
    //{

    //}

}
