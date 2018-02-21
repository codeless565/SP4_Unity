using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    public GameObject Player;
    public bool RandomPlayerSpawn = false;

    public GameObject Camera;

    public IntRange NumChest = new IntRange(5, 10);
    public IntRange NumEnemy = new IntRange(5, 10);

    private Room[] m_rooms;
    private GameObject go_floorholder;
    private GameObject go_exit;
    private GameObject go_chest;
    private GameObject go_chestMimic;

    private int m_playerRoom;
    private Vector2 m_playerPos;
    private Vector2 m_exitPos;


    public void Init(int _floor)
    {
        go_floorholder = gameObject.GetComponent<BoardGenerator>().boardHolder;
        m_rooms = gameObject.GetComponent<BoardGenerator>().GetRooms();

        if (_floor == 10)
        {
            //spawn Boss / bonus floor
        }
        else
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
            tempChest.GetComponent<ObjectInfo>().Init(tempRoom, m_rooms[tempRoom], tempPos); //Set Starting Spawn location and detail to object

            //Debug.Log("Chest Info: " + tempChest.GetComponent<ObjectInfo>().RoomIndex + "  " + tempChest.GetComponent<ObjectInfo>().MapPostion);
        }
    }

    private void EnemySpawn(int _floor)
    {
        //spawn enemy and init their level based on the curr floor's level
    }
}
