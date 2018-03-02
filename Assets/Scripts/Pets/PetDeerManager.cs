using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDeerManager : MonoBehaviour
{
    // PetsManager
    private Player2D_StatsHolder m_PlayerStats;
    private GameObject m_Player;
    private PetsManager m_PetsManager;
    private StateMachine m_StateMachine;

    void Start()
    {
        // Pet Manager
        m_PetsManager = GetComponent<PetsManager>();

        // Player
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        m_PetsManager.SetPlayer(m_Player);
        m_PetsManager.SetPlayerStats(m_PlayerStats);

        // Pet Variables from Pets Manager
        m_PetsManager.GetPetDestination().Set(0, 0, 0);
        m_PetsManager.SetPetDestination(m_PetsManager.GetPetDestination());
        m_PetsManager.SetDistanceApart(0f);
        m_PetsManager.SetGuardRange(0f);
        m_PetsManager.SetFollowRange(0f);

        // StateMachine
        m_StateMachine = new StateMachine();
        m_PetsManager.SetStateMachine(m_StateMachine);
        m_PetsManager.GetStateMachine().AddState(new StateDeerGuard("StateDeerGuard", gameObject));
        m_PetsManager.GetStateMachine().AddState(new StateDeerFollow("StateDeerFollow", gameObject));
        m_PetsManager.GetStateMachine().AddState(new StateDeerTeleport("StateDeerTeleport", gameObject));
        m_PetsManager.GetStateMachine().SetNextState("StateDeerGuard");

        m_PetsManager.EXPReward = m_PetsManager.EXPRewardScaling * m_PetsManager.Level;
        m_PetsManager.Init();
    }

    void Update()
    {
        // StateMachine
        m_PetsManager.GetStateMachine().Update();

        Debug.Log("State: " + m_PetsManager.GetStateMachine().CurrentState);
    }
}
