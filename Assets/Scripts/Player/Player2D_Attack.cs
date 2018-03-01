using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attack System of Player in 2D */
public class Player2D_Attack : MonoBehaviour
{
    /* Owner */
    GameObject m_player;

    /* For Attacking */
    [SerializeField]
    private GameObject[] meleecombo; // game object to spawn at its location
    static public GameObject temp; // store the created game object

    private float m_timer; // for duration of sprite
    public float m_comboResetTime = 3;
    private float m_comboElaspeTimer;
    private int m_comboCount;

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
        m_player = transform.parent.gameObject;

        m_timer = 0.1f;
        m_AngleToRotate = 0.0f;

        /* Set Start Downwards */
        Direction = new Vector2(0.0f, -1.0f);

        /* Default not interacting */
        m_bisInteracting = false;

        m_comboCount = 0;
        m_comboElaspeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack2D();
    }

    /* Spawn HitBox to detect Collision */
    public void PlayerAttack2D()
    {
        if (!m_bisInteracting) // if is not interacting with merchant
        {
            // Only when no created hitbox has been created
            if (GetTrigger() && !temp)
            {
                //create a hit
                temp = Instantiate(meleecombo[m_comboCount], transform.position, transform.rotation, m_player.transform);

                /* Transformation to rotate the Hitbox */
                m_AngleToRotate = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
                temp.transform.Rotate(0, 0, -m_AngleToRotate);
                temp.transform.Translate(new Vector3(0.0f, 0.5f, 0.0f));

                //Set Damage to Attack
                temp.GetComponent<CollisionPlayerAttack>().Init(m_player.GetComponent<StatsBase>().Attack);

                /* Set Animation in Parent to Start */
                Player2D_Manager.attackClicked = true;

                /* Set Trigger to False */
#if UNITY_ANDROID || UNITY_IPHONE
                Player2D_TriggerAttack._triggered = false;
#endif

                m_comboElaspeTimer = m_comboResetTime;
                ++m_comboCount;
                m_comboCount %= meleecombo.Length;
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
                    m_timer = 0.1F;
                }
            }

            m_comboElaspeTimer -= Time.deltaTime;
            if (m_comboElaspeTimer <= 0)
            {
                m_comboCount = 0;
            }
        }
    }

    private bool GetTrigger()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
        //return Player2D_TriggerAttack._triggered;
#elif UNITY_ANDROID || UNITY_IPHONE
        return Player2D_TriggerAttack._triggered;
#else
        return false;
#endif
    }

}
