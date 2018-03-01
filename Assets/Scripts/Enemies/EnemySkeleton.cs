using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
	// Enemy Manager //
	private Player2D_StatsHolder m_PlayerStats;
	private GameObject m_Player;
    private EnemyManager m_EnemyManager;
	private StateMachine m_StateMachine;
	private SpriteManager m_SpriteManager;
    private Vector3 m_EnemyDestination;

    public void Init(int _level, Vector3[] _wayPoints, int _wayPointCurrID)
    {
        // Enemy Manager
        m_EnemyManager = GetComponent<EnemyManager>();
        
        // Player 
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        m_EnemyManager.SetPlayer(m_Player);
        m_EnemyManager.SetPlayerStats(m_PlayerStats);
        
        // Enemy Variables from Enemy Manager
        m_EnemyManager.GetEnemyDestination().Set(0, 0, 0);
        m_EnemyManager.SetEnemyDestination(m_EnemyManager.GetEnemyDestination());
        m_EnemyManager.SetDistanceApart(0f);
        m_EnemyManager.SetBoolCanAttack(false);
        m_EnemyManager.SetBoolCanPatrol(false);

        // StateMachine
        m_StateMachine = new StateMachine();
        m_EnemyManager.SetStateMachine(m_StateMachine);
        m_EnemyManager.GetStateMachine().AddState(new StateSkeletonIdle("StateSkeletonIdle", gameObject));
        m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonIdle");

        // Sprite Manager
        m_SpriteManager = GetComponent<SpriteManager>();
        m_EnemyManager.SetSpriteManager(m_SpriteManager);
		m_EnemyManager.GetSpriteManager ().SetHeadEquip (SpriteManager.S_Wardrobe.HEADP_NULL);
		m_EnemyManager.GetSpriteManager ().SetTopEquip (SpriteManager.S_Wardrobe.TOP_NULL);
		m_EnemyManager.GetSpriteManager ().SetBottomEquip (SpriteManager.S_Wardrobe.BOTTOM_NULL);
		m_EnemyManager.GetSpriteManager ().SetShoesEquip (SpriteManager.S_Wardrobe.SHOES_NULL);
		m_EnemyManager.GetSpriteManager ().SetWeaponEquip (SpriteManager.S_Weapon.DAGGER);

        m_EnemyManager.Level = _level;
        m_EnemyManager.Waypoint = _wayPoints;
        m_EnemyManager.CurrWaypointID = _wayPointCurrID;
        m_EnemyManager.EXPReward = m_EnemyManager.EXPRewardScaling * _level;
        m_EnemyManager.Init();
    }

    void Start()
    {
        // Enemy Manager
        m_EnemyManager = GetComponent<EnemyManager>();

        // Player 
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        m_EnemyManager.SetPlayer(m_Player);
        m_EnemyManager.SetPlayerStats(m_PlayerStats);

        // Enemy Variables from Enemy Manager
        m_EnemyManager.GetEnemyDestination().Set(0, 0, 0);
        m_EnemyManager.SetEnemyDestination(m_EnemyManager.GetEnemyDestination());
        m_EnemyManager.SetDistanceApart(0f);
        m_EnemyManager.SetBoolCanAttack(false);
        m_EnemyManager.SetBoolCanPatrol(false);

        // StateMachine
        m_StateMachine = new StateMachine();
        m_EnemyManager.SetStateMachine(m_StateMachine);
        m_EnemyManager.GetStateMachine().AddState(new StateSkeletonIdle("StateSkeletonIdle", gameObject));
        m_EnemyManager.GetStateMachine().SetNextState("StateSkeletonIdle");

        // Sprite Manager
        m_SpriteManager = GetComponent<SpriteManager>();
        m_EnemyManager.SetSpriteManager(m_SpriteManager);
        m_EnemyManager.GetSpriteManager().SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_NULL);
        m_EnemyManager.GetSpriteManager().SetTopEquip(SpriteManager.S_Wardrobe.TOP_NULL);
        m_EnemyManager.GetSpriteManager().SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_NULL);
        m_EnemyManager.GetSpriteManager().SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_NULL);
        m_EnemyManager.GetSpriteManager().SetWeaponEquip(SpriteManager.S_Weapon.DAGGER);

        m_EnemyManager.Level = 1;
        //m_EnemyManager.Waypoint = _wayPoints;
        //m_EnemyManager.CurrWaypointID = _wayPointCurrID;
        m_EnemyManager.EXPReward = m_EnemyManager.EXPRewardScaling * 1;
        m_EnemyManager.Init();
    }

    void Update()
    {
        // StateMachine
        m_EnemyManager.GetStateMachine().Update();
    }
}
