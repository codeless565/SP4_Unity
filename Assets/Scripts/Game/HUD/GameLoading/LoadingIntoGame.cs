using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIntoGame : MonoBehaviour
{
    public float GameStartDelay = 3;
    public GameObject GameScripts;
    private bool m_isBossFloor;

	// Use this for initialization
	public void Init (GameObject _GameScripts, int _currFloor, bool _isBossFloor)
    {
        gameObject.SetActive(true);

        GameScripts = _GameScripts;
        m_isBossFloor = _isBossFloor;

        if (transform.GetChild(0).GetComponent<Text>() != null)
        {
            if (_currFloor > 0)
            {
                if (_isBossFloor)
                    transform.GetChild(0).GetComponent<Text>().text = "Entering Floor " + _currFloor.ToString() + "\nBoss Round";
                else
                    transform.GetChild(0).GetComponent<Text>().text = "Entering Floor " + _currFloor.ToString();
            }
            else
                transform.GetChild(0).GetComponent<Text>().text = "Entering Tutorial";
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameStartDelay -= Time.deltaTime;
        //Debug.Log("ScreenTime: " + 1.0f / Time.frameCount);

        if (GameStartDelay <= 0)
        {
            GameScripts.GetComponent<GameTimer>().Init();
            Destroy(gameObject);
        }
    }
}
