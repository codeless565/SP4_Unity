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

    private float m_timer; // for duration of sprite

    /* Direction the Attack will be facing */
    private float m_AngleToRotate;
    public static Vector2 Direction;

    /* For Interaction */
    private bool m_bisInteracting;
    public bool Interact
    {
        get
        {
            return m_bisInteracting;
        }

        set
        {
            m_bisInteracting = value;
        }
    }

    void Start()
    {
        m_timer = 0.4F;
        m_AngleToRotate = 0.0f;

        /* Set Start Downwards */
        Direction = new Vector2(0.0f, -1.0f);

        /* Default not interacting */
        m_bisInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack2D();
    }

    /* Spawn HitBox to detect Collision */
    public void PlayerAttack2D()
    {
        if (!m_bisInteracting)
        {
            // Only when no created hitbox
            if (Input.GetMouseButtonDown(0) && !temp)
            {
                //create a hitbox
                temp = Instantiate(melee, transform.position, transform.rotation);
                temp.transform.parent = GameObject.FindGameObjectWithTag("Player").transform; // parenting 

                /* Transformation to rotate the Hitbox */
                m_AngleToRotate = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
                temp.transform.Rotate(0, 0, -m_AngleToRotate);
                temp.transform.Translate(new Vector3(0.0f, 0.8f, 0.0f));

                /* Set Animation in Parent to Start */
                Player2D_Manager.attackClicked = true;
            }
        }

        /* When a hitbox is created */
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

    /* Collsion Box of Attack hits another Collidable */
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(temp);
    //}
}
