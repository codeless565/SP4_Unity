using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBossIdle : StateBase
{
    string m_StateID;
    GameObject m_go;

    float m_engageDistance = 5;

    GameObject m_player;

    public StateBossIdle(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
    }

    public void EnterState()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateState()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            // Change state to attack once player is in range
            float distance = (m_player.GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude;
            if (distance <= m_engageDistance)
                m_go.GetComponent<BossStatsManager>().SM.SetNextState("StateAttack");
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
