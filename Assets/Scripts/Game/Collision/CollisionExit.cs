using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExit : MonoBehaviour, CollisionBase
{
    public void CollisionResponse(string tag)
    {
        // Remove all Status Aliment before exiting as it may bring over to next level
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<SlowDownTrapEffect>() != null)
        {
            player.GetComponent<SlowDownTrapEffect>().RemoveEffect();
        }



        GameObject.FindGameObjectWithTag("GameScript").GetComponent<GameMode>().GameClear();
    }
}
