using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collision with melee attack of player - Add to enemies */
public class CollisionPlayerMelee : MonoBehaviour, CollisionBase
{
    /* Check if Attacked, used in enemy scripts to deduct health */
    private bool m_bisAttacked = false;

    public bool Attacked
    {
        get
        {
            return m_bisAttacked;
        }
        set
        {
            m_bisAttacked = value;
        }
    }

    public void CollisionResponse(string _tag)
    {
        // Check only with Attack
        if (_tag != "MeleeHitbox")
            return;

        // Set attacked to be true
        m_bisAttacked = true;

        //Debug.Log(m_bisAttacked);
    }
}
