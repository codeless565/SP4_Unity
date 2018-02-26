using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public Text TimeDisplay;
    public float m_Gametime = 500; //In Seconds

	void Init () {
        if (m_Gametime <= 10)
            m_Gametime = 10;
	}
	
	void Update () {
        m_Gametime -= Time.deltaTime;
        TimeDisplay.text = "Time: " + m_Gametime.ToString("0") + " seconds";

        if (m_Gametime <= 0)
        {
            GetComponent<GameMode>().GameOver();
        }
    }

    // Modifiers //
    void AddTime(float _time)
    {
        m_Gametime += _time;
    }

    void DeductTime(float _time)
    {
        m_Gametime -= _time;

        if (m_Gametime < 0)
            m_Gametime = 0;
    }
}
