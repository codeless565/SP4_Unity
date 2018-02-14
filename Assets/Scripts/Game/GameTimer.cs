using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    [SerializeField]
    GameObject Level;   //Floor of the game<parent of every other object>

    public Text TimeDisplay;
    public float m_Gametime = 500; //In Seconds

	// Use this for initialization
	void Start () {
        if (m_Gametime <= 10)
            m_Gametime = 10;
	}
	
	// Update is called once per frame
	void Update () {
        m_Gametime -= Time.deltaTime;
        TimeDisplay.text = "Time: " + m_Gametime.ToString("0") + " seconds";

        if (m_Gametime <= 0)
        {
            gameObject.GetComponent<GameMode>().GameOver();
        }

    }
}
