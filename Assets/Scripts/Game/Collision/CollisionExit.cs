using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExit : MonoBehaviour, CollisionBase
{
    public void CollisionResponse(string tag)
    {
        GameObject.FindGameObjectWithTag("GameScript").GetComponent<GameMode>().GameClear();
    }
}
