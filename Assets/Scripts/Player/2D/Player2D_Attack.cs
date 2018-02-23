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

    //private float ThisShallBeMyRotation()
    //{
    //    transform.Rotate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0));

    //    return 0.0f;
    //}

    ///* Shifting the Attack Melee Box according to direction */
    //private float CalculateRotationAngle(int _verticalOffset, int _horizontalOffset)
    //{
    //    /* Store the values of the offsets */
    //    int storeVertical = _verticalOffset;
    //    int storeHorizontal = _horizontalOffset;

    //    /* Work with Positive values */
    //    if (storeVertical < 0)
    //        storeVertical = 2;
    //    if (storeHorizontal < 0)
    //        storeHorizontal = 2;

    //    /* Store the angles to be rotated by in arrays */
    //    float[] horizontalAngles = { 90, 270 };
    //    float[] verticalAngles = { 0, 180, 360 };

    //    /* To get the angle to Rotate by */
    //    switch (storeHorizontal)
    //    {
    //        case 0:
    //            {
    //                if (storeVertical != 0)
    //                {
    //                    if (storeVertical == 1)
    //                        return verticalAngles[storeVertical - 1];
    //                    else
    //                        return (verticalAngles[storeVertical - 1] == m_AngleToRotate ? (0): (verticalAngles[storeVertical - 1])) ;
    //                }

    //                return m_AngleToRotate; // when 0, just return the horizontal angle
    //            }

    //        case 1: // when going in the positive direction
    //            {
    //                Debug.Log("the vertical: " + verticalAngles[storeVertical] + " the horizontal : " + horizontalAngles[storeHorizontal]);
                    
    //                /* Check if the vertical is not 0 */
    //                if (storeVertical != 0)
    //                    return (verticalAngles[storeVertical - 1] + horizontalAngles[storeHorizontal - 1]) * 0.5f - m_AngleToRotate;
                    
    //                return horizontalAngles[storeHorizontal - 1] - m_AngleToRotate; // when 0, just return the horizontal angle
    //            }
    //        case 2: // when going in the negative direction
    //            {
    //                if (storeVertical == 1)
    //                {
    //                    Debug.Log("the vertical: " + verticalAngles[storeVertical] + " the horizontal : " + horizontalAngles[storeHorizontal]);
    //                    return (verticalAngles[storeVertical] + horizontalAngles[storeHorizontal - 1]) * 0.5f - m_AngleToRotate;
    //                }
    //                else
    //                {
    //                    if (storeVertical != 0)
    //                    {
    //                        Debug.Log("the vertical: " + verticalAngles[storeVertical] + " the horizontal : " + horizontalAngles[storeHorizontal]);
    //                        return (verticalAngles[storeVertical - 1] + horizontalAngles[storeHorizontal - 1]) * 0.5f - m_AngleToRotate;
    //                    }
    //                    return horizontalAngles[storeHorizontal - 1] - m_AngleToRotate; // if 0, return horizontal angle
    //                }
    //            }
    //        default: // when player is not moving, take the last move direction
    //            {
    //                return m_AngleToRotate;
    //            }
    //    }
    //}
}
