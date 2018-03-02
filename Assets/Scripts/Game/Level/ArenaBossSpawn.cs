using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBossSpawn : MonoBehaviour
{ 
    //Player
    public GameObject Player;
    private Vector2 m_playerPos;

    //Boss
    public GameObject BossEntity;
    private Vector2 m_BossPos;

    //Main Camera/Player Camera
    public GameObject MainCamera;
    public GameObject MiniMap;

    //GO
    private GameObject go_floorholder;
    private GameObject go_exit;
    private Vector2 m_exitPos;

    private int m_currentFloor;
    private int m_arenaSizeRow;
    private int m_arenaSizeColum;

    //Boss HUD
    public GameObject BossHPBarPrefab;
    public GameObject HUD;

    public void Init(int _currentFloor)
    {
        m_currentFloor = _currentFloor;

        go_floorholder = gameObject.GetComponent<ArenaGenerator>().boardHolder;
        m_arenaSizeRow = gameObject.GetComponent<ArenaGenerator>().rows;
        m_arenaSizeColum = gameObject.GetComponent<ArenaGenerator>().columns;

        GameObject go_chest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;
        GameObject go_royalchest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().RoyalChest;

        //Player and Exit
        PlayerSpawn();
        BossSpawn();
    }

    private void PlayerSpawn()
    {
        m_playerPos = new Vector2(m_arenaSizeRow * 0.5f, 1);
        GameObject t_player = Instantiate(Player, m_playerPos, Quaternion.identity, go_floorholder.transform); //Create Player Object

        MainCamera.GetComponent<CameraController>().SetPlayer(t_player); //Spawn Player and Set the Instantiated player into Camera
        MiniMap.GetComponent<ExplorationMap>().Init();

        if (GetComponent<CameraEffects>() != null)
            GetComponent<CameraEffects>().Init(MainCamera.GetComponent<Camera>());
    }

    private void BossSpawn()
    {
        m_BossPos = new Vector2(m_arenaSizeRow * 0.5f, m_arenaSizeColum * 0.7f);
        GameObject t_boss = Instantiate(BossEntity, m_BossPos, Quaternion.identity, go_floorholder.transform); //Create Player Object

        t_boss.GetComponent<BossStatsManager>().Init(m_currentFloor);

        // Non Implemented as of now TODO

        // Create Boss HP Bar on the player's HUD
        if (BossHPBarPrefab != null)
        {
            BossHPBarPrefab = Instantiate(BossHPBarPrefab, HUD.transform) as GameObject;
            BossHPBarPrefab.GetComponent<PlayerHealthBar>().Init(t_boss);
        }
    }

}
