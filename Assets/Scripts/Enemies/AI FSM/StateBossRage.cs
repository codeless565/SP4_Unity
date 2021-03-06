﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBossRage : StateBase
{
    string m_StateID;
    GameObject m_go;

    float m_attackDelay;
    float m_attackTimer;
    GameObject m_Fireball;
    GameObject m_Deathball;
    float m_spawnDisplacementDist;

    GameObject m_player;

    public StateBossRage(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_Fireball = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().Fireball;
        m_Deathball = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().DeathBall;
        m_spawnDisplacementDist = m_go.transform.localScale.x * 0.5f;
    }

    public void EnterState()
    {
        m_attackDelay = 0.3f;
        m_attackTimer = m_attackDelay;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateState()
    {
        m_attackTimer -= Time.deltaTime;

        if (m_player == null)
            m_player = GameObject.FindGameObjectWithTag("Player");
        else
        {
            // Fires Projectile at enemy at a faster rate
            if (m_attackTimer <= 0)
            {
                //Instatiate a fire ball toward the player
                Vector3 targetPos = m_player.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
                Vector3 targetdir = Vector3.Normalize(targetPos - m_go.transform.position);
                Vector3 spawnPoint = targetdir * m_spawnDisplacementDist + m_go.transform.position;

                // Randomly choose from 2 type of attack and spawn
                GameObject t_FireBall;
                int chance = Random.Range(0, 2);
                if (chance == 0)
                    t_FireBall = Object.Instantiate(m_Deathball, spawnPoint, Quaternion.identity) as GameObject; // Non Deflectable
                else
                    t_FireBall = Object.Instantiate(m_Fireball, spawnPoint, Quaternion.identity) as GameObject; // Deflectable

                t_FireBall.GetComponent<Projectile>().Init(m_go, targetdir, 1, 2);

                m_attackTimer = m_attackDelay;
            }
        }
    }

    public void ExitState()
    {

    }

    public string StateID
    {
        get { return m_StateID; }
    }
}
