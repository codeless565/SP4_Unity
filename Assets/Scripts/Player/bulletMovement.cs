using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Movement of "bullet" to detect collision */
public class bulletMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float moveSpeed = 2.0F;

    private float bulletRange = 10.0F;
    private float distance;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        // Getting the distance between bullet and player
        //if ((player.transform.position - transform.position).sqrMagnitude < 10.0F)
        //{
        //    Debug.Log("IS IN RANGE");
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    Debug.Log("IS OUT RANGE");
        //}
       
    }
}
