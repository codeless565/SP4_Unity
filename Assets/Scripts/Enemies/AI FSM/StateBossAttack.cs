using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBossAttack : StateBase
{
    string m_StateID;
    GameObject m_go;

    float m_attackDelay;
    float m_attackTimer;
    GameObject m_Fireball;
    float m_spawnDisplacementDist;

    GameObject m_player;

    public StateBossAttack(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_Fireball = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().Fireball;
        m_spawnDisplacementDist = m_go.transform.localScale.x * 0.5f;
    }

    public void EnterState()
    {
        m_attackDelay = 1;
        m_attackTimer = m_attackDelay;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateState()
    {
        // Change state to Enraged if hp goes down to 50%
        if (m_go.GetComponent<BossStatsManager>().Health <= m_go.GetComponent<BossStatsManager>().MaxHealth * 0.5)
            m_go.GetComponent<BossStatsManager>().SM.SetNextState("StateRage");

        m_attackTimer -= Time.deltaTime;

        if (m_player == null) // if there is no player, find player
            m_player = GameObject.FindGameObjectWithTag("Player");
        else
        {
            // Shoots Projectile at player
            if (m_attackTimer <= 0)
            {
                //Instatiate a fire ball toward the player
                Vector3 targetPos = m_player.transform.position;
                Vector3 targetdir = Vector3.Normalize(targetPos - m_go.transform.position);
                Vector3 spawnPoint = targetdir * m_spawnDisplacementDist + m_go.transform.position;

                GameObject t_FireBall = Object.Instantiate(m_Fireball, spawnPoint, Quaternion.identity) as GameObject;
                t_FireBall.GetComponent<ProjectileObject>().Init(m_go, targetdir, 1, 2);

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
