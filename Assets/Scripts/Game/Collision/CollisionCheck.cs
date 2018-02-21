using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CollisionBase>() == null) //Check if the script with "CollisionBase" as interface exists
            return;
        
        other.GetComponent<CollisionBase>().CollisionResponse(tag);
    }
}
