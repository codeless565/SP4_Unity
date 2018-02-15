using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2DManager : MonoBehaviour
{
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement2D();

		
	}


    /* Movement of Player */
    void Movement2D()
    {
        // Up / Down
        if (Input.GetKey(KeyCode.W))
        {
            //storePos.y +=  Time.deltaTime;
            //transform.position += storePos;
            transform.position += transform.up * 10.0F * Time.deltaTime;

            Debug.Log(transform.position.ToString());
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * 10.0F * Time.deltaTime;
        }

        // Left / Right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
            //transform.position -= transform.right * 10.0F * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
            //transform.position += transform.right * 10.0F * Time.deltaTime;
           

        }


    }
}
