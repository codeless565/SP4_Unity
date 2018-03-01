using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Dictionary<string, StateBase> m_stateMap;
    StateBase m_currState;
    StateBase m_nextState;

	// Use this for initialization
	public StateMachine()
    {
        m_stateMap = new Dictionary<string, StateBase>();
        m_currState = null;
        m_nextState = null;
	}
	
	// Update is called once per frame
	public void Update()
    {
	    if (m_nextState != m_currState)
        {
            m_currState.ExitState();
            m_currState = m_nextState;
            m_currState.EnterState();
        }
        m_currState.UpdateState();
	}

    public void AddState(StateBase _newState)
    {
        if (_newState == null)
        {
            Debug.Log("AddState - State is null");
            return;
        }
        if (m_stateMap.ContainsKey(_newState.StateID))
        {
            Debug.Log("AddState - State already exists");
            return;
        }
        if (m_currState == null)
        {
            Debug.Log("AddState - Currstate is changed to newstate");
            m_currState = m_nextState = _newState;
        }

        m_stateMap.Add(_newState.StateID, _newState);
    }

    public void SetNextState(string _nextStateID)
    {
        if (m_stateMap.ContainsKey(_nextStateID))
        {
            Debug.Log("SetNextState - changing state");
            m_nextState = m_stateMap[_nextStateID];
        }
    }

    public string CurrentState
    {
        get
        {
            if (m_currState != null)
                return m_currState.StateID;
            return "<No states>";
        }
    }
}
