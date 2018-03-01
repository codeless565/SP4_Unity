using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkeletonChase : StateBase
{
	// Skeleton Chase
	EnemyManager m_EnemyManager;
	GameObject m_Player;

	// StateBase
	string m_StateID;
	GameObject m_go;

	public StateSkeletonChase(string _stateID, GameObject _go)
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
        // Setting Enemy Attack Range
        m_EnemyManager.SetAttackRange(1f);
	}

	public void UpdateState()
	{
        // Safety Checks //
        // If Moving Animation is False, switch it to True.
        if (!m_EnemyManager.GetSpriteManager().GetMoving())
            m_EnemyManager.GetSpriteManager().SetMoving(true);

        // If Chase Range is still 0, set it to 4.
        if (m_EnemyManager.GetChaseRange() == 0f)
            m_EnemyManager.SetChaseRange(4f);

        // If Attack Range is still 0, set it to 1.
        if (m_EnemyManager.GetAttackRange() == 0f)
            m_EnemyManager.SetAttackRange(1f);

        // Get Distance Apart between Player and Enemy //
        m_EnemyManager.SetDistanceApart((m_EnemyManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

		// States //
		if(m_EnemyManager.GetPlayer() != null)
		{
            // Enemy will walk to Player
            m_go.GetComponent<Transform>().position = Vector2.MoveTowards(m_go.GetComponent<Transform>().position, m_EnemyManager.GetPlayer().GetComponent<Transform>().position, Time.deltaTime);

            // Change to ATTACK when Player is in range.
            if(m_EnemyManager.GetDistanceApart() <= m_EnemyManager.GetAttackRange())
            {
                m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonAttack");
            }
            // Otherwise, Change to IDLE when Player is out of range.
            else if (m_EnemyManager.GetDistanceApart() > m_EnemyManager.GetChaseRange())
			{
				m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonIdle");
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
