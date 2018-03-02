using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeerTeleport : MonoBehaviour, StateBase
{
    // Deer Teleport
    PetsManager m_PetsManager;
    GameObject m_Player;
    bool m_fHasTeleport = false;

    // StateBase
    string m_StateID;
    GameObject m_go;

    public StateDeerTeleport(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_PetsManager = _go.GetComponent<PetsManager>();
    }

    public void EnterState()
    {
        // Player
        m_PetsManager.GetPlayer();
        // Setting Pet Follow Range
        m_PetsManager.SetFollowRange(5f);
    }

    public void UpdateState()
    {
        // Safety Checks //
        // If Follow Range is still 0, set it to 5.
        if (m_PetsManager.GetFollowRange() == 0f)
            m_PetsManager.SetFollowRange(5f);

        
        // Get Distance Apart between Player and Pet //
        m_PetsManager.SetDistanceApart((m_PetsManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

        if (m_PetsManager.GetPlayer() != null)
        {
            // Change to FOLLOW when Player has Teleported.
            m_go.GetComponent<Transform>().position = m_PetsManager.GetPlayer().GetComponent<Transform>().position;
            m_fHasTeleport = true;

            if(m_fHasTeleport)
            {
                m_fHasTeleport = false;
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
