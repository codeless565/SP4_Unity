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

    public void Init(int _currentFloor)
    {
        m_currentFloor = _currentFloor;

        go_floorholder = gameObject.GetComponent<ArenaGenerator>().boardHolder;
        m_arenaSizeRow = gameObject.GetComponent<ArenaGenerator>().rows;
        m_arenaSizeColum = gameObject.GetComponent<ArenaGenerator>().columns;

        GameObject go_chest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;
        GameObject go_royalchest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().RoyalChest;

        //Player and Exit
        PlayerSpawn(m_arenaSizeRow, m_arenaSizeColum);
    }

    private void PlayerSpawn(int _arenaSizeR, int _arenaSizeC)
    {
        m_playerPos = new Vector2(_arenaSizeR * 0.5f, _arenaSizeC * 0.5f);
        GameObject t_player = Instantiate(Player, m_playerPos, Quaternion.identity, go_floorholder.transform); //Create Player Object

        MainCamera.GetComponent<CameraController>().SetPlayer(t_player); //Spawn Player and Set the Instantiated player into Camera
        MiniMap.GetComponent<ExplorationMap>().Init();
    }

    private void BossSpawn(int _arenaSizeR, int _arenaSizeC)
    {

    }

}
