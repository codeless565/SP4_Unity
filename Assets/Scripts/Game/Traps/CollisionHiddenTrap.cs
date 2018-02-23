using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Pops out a random trap when collide */
public class CollisionHiddenTrap : MonoBehaviour, CollisionBase
{
    /* Will Implement the random traps soon */
    /* For now, just render Bear trap when step over */

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /* Respomses to Collision */
    public void CollisionResponse(string _tag)
    {
        throw new System.NotImplementedException();
    }

   
}
