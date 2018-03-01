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
    bool t1IsSpawned, t2IsSpawned, t3IsSpawned, t4IsSpawned;
    bool exitIsSpawned = false;

    public void Init()
    {
        m_currentFloor = 1;

        go_floorholder = gameObject.GetComponent<ArenaGenerator>().boardHolder;
        m_arenaSizeRow = gameObject.GetComponent<ArenaGenerator>().rows;
        m_arenaSizeColum = gameObject.GetComponent<ArenaGenerator>().columns;

        t1IsSpawned = t2IsSpawned = t3IsSpawned = t4IsSpawned = false;

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

    public void ExitSpawn()
    {
        if (exitIsSpawned)
            return;

        go_exit = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().CloseDoor;

        GameObject t_exit = Instantiate(go_exit, m_exitPos, Quaternion.identity, go_floorholder.transform);
        exitIsSpawned = true;
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
    public void TrapSpawn1()
    {
        GameObject tempTrap;

        if (t1IsSpawned)
            return;
        GameObject go_slowTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().SlowTrap;
        tempTrap = Instantiate(go_slowTrap, new Vector2(m_playerPos.x - 4, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
        t1IsSpawned = true;
    }
    public void TrapSpawn2()
    {
        GameObject tempTrap;

        if (t2IsSpawned)
            return;
        GameObject go_bearTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().BearTrap;
        tempTrap = Instantiate(go_bearTrap, new Vector2(m_playerPos.x - 2, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
        t2IsSpawned = true;
    }
    public void TrapSpawn3()
    {
        GameObject tempTrap;

        if (t3IsSpawned)
            return;
        GameObject go_poisonTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().PoisonTrap;
        tempTrap = Instantiate(go_poisonTrap, new Vector2(m_playerPos.x, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
        t3IsSpawned = true;
    }
    public void TrapSpawn4()
    {
        GameObject tempTrap;

        if (t4IsSpawned)
            return;
        GameObject go_confusionTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ConfusionTrap;

        tempTrap = Instantiate(go_confusionTrap, new Vector2(m_playerPos.x +2, m_playerPos.y + 2), Quaternion.identity, go_floorholder.transform);
        t4IsSpawned = true;
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
