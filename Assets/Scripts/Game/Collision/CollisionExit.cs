using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExit : MonoBehaviour, CollisionBase
{
    public void CollisionResponse()
    {
        GameObject.FindGameObjectWithTag("GameScript").GetComponent<GameMode>().GameClear();
    }
}
