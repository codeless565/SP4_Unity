using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Confuse Player and messes their controls */
/* Specific to player */
public class CollisionConfusionTrap : MonoBehaviour
{
    /* Timer for Confuseion Effect on Player */
    public float EffectDuration = 5;

    /* Timer for Confused Trap */
    private float m_fDuration;
    private bool m_isActivated;

    // Use this for initialization
    void Start()
    {
        m_fDuration = 3.0F;
        m_isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* For Rendering the Sprite */
        if (m_isActivated)
        {
            m_fDuration -= Time.deltaTime;
            if (m_fDuration <= 0.0F)
                Destroy(gameObject);
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
        m_isActivated = true;

        /* Doing Effect in another Script, Check if have Script alr */
        if (collision.GetComponent<ConfusedEffect>() == null)
        {
            collision.gameObject.AddComponent<ConfusedEffect>(); // added to other
            collision.GetComponent<ConfusedEffect>().SetDuration(EffectDuration);
        }
        else /* If alr have script, entend the duration */
        {
            collision.GetComponent<ConfusedEffect>().Resets();
        }

        GameObject Profile = GameObject.FindGameObjectWithTag("PlayerProfileHUD");
        if (Profile != null)
        {
            GameObject Aliment = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ProfileAlimentConfusion;
            Aliment = Instantiate(Aliment, Profile.transform);
            Aliment.GetComponent<PlayerProfileStatusAliment>().Init(EffectDuration);
        }
    }
}
