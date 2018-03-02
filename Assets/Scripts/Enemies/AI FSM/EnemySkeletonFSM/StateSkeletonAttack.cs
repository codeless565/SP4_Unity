using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkeletonAttack : StateBase
{
    // Skeleton Attack
    EnemyManager m_EnemyManager;
    GameObject m_Player;

    // StateBase
    string m_StateID;
    GameObject m_go;

    // Dodge System 
    private int randomValue;
    private bool m_bbol;

    public StateSkeletonAttack(string _stateID, GameObject _go)
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
        // Setting Enemy Attack Range
        m_EnemyManager.SetAttackRange(1f);
        // Setting Enemy Attack Timer
        m_EnemyManager.SetAttackTimer(1f);

        randomValue = 0;
        m_bbol = false;
    }

    public void UpdateState()
    {
        // Safety Checks //
        // If Moving Animation is True, switch it to false.
        if (m_EnemyManager.GetSpriteManager().GetMoving())
            m_EnemyManager.GetSpriteManager().SetMoving(false);

        // Player
        m_EnemyManager.GetPlayer();

        // If Attack Range is still 0, set it to 1.
        if (m_EnemyManager.GetAttackRange() == 0f)
            m_EnemyManager.SetAttackRange(1f);

        // If Attack Timer is still 0, set it to 1.
        if (m_EnemyManager.GetAttackTimer() == 0f)
            m_EnemyManager.SetAttackTimer(1f);

        // Get Distance Apart between Player and Enemy //
        m_EnemyManager.SetDistanceApart((m_EnemyManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

        // States //
        if (m_EnemyManager.GetPlayer() != null)
        {
            // Attack Count Down
            m_EnemyManager.SetAttackTimer(m_EnemyManager.GetAttackTimer() - Time.deltaTime);
            
            if(m_EnemyManager.GetAttackTimer() <= 0f)
            {
                m_EnemyManager.GetSpriteManager().SetAttack(true);
                
                /* Simple Dodge System */
                randomValue = Random.Range(1,100);
                switch(randomValue)
                {
                    case 1:
                        m_bbol = false;
                        break;

                    default:
                        m_bbol = true;
                        break;
                }

                if (m_bbol)
                {
                    Debug.Log("IN ATTACK");
                    m_EnemyManager.GetPlayerStats().Health -= Calculator.Instance.CalculateDamage(m_EnemyManager.GetPlayerStats().Attack, m_EnemyManager.GetPlayerStats().GetComponent<Player2D_StatsHolder>().Defense);
                }
                else if (!m_bbol)
                {
                    Debug.Log("IN DODGE");
                    // TODO SPawn Dodge to tell that Player DOdged
                }

                m_EnemyManager.SetAttackTimer(1f);
            }

            // Change to CHASE when Player is out of range.
            if(m_EnemyManager.GetDistanceApart() > m_EnemyManager.GetAttackRange())
            {
                m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonChase");
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
