using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawn : MonoBehaviour {
    //Player
    public GameObject Player;
    private Vector2 m_playerPos;

    //Merchant
    public GameObject Merchant;
    private Vector2 m_MerchantPos;

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

	//spawn one
	bool eIsSpawned = false;
    bool cIsSpawned = false;
    bool mIsSpawned = false;
    bool tIsSpawned = false;
    bool exitIsSpawned = false;

    public void Init()
    {
        m_currentFloor = 1;

        go_floorholder = gameObject.GetComponent<ArenaGenerator>().boardHolder;
        m_arenaSizeRow = gameObject.GetComponent<ArenaGenerator>().rows;
        m_arenaSizeColum = gameObject.GetComponent<ArenaGenerator>().columns;
        
        
        //Player and Exit
        PlayerSpawn(m_arenaSizeRow, m_arenaSizeColum);
       // ExitSpawn();
     //   MerchantSpawn();
    }

    private void PlayerSpawn(int _arenaSizeR, int _arenaSizeC)
    {
        m_playerPos = new Vector2(_arenaSizeR * 0.5f, _arenaSizeC * 0.5f);
        GameObject t_player = Instantiate(Player, m_playerPos, Quaternion.identity, go_floorholder.transform); //Create Player Object

        MainCamera.GetComponent<CameraController>().SetPlayer(t_player); //Spawn Player and Set the Instantiated player into Camera
        MiniMap.GetComponent<ExplorationMap>().Init();
    }

    public void MerchantSpawn()
    {
        if (mIsSpawned)
            return;
        /* Spawns merchant with the player in the same room at start of level, 
           Merchant will be at the top right corner of the room */
        //m_MerchantRoom = m_playerRoom;

        //int ranXpos = m_rooms[m_MerchantRoom].xPos + m_rooms[m_MerchantRoom].roomWidth - 2;
        //int ranYpos = m_rooms[m_MerchantRoom].yPos + m_rooms[m_MerchantRoom].roomHeight - 2;

        //m_MerchantPos = new Vector2(ranXpos, ranYpos);

        GameObject t_Merchant = Instantiate(Merchant, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
        mIsSpawned = true;
        //if (t_Merchant.GetComponent<ObjectInfo>() != null)
        //    t_Merchant.GetComponent<ObjectInfo>().Init(m_MerchantRoom, m_rooms[m_MerchantRoom], m_exitPos); //Set Starting Spawn location and detail to object
    }

    public void ExitSpawn()
    {
        go_exit = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().CloseDoor;

       // int exitRoom;

    //    do { exitRoom = Random.Range(0, m_rooms.Length - 1); }
    //    while (exitRoom == m_playerRoom);

    //    // Spawn the Exit on 1 of the tile in the room except the edges
    //    int ranXpos = Random.Range(m_rooms[exitRoom].xPos + 1, m_rooms[exitRoom].xPos + m_rooms[exitRoom].roomWidth - 1);
    //    int ranYpos = Random.Range(m_rooms[exitRoom].yPos + 1, m_rooms[exitRoom].yPos + m_rooms[exitRoom].roomHeight - 1);

    //    m_exitPos = new Vector2(ranXpos, ranYpos);

    //    GameObject t_exit = Instantiate(go_exit, m_exitPos, Quaternion.identity, go_floorholder.transform);
    //    if (t_exit.GetComponent<ObjectInfo>() != null)
    //        t_exit.GetComponent<ObjectInfo>().Init(exitRoom, m_rooms[exitRoom], m_exitPos); //Set Starting Spawn location and detail to object
    }

    public void ItemSpawn()
    {
        if (cIsSpawned)
            return;

        GameObject go_item = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;

        GameObject tempItem = Instantiate(go_item, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);

        cIsSpawned = true;
    }

    public void EnemySpawn()
    {
        //spawn enemy and init their level based on the curr floor's level
        /* floor just set to 1
         * Spawn enemy in the room, give 4 vector3 in a array as waypoint , waypoint are corners of the room
         * */
		if (eIsSpawned) {
			return;
		}
        GameObject go_enemy = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().EnemySkeleton;

        //int AmtOfEnemy = NumEnemy.Random;

        //// Safety Check
        //if (AmtOfEnemy >= m_rooms.Length)
        //{
        //    AmtOfEnemy = m_rooms.Length - 2;
        //}

        //// Temp values as holder
        //int tempRoom;
        //Vector2 tempPos = new Vector2(0, 0);

        //// Spawn the Number of Enemies in loop
        //for (int i = 0; i < AmtOfEnemy; ++i)
        //{
        //    do
        //    {
        //        // Choose a random room
        //        tempRoom = Random.Range(0, m_rooms.Length - 1);
        //    } while (tempRoom == m_playerRoom);

        //    // Setup waypoints
        //    Vector3[] Waypoint = new Vector3[4];
        //    Waypoint[0] = new Vector3(m_rooms[tempRoom].xPos, m_rooms[tempRoom].yPos);
        //    Waypoint[1] = new Vector3(m_rooms[tempRoom].xPos, m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1);
        //    Waypoint[2] = new Vector3(m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1, m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1);
        //    Waypoint[3] = new Vector3(m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1, m_rooms[tempRoom].yPos);

        //    int randomID = Random.Range(0, Waypoint.Length - 1);
        //    tempPos = Waypoint[randomID];

        //    // Instantiate Object, Set waypoint and Set room info into ObjectInfo
		GameObject tempEnemy = Instantiate(go_enemy, new Vector2(m_playerPos.x, m_playerPos.y + 5), Quaternion.identity, go_floorholder.transform);
        //    if (tempEnemy.GetComponent<SkeletonEnemyManager>() != null)
        //    {
        //        //Set EXP Reward for enemy 
        //        tempEnemy.GetComponent<SkeletonEnemyManager>().EXPReward = tempEnemy.GetComponent<SkeletonEnemyManager>().EXPRewardScaling * m_currentFloor;
        //        tempEnemy.GetComponent<SkeletonEnemyManager>().Waypoint = Waypoint;
        //        tempEnemy.GetComponent<SkeletonEnemyManager>().CurrWaypointID = randomID;
        //    }

        //    if (tempEnemy.GetComponent<ObjectInfo>() != null)
        //        tempEnemy.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        //}
		eIsSpawned = true;

    }

    public void TrapSpawn(int trapChoice)
    {
        ///* Get all traps from holder */
        GameObject go_bearTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().BearTrap;
        GameObject go_poisonTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().PoisonTrap;
        GameObject go_slowTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().SlowTrap;
        GameObject go_confusionTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ConfusionTrap;

        ///* Initialize repeatable variable for use */
        GameObject tempTrap;
        //int trapChoice;
        //int tempRoom;
        //Vector2 tempPos = new Vector2(0, 0);

        ///* Spawn required number of traps */
        //for (int i = 0; i < amt; ++i)
        //{
        //    /* Get a space that is not on player or exit */
        //    do
        //    {
        //        tempRoom = Random.Range(0, m_rooms.Length - 1);

        //        int ranXpos = Random.Range(m_rooms[tempRoom].xPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
        //                                   m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

        //        int ranYpos = Random.Range(m_rooms[tempRoom].yPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
        //                                   m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

        //        tempPos.Set(ranXpos, ranYpos);

        //    } while (tempRoom == m_playerRoom || tempPos == m_exitPos);

        //    /* Randomly choose 1 type of trap to spawn */
        //    trapChoice = Random.Range(1, 4);
        if (tIsSpawned)
            return;

        switch (trapChoice)
        {
            case 1: // Spawns a Slow Trap
                tIsSpawned = false;
                tempTrap = Instantiate(go_slowTrap, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
                tIsSpawned = true;
                break;
            case 2: // Spawns a Bear Trap (DEFAULT if no selection)
                tIsSpawned = false;
                tempTrap = Instantiate(go_bearTrap, new Vector2(m_playerPos.x, m_playerPos.y + 3), Quaternion.identity, go_floorholder.transform);
                tIsSpawned = true;
                break;
            case 3: // Spawns a Poison Trap
                tIsSpawned = false;
                tempTrap = Instantiate(go_poisonTrap, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
                tIsSpawned = true;
                break;
            case 4: // Spawns a Confusion Trap
                tIsSpawned = false;
                tempTrap = Instantiate(go_confusionTrap, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
                tIsSpawned = true;
                break;
        }

        //    if (tempTrap.GetComponent<CollisionTrapPoison>() != null)
        //        tempTrap.GetComponent<CollisionTrapPoison>().CurrentFloor = m_currentFloor;
        //    else if (tempTrap.GetComponent<CollisionBearTrap>() != null)
        //        tempTrap.GetComponent<CollisionBearTrap>().CurrentFloor = m_currentFloor;

        //    if (tempTrap.GetComponent<ObjectInfo>() != null)
        //        tempTrap.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        //}
    }

    // Getters
    public GameObject GameLevel
    {
        get { return go_floorholder; }
    }

    public int CurrentFloor
    {
        get { return m_currentFloor; }
    }
}
