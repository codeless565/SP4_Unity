using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Slows Down Player When Collide with it */
public class CollisionSlowDownTrap : MonoBehaviour, CollisionBase
{
    private Player2D_StatsHolder m_hold;
    private bool startBlinking;
    
    // Use this for initialization
    void Start ()
    {
        m_hold = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        startBlinking = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /* Responses to Collision */
    //static private bool m_isCheckedBefore = false;
    public void CollisionResponse(string _tag)
    {
        /* When collide with Player */
        /* Do not let Attack affect the traps - Still editing doesnt work */
        //if (_tag != "Player")
        //{
        //m_isCheckedBefore = true;
        //    return;
        //}
        /* Check for Player inside due to player having script too*/
        //if (m_isCheckedBefore)
        //return;

        m_hold.MoveSpeed /= 2; // half the speed
        startBlinking = true;


    }
}
