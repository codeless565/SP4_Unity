using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkeletonIdle : StateBase
{
    EnemyManager m_EnemyManager;

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
        m_EnemyManager.GetPlayer();
    }

    public void UpdateState()
    {
        // Animation
        m_EnemyManager.GetSpriteManager().SetMoving(false);
        // Distance Apart
        m_EnemyManager.SetDistanceApart((m_EnemyManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

    }

    public void ExitState()
    {

    }

    public string StateID
    {
        get { return m_StateID; }
    }

}
