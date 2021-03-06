﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrapPoison : MonoBehaviour
{
    private int m_currentLevel;

    public  float DestroyDelayTime = 3;
    public float EffectDuration = 5;
    private float m_elapseTimer;
    private bool b_isDestroying;

    void Start()
    {
        b_isDestroying = false;
        m_elapseTimer = DestroyDelayTime;
    }

    void Update()
    {
        if (b_isDestroying)
        {
            m_elapseTimer -= Time.deltaTime;
            if (m_elapseTimer <= 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (b_isDestroying)
            return;

        if (other.GetComponent<Player2D_StatsHolder>() == null) 
            return;

        if (other.GetComponent<PoisonTrapEffect>() == null)
        {
            other.gameObject.AddComponent<PoisonTrapEffect>();
            other.GetComponent<PoisonTrapEffect>().SetDuration(EffectDuration);
            other.GetComponent<PoisonTrapEffect>().SetDamage(m_currentLevel * 0.5f);
        }
        else
            other.GetComponent<PoisonTrapEffect>().ResetTimer();

        /* Create Status Aliment Indicator over Profile */
        GameObject Profile = GameObject.FindGameObjectWithTag("PlayerProfileHUD");
        if (Profile != null)
        {
            GameObject Aliment = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ProfileAlimentPoison;
            Aliment = Instantiate(Aliment, Profile.transform);
            Aliment.GetComponent<PlayerProfileStatusAliment>().Init(EffectDuration);
        }

        b_isDestroying = true;
    }

    public int CurrentFloor
    {
        set { m_currentLevel = value; }
    }
}
