using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public float DelayTime2GameOver = 3;
    float elaspeTime;

    Camera m_mainCamera;
    bool m_playGameOver;

	// Use this for initialization
	public void Init(Camera _cam)
    {
        m_mainCamera = _cam;

        m_playGameOver = false;

        elaspeTime = DelayTime2GameOver;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (m_playGameOver)
        {
            if (m_mainCamera.orthographicSize > 0)
                m_mainCamera.orthographicSize -= Time.deltaTime;
            else
            {
                GetComponent<GameMode>().GameOver();
                return;
            }

            elaspeTime -= Time.deltaTime;
            if (elaspeTime <= 0)
                GetComponent<GameMode>().GameOver();
        }
	}

    public void PlayGameOverEffect()
    {
        m_playGameOver = true;
    }
}
