using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with melee attack of player - Add to enemies */
public class CollisionPlayerMelee : MonoBehaviour, CollisionBase
{
    /* On trigger in 2D */
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("MeleeHitbox"))
    //    {
    //        Debug.Log("ATTACKED!!");
    //        Destroy(other.gameObject);
    //        Destroy(transform.parent.gameObject); // destroy the gameobject

    //       // Debug.Log("WHY NO IN :(");
    //    }
    //}

    public void CollisionResponse()
    {

        gameObject.SetActive(false);
    }
}
