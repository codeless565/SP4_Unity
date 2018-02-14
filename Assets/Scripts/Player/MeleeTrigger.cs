using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision Trigger Responses */
public class MeleeTrigger : MonoBehaviour, CollisionBase
{
    private GameObject temp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MeleeHitbox")
        {
            Debug.Log("ATTACKED!!");
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject); // destroy the gameobject
        }
    }

    public void CollisionResponse()
    {
        //temp = GameObject.FindGameObjectWithTag("MeleeHitbox");

        //Debug.Log("ATTACKED!!");
        //Destroy(temp);
        //Destroy(transform.parent.gameObject); // destroy the gameobject
    }
}
