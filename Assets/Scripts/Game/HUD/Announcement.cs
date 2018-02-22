using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcement : MonoBehaviour
{
    public float timeLimit = 3;
    private float elapseTime;

	// Use this for initialization
	void Start ()
    {
        elapseTime = timeLimit;
    }
	
	// Update is called once per frame
	void Update () {
		if (elapseTime > 0)
        {
            elapseTime -= Time.deltaTime;
            if (elapseTime <= 0)
            {
                gameObject.GetComponent<Text>().text = "";
            }
        }
	}

    public void SetNewAnnouncement(string _input)
    {
        elapseTime = timeLimit;
        gameObject.GetComponent<Text>().text = _input;
    }
}
