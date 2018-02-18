using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CollisionBase>() == null) //Check if the script with "CollisionBase" as interface exists
            return;
        
        other.GetComponent<CollisionBase>().CollisionResponse(tag);
    }
}
