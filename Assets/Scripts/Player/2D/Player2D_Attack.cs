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

    private float m_timer, testTimer; // for duration of hitbox
    
    void Start()
    {
        m_timer = 0.4F;
        testTimer = 0.0f;
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
            temp = Instantiate(melee, transform.position , transform.rotation);
            temp.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            /* Translate the hitbox so that it can rotate according to player direction */
            temp.transform.Translate(0, 1.25F,0);
            //transform.Rotate(0, 0, 180 * Time.deltaTime);

            //Debug.Log(transform.rotation.ToString());
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
