using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIntoGame : MonoBehaviour
{
    public float GameStartDelay = 3;
    public GameObject GameScripts;

	// Use this for initialization
	public void Init (GameObject _GameScripts, int _currFloor)
    {
        gameObject.SetActive(true);

        GameScripts = _GameScripts;

        if (transform.GetChild(0).GetComponent<Text>() != null)
            transform.GetChild(0).GetComponent<Text>().text = "Entering Floor " + _currFloor.ToString();
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
