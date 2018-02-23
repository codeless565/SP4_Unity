using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    //Player
    public  GameObject Player;
    public  bool RandomPlayerSpawn = false;
    private int m_playerRoom;
    private Vector2 m_playerPos;

    //Main Camera/Player Camera
    public GameObject Camera;

    //Chests
    public IntRange NumChest = new IntRange(5, 10);
    public IntRange NumRoyalChest = new IntRange(0, 2);

    //Enemy
    public GameObject Enemy;
    public IntRange NumEnemy = new IntRange(5, 10);

    //GO
    private Room[] m_rooms;
    private GameObject go_floorholder;
    private GameObject go_exit;
    private GameObject go_chest;
    private GameObject go_royalchest;

    private Vector2 m_exitPos;

    private int m_currentFloor;


    public void Init(int _floor)
    {
        m_currentFloor = _floor;

        go_floorholder = gameObject.GetComponent<BoardGenerator>().boardHolder;
        m_rooms        = gameObject.GetComponent<BoardGenerator>().GetRooms();

        //if (m_currentFloor == 10)
        //{
        //    //spawn Boss / bonus floor
        //}
        //else
        {
            PlayerSpawn();
            ExitSpawn();
            ItemSpawn(_floor);
            EnemySpawn(_floor);
        }
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

        Camera.GetComponent<CameraController>().SetPlayer(t_player); //Spawn Player and Set the Instantiated player into Camera
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

    private void ItemSpawn(int _floor)
    {
        go_chest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;

        int AmtOfChest = NumChest.Random;
        int tempRoom;
        Vector2 tempPos = new Vector2(0, 0);

        for (int i = 0; i < AmtOfChest; ++i)
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

            GameObject tempChest = Instantiate(go_chest, tempPos, Quaternion.identity, go_floorholder.transform);
            if (tempChest.GetComponent<ObjectInfo>() != null)
                tempChest.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object
        }
    }

    private void EnemySpawn(int _floor)
    {
        //spawn enemy and init their level based on the curr floor's level
        /*
         * Spawn enemy in the room, give 4 vector3 in a array as waypoint , waypoint are corners of the room
         * */

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
            GameObject tempEnemy = Instantiate(Enemy, tempPos, Quaternion.identity, go_floorholder.transform);
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
}
