using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    //Player
    public GameObject Player;
    public bool RandomPlayerSpawn = false;
    private int m_playerRoom;
    private Vector2 m_playerPos;

    //Merchant
    public GameObject Merchant;
    private int m_MerchantRoom;
    private Vector2 m_MerchantPos;

    //Main Camera/Player Camera
    public GameObject MainCamera;
    public GameObject MiniMap;

    //Chests
    public IntRange NumChest = new IntRange(5, 10);
    public IntRange NumRoyalChest = new IntRange(0, 2);

    //Enemy
    public IntRange NumEnemy = new IntRange(5, 10);

    //Traps
    public IntRange NumTraps = new IntRange(5, 10);

    //GO
    private Room[] m_rooms;
    private GameObject go_floorholder;
    private GameObject go_exit;
    private Vector2 m_exitPos;

    private int m_currentFloor;


    public void Init(int _floor)
    {
        m_currentFloor = _floor;

        go_floorholder = gameObject.GetComponent<BoardGenerator>().boardHolder;
        m_rooms = gameObject.GetComponent<BoardGenerator>().GetRooms();

        GameObject go_chest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;
        GameObject go_royalchest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().RoyalChest;

        /* Spawn Mechant only at every 5th floor */
        //if (m_currentFloor % 5 == 0)
        {
            MerchantSpawn();
        }

        /* Spawn Boss floor only at every 10th floor */
        if (m_currentFloor % 10 == 0)
        {
        }

        /* Normal spawns */
        //Player and Exit
        PlayerSpawn();
        ExitSpawn();

        // Chests
        ItemSpawn(go_chest, NumChest.Random);
        ItemSpawn(go_royalchest, NumRoyalChest.Random);

        // Traps
        TrapSpawn(NumTraps.Random);

        // Enemies
        EnemySpawn(_floor);
    }

    private void PlayerSpawn()
    {
        m_playerRoom = 0;

        if (RandomPlayerSpawn)
            m_playerRoom = Random.Range(0, m_rooms.Length - 1);

        m_playerPos = new Vector2(m_rooms[m_playerRoom].xPos + m_rooms[m_playerRoom].roomWidth * 0.5f, m_rooms[m_playerRoom].yPos + m_rooms[m_playerRoom].roomHeight * 0.5f);
        GameObject t_player = Instantiate(Player, m_playerPos, Quaternion.identity, go_floorholder.transform); //Create Player Object
        if (t_player.GetComponent<ObjectInfo>() != null)
            t_player.GetComponent<ObjectInfo>().Init(m_playerRoom, m_rooms[m_playerRoom], m_playerPos); //Set Starting Spawn location and detail to object

        MainCamera.GetComponent<CameraController>().SetPlayer(t_player); //Spawn Player and Set the Instantiated player into Camera
        MiniMap.GetComponent<ExplorationMap>().Init();
    }

    private void MerchantSpawn()
    {
        /* Spawns merchant with the player in the same room at start of level, 
           Merchant will be at the top right corner of the room */
        m_MerchantRoom = m_playerRoom;

        int ranXpos = m_rooms[m_MerchantRoom].xPos + m_rooms[m_MerchantRoom].roomWidth - 2;
        int ranYpos = m_rooms[m_MerchantRoom].yPos + m_rooms[m_MerchantRoom].roomHeight - 2;

        m_MerchantPos = new Vector2(ranXpos, ranYpos);

        GameObject t_Merchant = Instantiate(Merchant, m_MerchantPos, Quaternion.identity, go_floorholder.transform);
        if (t_Merchant.GetComponent<ObjectInfo>() != null)
            t_Merchant.GetComponent<ObjectInfo>().Init(m_MerchantRoom, m_rooms[m_MerchantRoom], m_exitPos); //Set Starting Spawn location and detail to object
    }

    private void ExitSpawn()
    {
        go_exit = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().CloseDoor;

        int exitRoom;

        do { exitRoom = Random.Range(0, m_rooms.Length - 1); }
        while (exitRoom == m_playerRoom);

        // Spawn the Exit on 1 of the tile in the room except the edges
        int ranXpos = Random.Range(m_rooms[exitRoom].xPos + 1, m_rooms[exitRoom].xPos + m_rooms[exitRoom].roomWidth - 1);
        int ranYpos = Random.Range(m_rooms[exitRoom].yPos + 1, m_rooms[exitRoom].yPos + m_rooms[exitRoom].roomHeight - 1);

        m_exitPos = new Vector2(ranXpos, ranYpos);

        GameObject t_exit = Instantiate(go_exit, m_exitPos, Quaternion.identity, go_floorholder.transform);
        if (t_exit.GetComponent<ObjectInfo>() != null)
            t_exit.GetComponent<ObjectInfo>().Init(exitRoom, m_rooms[exitRoom], m_exitPos); //Set Starting Spawn location and detail to object

        //Debug.Log("Exit Info: " + go_exit.GetComponent<ObjectInfo>().RoomIndex + "  " + go_exit.GetComponent<ObjectInfo>().MapPostion);
    }

    private void ItemSpawn(GameObject _Item, int amt)
    {
        int tempRoom;
        Vector2 tempPos = new Vector2(0, 0);

        for (int i = 0; i < amt; ++i)
        {
            do
            {
                tempRoom = Random.Range(0, m_rooms.Length - 1);

                int ranXpos = Random.Range(m_rooms[tempRoom].xPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
                                           m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

                int ranYpos = Random.Range(m_rooms[tempRoom].yPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
                                           m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

                tempPos.Set(ranXpos, ranYpos);

            } while (tempPos == m_playerPos || tempPos == m_exitPos);

            GameObject tempItem = Instantiate(_Item, tempPos, Quaternion.identity, go_floorholder.transform);
            if (tempItem.GetComponent<ObjectInfo>() != null)
                tempItem.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        }
    }

    private void EnemySpawn(int _floor)
    {
        //spawn enemy and init their level based on the curr floor's level
        /*
         * Spawn enemy in the room, give 4 vector3 in a array as waypoint , waypoint are corners of the room
         * */

        GameObject go_enemy = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().EnemySkeleton;

        int AmtOfEnemy = NumEnemy.Random;

        // Safety Check
        if (AmtOfEnemy >= m_rooms.Length)
        {
            AmtOfEnemy = m_rooms.Length - 2;
        }

        // Temp values as holder
        int tempRoom;
        Vector2 tempPos = new Vector2(0, 0);

        // Spawn the Number of Enemies in loop
        for (int i = 0; i < AmtOfEnemy; ++i)
        {
            do
            {
                // Choose a random room
                tempRoom = Random.Range(0, m_rooms.Length - 1);
            } while (tempRoom == m_playerRoom);

            // Setup waypoints
            Vector3[] Waypoint = new Vector3[4];
            Waypoint[0] = new Vector3(m_rooms[tempRoom].xPos, m_rooms[tempRoom].yPos);
            Waypoint[1] = new Vector3(m_rooms[tempRoom].xPos, m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1);
            Waypoint[2] = new Vector3(m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1, m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1);
            Waypoint[3] = new Vector3(m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1, m_rooms[tempRoom].yPos);

            int randomID = Random.Range(0, Waypoint.Length - 1);
            tempPos = Waypoint[randomID];

            // Instantiate Object, Set waypoint and Set room info into ObjectInfo
            GameObject tempEnemy = Instantiate(go_enemy, tempPos, Quaternion.identity, go_floorholder.transform);
            if (tempEnemy.GetComponent<SkeletonEnemyManager>() != null)
            {
                //Set EXP Reward for enemy 
                tempEnemy.GetComponent<SkeletonEnemyManager>().EXPReward = tempEnemy.GetComponent<SkeletonEnemyManager>().EXPRewardScaling * m_currentFloor;
                tempEnemy.GetComponent<SkeletonEnemyManager>().Waypoint = Waypoint;
                tempEnemy.GetComponent<SkeletonEnemyManager>().CurrWaypointID = randomID;
            }

            if (tempEnemy.GetComponent<ObjectInfo>() != null)
                tempEnemy.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        }

    }

    private void TrapSpawn(int amt)
    {
        /* Get all traps from holder */
        GameObject go_bearTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().BearTrap;
        GameObject go_poisonTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().PoisonTrap;
        GameObject go_slowTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().SlowTrap;
        GameObject go_confusionTrap = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ConfusionTrap;

        /* Initialize repeatable variable for use */
        GameObject tempTrap;
        int trapChoice;
        int tempRoom;
        Vector2 tempPos = new Vector2(0, 0);

        /* Spawn required number of traps */
        for (int i = 0; i < amt; ++i)
        {
            /* Get a space that is not on player or exit */
            do
            {
                tempRoom = Random.Range(0, m_rooms.Length - 1);

                int ranXpos = Random.Range(m_rooms[tempRoom].xPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
                                           m_rooms[tempRoom].xPos + m_rooms[tempRoom].roomWidth - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

                int ranYpos = Random.Range(m_rooms[tempRoom].yPos + 1, // +1 to avoid spawning on edge of the room and potentially block the entrance
                                           m_rooms[tempRoom].yPos + m_rooms[tempRoom].roomHeight - 1); // -1 to avoid spawning on edge of the room and potentially block the entrance

                tempPos.Set(ranXpos, ranYpos);

            } while (tempRoom == m_playerRoom || tempPos == m_exitPos);

            /* Randomly choose 1 type of trap to spawn */
            trapChoice = Random.Range(1, 4);

            switch (trapChoice)
            {
                case 1: // Spawns a Poison Trap
                    tempTrap = Instantiate(go_poisonTrap, tempPos, Quaternion.identity, go_floorholder.transform);
                    break;
                case 2: // Spawns a Slow Trap
                    tempTrap = Instantiate(go_slowTrap, tempPos, Quaternion.identity, go_floorholder.transform);
                    break;
                case 3: // Spawns a Confusion Trap
                    tempTrap = Instantiate(go_confusionTrap, tempPos, Quaternion.identity, go_floorholder.transform);
                    break;
                default: // Spawns a Bear Trap (DEFAULT if no selection)
                    Debug.Log("Spawned Bear");
                    tempTrap = Instantiate(go_bearTrap, tempPos, Quaternion.identity, go_floorholder.transform);
                    break;
            }

            if (tempTrap.GetComponent<CollisionTrapPoison>() != null)
                tempTrap.GetComponent<CollisionTrapPoison>().CurrentFloor = m_currentFloor;
            else if (tempTrap.GetComponent<CollisionBearTrap>() != null)
                tempTrap.GetComponent<CollisionBearTrap>().CurrentFloor = m_currentFloor;

            if (tempTrap.GetComponent<ObjectInfo>() != null)
                tempTrap.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        }
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
