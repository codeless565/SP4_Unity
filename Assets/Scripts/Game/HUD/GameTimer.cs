using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public Text TimeDisplay;
    public float Gametime = 100; //In Seconds
    private float m_elapseTime;

	public void Init (bool _isBossLevel)
    {
        if (Gametime < 10)
            Gametime = 10;
        else if (Gametime > 300)
            Gametime = 300;

        m_elapseTime = Gametime;

        if (_isBossLevel)
            m_elapseTime *= 2;
    }
	
	void Update () {
        m_elapseTime -= Time.deltaTime;
        TimeDisplay.text = "Time: " + m_elapseTime.ToString("0") + " seconds";

        if (m_elapseTime <= 0)
        {
            PlayerPrefs.SetString("KilledBy", "Time");

            if (GetComponent<CameraEffects>() != null)
                GetComponent<CameraEffects>().PlayGameOverEffect();
            else
                GetComponent<GameMode>().GameOver();
        }
    }

    // Modifiers //
    void AddTime(float _time)
    {
        m_elapseTime += _time;
    }

    void DeductTime(float _time)
    {
        m_elapseTime -= _time;

        if (m_elapseTime < 0)
            m_elapseTime = 0;
    }
}
