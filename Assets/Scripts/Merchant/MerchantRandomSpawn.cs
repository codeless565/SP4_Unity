using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Just a script to test the button clicking and all */
public class MerchantRandomSpawn : MonoBehaviour
{
    public GameObject test;
    private bool isSpawned;

	// Use this for initialization
	void Start ()
    {
        isSpawned = false;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isSpawned)
            return;

        Vector3 ranPos = new Vector3(Random.Range(10, 20), Random.Range(10, 20), 0);
        Instantiate(test, ranPos, transform.rotation);
        isSpawned = true;
    }
}
