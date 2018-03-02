using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeerGuard : MonoBehaviour, StateBase
{
    // Deer Guard
    PetsManager m_PetsManager;
    GameObject m_Player;

    // StateBase
    string m_StateID;
    GameObject m_go;

    public StateDeerGuard(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_PetsManager = _go.GetComponent<PetsManager>();
    }

    public void EnterState()
    {
        // Player
        m_PetsManager.GetPlayer();
        // Setting Pet Guard Range
        m_PetsManager.SetGuardRange(2f);
        // Setting Pet Follow Range
        m_PetsManager.SetFollowRange(5f);
    }

    public void UpdateState()
    {
        // Safety Checks //
        // If Guard Range is still 0, set it to 2.
        if (m_PetsManager.GetGuardRange() == 0f)
            m_PetsManager.SetGuardRange(2f);

        // If Follow Range is still 0, set it to 5.
        if (m_PetsManager.GetFollowRange() == 0f)
            m_PetsManager.SetFollowRange(5f);

        // Get Distance Apart between Player and Pet //
        m_PetsManager.SetDistanceApart((m_PetsManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);
        
        if(m_PetsManager.GetPlayer() != null)
        {
            // Change to FOLLOW when Player is in range.
            if(m_PetsManager.GetDistanceApart() >= m_PetsManager.GetGuardRange())
            {
                m_PetsManager.GetStateMachine().SetNextState("StateDeerFollow");
            }
        }
        else
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_PetsManager.SetPlayer(m_Player);
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
