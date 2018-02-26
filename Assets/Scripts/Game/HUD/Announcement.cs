using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcement : MonoBehaviour
{
    public float timeLimit = 3;
    private float elapseTime;
    private Vector3 movingDir;
    private Vector3 acceleration;

	// Use this for initialization
	void Start ()
    {
        elapseTime = timeLimit;
        movingDir = new Vector3(0, 10, 0);
        acceleration = new Vector3(0, 5, 0);
    }
	
	// Update is called once per frame
	void Update () {
		if (elapseTime > 0)
        {
            elapseTime -= Time.deltaTime;
            movingDir += acceleration * Time.deltaTime;
            GetComponent<Transform>().position += movingDir * Time.deltaTime;
            if (elapseTime <= 0)
            {
                Destroy(gameObject);
            }
        }
	}

    public void SetNewAnnouncement(string _input)
    {
        elapseTime = timeLimit;
        gameObject.GetComponent<Text>().text = _input;
    }
}
