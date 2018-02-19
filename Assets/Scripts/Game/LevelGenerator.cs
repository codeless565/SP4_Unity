using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /* TODO
     * Spawn Check with other structures
     * spawn m_Exit last and check with all existing structures
     */
    public GameObject Player;

    public int MaxNoOfHorizontalWalls = 10;
    public int MaxNoOfVerticalWalls   = 10;
    public int MinNoOfChest = 5;
    public int MaxNoOfChest = 5;
    public float MinDistanceFromExit = 500;

    private GameObject m_GameLevel;
    private GameObject m_Floor;
    private GameObject m_Exit;

    float m_FloorGridSize;
    float m_FloorGridOffSet;

    //Grid Occupancies
    bool[,] m_HorizontalWall_occupied; //this is a 3D array
    bool[,] m_VerticalWall_occupied; //this is a 3D array
    bool[,] m_item_occupied; //this is a 3D array

    // Use this for initialization
    void Start()
    {
        m_GameLevel = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().GameLevel;
        m_Floor = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;

        //Size of grid 10x10
        m_FloorGridSize = m_Floor.transform.localScale.x * 0.1f;
        m_FloorGridOffSet = m_Floor.transform.localScale.x * 0.05f;

        //Init Grid Occupancies Array
        m_HorizontalWall_occupied = new bool[(int)(m_Floor.transform.localScale.x * 0.1), (int)(m_Floor.transform.localScale.x * 0.1)];
        m_VerticalWall_occupied   = new bool[(int)(m_Floor.transform.localScale.x * 0.1), (int)(m_Floor.transform.localScale.x * 0.1)];

        m_item_occupied = new bool[(int)(m_Floor.transform.localScale.x * 0.1), (int)(m_Floor.transform.localScale.x * 0.1)];

        GenerateWalls();
        GenerateItem();
        Generatem_Exit();
        GeneratePlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateWalls()
    {
        // Outer Walls
        GameObject BoarderWall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().BoarderWall;
        Instantiate(BoarderWall, m_GameLevel.transform.position, m_GameLevel.transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)

        // Inner Walls
        GameObject Wall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;
        
        int safetyCount;
        int posX;
        int posY;

        //Horizontal Walls
        for (int i = 0; i < MaxNoOfHorizontalWalls; ++i)
        {
            safetyCount = 0;    //to avoid Infinite loop

            do
            {
                ++safetyCount;
                posX = Random.Range(0, m_HorizontalWall_occupied.GetLength(0));
                posY = Random.Range(1, m_HorizontalWall_occupied.GetLength(1));
            } while (m_HorizontalWall_occupied[posX, posY] && safetyCount < 50);

            m_HorizontalWall_occupied[posX, posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize);
            Instantiate(Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Vertical Walls
        for (int i = 0; i < MaxNoOfVerticalWalls; ++i)
        {
            safetyCount = 0;    //to avoid Infinite loop

            do
            {
                ++safetyCount;
                posX = Random.Range(1, m_VerticalWall_occupied.GetLength(0));
                posY = Random.Range(0, m_VerticalWall_occupied.GetLength(1));
            } while (m_VerticalWall_occupied[posX, posY] && safetyCount < 50);

            m_VerticalWall_occupied[posX, posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 90, 0)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void GenerateItem()
    {
        //Chests
        GameObject Chest = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().WoodenChest;

        int ChestSpawns = Random.Range(MinNoOfChest, MaxNoOfChest);
        int posX;
        int posY;

        for (int i = 0; i < ChestSpawns; ++i)
        {
            int safetyCount = 0;    //to avoid Infinite loop
            do
            {
                ++safetyCount;
                posX = Random.Range(0, m_item_occupied.GetLength(0));
                posY = Random.Range(0, m_item_occupied.GetLength(1));
            } while (m_item_occupied[posX, posY] && safetyCount < 50);

            if (m_item_occupied[posX, posY])
                continue;

            m_item_occupied[posX, posY] = true;

            Vector3 temppos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Chest, temppos, Quaternion.Euler(new Vector3(0, 180, 0)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void Generatem_Exit()
    {
        //m_Exit
        GameObject Exit = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Exit;
        
        //Randomise and create an m_Exit
        int posX;
        int posY;
        int safetyCount = 0; //to avoid Infinite loop

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_item_occupied.GetLength(0));
            posY = Random.Range(0, m_item_occupied.GetLength(1));
        } while (m_item_occupied[posX, posY] && safetyCount < 50);

        m_item_occupied[posX, posY] = true;

        float m_ExitOffset = m_FloorGridOffSet * 0.5f;

        Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet + m_ExitOffset, 0, posY * m_FloorGridSize + m_FloorGridOffSet + m_ExitOffset);
        m_Exit = Instantiate(Exit, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
    }

    private void GeneratePlayerSpawn()
    {
        // Safety check
        if (MinDistanceFromExit >= 5000)
            MinDistanceFromExit = 5000;

        //Exit
        GameObject PlayerSpawn = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().PlayerSpawnLocation;
        
        // Randomise and create an spawn location
        int safetyCount = 0; //to avoid Infinite loop
        int posX;
        int posY;
        Vector3 pos;

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_item_occupied.GetLength(0));
            posY = Random.Range(0, m_item_occupied.GetLength(1));
            pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
        } while (m_item_occupied[posX, posY] || (pos - m_Exit.transform.position).sqrMagnitude <= MinDistanceFromExit);

        Debug.Log("SqrdMagnitude: " + (pos - m_Exit.transform.position).sqrMagnitude);
        Debug.Log("Spawn Position: " + pos);

        m_item_occupied[posX, posY] = true;

        var Spawn = Instantiate(PlayerSpawn, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        Player.transform.Translate(Spawn.transform.position.x, Spawn.transform.position.y, Spawn.transform.position.z);
    }

}
