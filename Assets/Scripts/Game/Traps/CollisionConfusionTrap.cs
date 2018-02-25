using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Confuse Player and messes their controls */
/* Specific to player */
public class CollisionConfusionTrap : MonoBehaviour
{
    /* Modifiers to adjust player movement */
    static public int m_confusedModifier;

    /* Timer for Confused Effect */
    private float m_fDuration;
    private bool m_isActivated;

    // Use this for initialization
    void Start()
    {
        m_confusedModifier = 1;

        m_fDuration = 5.0F;
        m_isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isActivated)
        {
            m_fDuration -= Time.deltaTime;

            /* For Rendering the Sprite */
            if (m_fDuration <= 0.0F)
            {
                Destroy(gameObject);
                m_confusedModifier = 1;
                m_fDuration = 5.0f;
            }
        }
    }

    /* When Collide, Confuse the movement */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* If Only want player to be affected */
        if (collision.GetComponent<Player2D_Manager>() == null)
            return;

        /* Only Once */
        if (m_isActivated)
            return;

        /* Set Timer for Duration of Confusion */
        m_confusedModifier = -1;
        m_isActivated = true;
    }
}
