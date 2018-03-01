using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
	// Enemy Manager //
    private EnemyManager m_EnemyManager;
    private Player2D_StatsHolder m_PlayerStats;
    private GameObject m_Player;
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

        // Sprite Manager
        m_EnemyManager.SetSpriteManager(m_EnemyManager.GetSpriteManager().GetComponent<SpriteManager>());
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
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        m_EnemyManager.SetPlayer(m_Player);
        m_EnemyManager.SetPlayer(m_Player);
        m_EnemyManager.SetPlayerStats(m_PlayerStats);

        // Enemy Variables from Enemy Manager
        m_EnemyManager.GetEnemyDestination().Set(0f, 0f, 0f);
        m_EnemyManager.SetEnemyDestination(m_EnemyManager.GetEnemyDestination());
        m_EnemyManager.SetDistanceApart(0f);
        m_EnemyManager.SetBoolCanAttack(false);
        m_EnemyManager.SetBoolCanPatrol(false);

        // Sprite Manager
        m_EnemyManager.SetSpriteManager(m_EnemyManager.GetSpriteManager().GetComponent<SpriteManager>());
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
        Debug.Log(m_EnemyManager.GetBoolCanAttack());
        // States
    }
}
