using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkeletonIdle : StateBase
{
	// Skeleton Idle
    EnemyManager m_EnemyManager;
    GameObject m_Player;
    bool m_fCanChangeState = false;

    // StateBase
    string m_StateID;
    GameObject m_go;

    public StateSkeletonIdle(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_EnemyManager = _go.GetComponent<EnemyManager>();
    }

    public void EnterState()
    {
        // Animation
        m_EnemyManager.GetSpriteManager().SetMoving(false);
        // Player
        m_EnemyManager.GetPlayer();
        // Setting Enemy Chase Range
        m_EnemyManager.SetChaseRange(4f);
        // Setting Enemy Change State Timer
        m_EnemyManager.SetChangeStateTimer(4f);
    }

    public void UpdateState()
    {
        // Safety Checks //
        // If Moving Animation is True, switch it to false.
        if (m_EnemyManager.GetSpriteManager().GetMoving())
            m_EnemyManager.GetSpriteManager().SetMoving(false);

        // If Chase Range is still 0, set it to 4.
        if (m_EnemyManager.GetChaseRange() == 0f)
            m_EnemyManager.SetChaseRange(4f);

        // If Timer is still 0, set it to 4.
		if (m_EnemyManager.GetChangeStateTimer() == 0f && !m_fCanChangeState)
            m_EnemyManager.SetChangeStateTimer(4f);

        // Get Distance Apart between Player and Enemy //
        m_EnemyManager.SetDistanceApart((m_EnemyManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

        // States //
        if(m_EnemyManager.GetPlayer() != null)
        {
            // Change to CHASE when Player is in range.
            if (m_EnemyManager.GetDistanceApart() <= m_EnemyManager.GetChaseRange())
            {
                m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonChase");
            }
            // Otherwise Change to PATROL after 4 seconds is up.
            else
            {
                m_fCanChangeState = true;
                m_EnemyManager.SetChangeStateTimer(m_EnemyManager.GetChangeStateTimer() - Time.deltaTime);

                if (m_EnemyManager.GetChangeStateTimer() <= 0f)
                {
                    m_fCanChangeState = false;
                    m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonPatrol");
                }
            }
        }
        else
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_EnemyManager.SetPlayer(m_Player);
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
