using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkeletonPatrol : StateBase
{
    // Skeleton Idle
    EnemyManager m_EnemyManager;
    GameObject m_Player;
    float m_fDistanceToWaypoint;
    float m_fMaxSpeed = 5f;

    // StateBase
    string m_StateID;
    GameObject m_go;

    public StateSkeletonPatrol(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_EnemyManager = _go.GetComponent<EnemyManager>();
    }

    public void EnterState()
    {
        // Animation
        m_EnemyManager.GetSpriteManager().SetMoving(true);
        // Player
        m_EnemyManager.GetPlayer();
        // Setting Enemy Chase Range
        m_EnemyManager.SetChaseRange(4f);
    }

    public void UpdateState()
    {
        // Safety Checks //
        // If Moving Animation is false, switch it to true.
        if (m_EnemyManager.GetSpriteManager().GetMoving())
            m_EnemyManager.GetSpriteManager().SetMoving(true);

        // If Chase Range is still 0, set it to 4.
        if (m_EnemyManager.GetChaseRange() == 0f)
            m_EnemyManager.SetChaseRange(4f);

        // Get Distance Apart between Player and Enemy //
        m_EnemyManager.SetDistanceApart((m_EnemyManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

        // States //
        if (m_EnemyManager.GetPlayer() != null)
        {
            // Change State to CHASE when Player is in range.
            if (m_EnemyManager.GetDistanceApart() <= m_EnemyManager.GetChaseRange() || m_EnemyManager.m_Waypoint.Length == null)
            {
                m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonChase");
            }

            // Patrolling
            m_fDistanceToWaypoint = (m_go.GetComponent<Transform>().position - m_EnemyManager.m_Waypoint[m_EnemyManager.m_currWaypointID]).magnitude;
            if (m_fDistanceToWaypoint <= m_EnemyManager.MoveSpeed * Time.deltaTime / m_fMaxSpeed) //if it is possible to reach the waypoint by this frame
            {
                ++m_EnemyManager.m_currWaypointID;
                m_EnemyManager.m_currWaypointID %= m_EnemyManager.m_Waypoint.Length;
            }
            else
            {
                Vector3 dir = (m_EnemyManager.m_Waypoint[m_EnemyManager.m_currWaypointID] - m_EnemyManager.GetComponent<Transform>().position).normalized;
                m_EnemyManager.GetComponent<Transform>().position += dir * m_EnemyManager.MoveSpeed * Time.deltaTime / m_fMaxSpeed;
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
