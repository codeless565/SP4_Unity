using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attack System of Player in 2D */
public class Player2D_Attack : MonoBehaviour
{
    /* For Attacking */
    [SerializeField]
    private GameObject melee; // game object to spawn at its location
    private GameObject temp; // store the created game object

    private float m_timer; // for duration of hitbox
    
    void Start()
    {
        m_timer = 0.4F;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack2D();
    }

    /* Spawn HitBox to detect Collision */
    public void PlayerAttack2D()
    {
        // Only when no created hitbox
        if (Input.GetMouseButtonDown(0) && !temp)
        {
            //create a hitbox
            temp = Instantiate(melee, transform.position , transform.parent.transform.rotation);
            temp.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            //Debug.Log("CREATED MELEE HITBOX " + temp.transform.position.ToString());
        }

        if (temp)
        {
            // Start timers
            m_timer -= Time.deltaTime;

            /* After timer is up, upspawn detection box */
            if (m_timer <= 0.0F)
            {
                DestroyImmediate(temp);
                m_timer = 0.4F;
            }
        }
    }
}
