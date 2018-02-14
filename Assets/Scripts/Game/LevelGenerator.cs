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

    float m_m_FloorGridSize;
    float m_m_FloorGridOffSet;

    //Item Occupancies
    bool[] m_gridX_occupied;
    bool[] m_gridY_occupied;

    // Use this for initialization
    void Start()
    {
        m_GameLevel = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().GameLevel;
        m_Floor = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;

        //Size of grid 10x10
        m_m_FloorGridSize = m_Floor.transform.localScale.x * 0.1f;
        m_m_FloorGridOffSet = m_Floor.transform.localScale.x * 0.05f;

        //Init Item Occupancies Array
        m_gridX_occupied = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];
        m_gridY_occupied = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];

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
        GameObject Wall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;

        int safetyCount;

        //Horizontal Walls
        bool[] H_gridX = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];
        bool[] H_gridY = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];

        int posX;
        int posY;

        for (int i = 0; i < MaxNoOfHorizontalWalls; ++i)
        {
            safetyCount = 0;    //to avoid Infinite loop

            do
            {
                ++safetyCount;
                posX = Random.Range(0, H_gridX.Length);
                posY = Random.Range(0, H_gridY.Length);
            } while (H_gridX[posX] && H_gridY[posY] && safetyCount < 50);

            H_gridX[posX] = true;
            H_gridY[posY] = true;

            Vector3 pos = new Vector3(posX * m_m_FloorGridSize + m_m_FloorGridOffSet, 0, posY * m_m_FloorGridSize);
            Instantiate(Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Vertical Walls
        bool[] V_gridX = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];
        bool[] V_gridY = new bool[(int)(m_Floor.transform.localScale.x * 0.1)];

        for (int i = 0; i < MaxNoOfVerticalWalls; ++i)
        {
            safetyCount = 0;    //to avoid Infinite loop

            do
            {
                ++safetyCount;
                posX = Random.Range(0, V_gridX.Length);
                posY = Random.Range(0, V_gridY.Length);
            } while (V_gridX[posX] && V_gridY[posY] && safetyCount < 50);

            V_gridX[posX] = true;
            V_gridY[posY] = true;

            Vector3 pos = new Vector3(posX * m_m_FloorGridSize, 0, posY * m_m_FloorGridSize + m_m_FloorGridOffSet);
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
                posX = Random.Range(0, m_gridX_occupied.Length);
                posY = Random.Range(0, m_gridY_occupied.Length);
            } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) && safetyCount < 50);

            if (m_gridX_occupied[posX] && m_gridY_occupied[posY])
                continue;

            m_gridX_occupied[posX] = true;
            m_gridY_occupied[posY] = true;

            Vector3 temppos = new Vector3(posX * m_m_FloorGridSize + m_m_FloorGridOffSet, 0, posY * m_m_FloorGridSize + m_m_FloorGridOffSet);
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
            posX = Random.Range(0, m_gridX_occupied.Length);
            posY = Random.Range(0, m_gridY_occupied.Length);
        } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) && safetyCount < 50);

        m_gridX_occupied[posX] = true;
        m_gridY_occupied[posY] = true;

        float m_ExitOffset = m_m_FloorGridOffSet * 0.5f;

        Vector3 pos = new Vector3(posX * m_m_FloorGridSize + m_m_FloorGridOffSet + m_ExitOffset, 0, posY * m_m_FloorGridSize + m_m_FloorGridOffSet + m_ExitOffset);
        m_Exit = Instantiate(Exit, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
    }

    private void GeneratePlayerSpawn()
    {
        if (MinDistanceFromExit >= 5000)
            MinDistanceFromExit = 5000;

        Debug.Log("GeneratePlayerSpawn");
        //Exit
        GameObject PlayerSpawn = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().PlayerSpawnLocation;
        
        //Randomise and create an spawn location
        int posX;
        int posY;
        Vector3 pos;
        int safetyCount = 0; //to avoid Infinite loop

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_gridX_occupied.Length - 1);
            posY = Random.Range(0, m_gridY_occupied.Length - 1);
            pos = new Vector3(posX * m_m_FloorGridSize + m_m_FloorGridOffSet, 0, posY * m_m_FloorGridSize + m_m_FloorGridOffSet);
        } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) || (pos - m_Exit.transform.position).sqrMagnitude <= MinDistanceFromExit);

        Debug.Log("SqrdMagnitude: " + (pos - m_Exit.transform.position).sqrMagnitude);
        Debug.Log("Spawn Position: " + pos);

        m_gridX_occupied[posX] = true;
        m_gridY_occupied[posY] = true;

        var Spawn = Instantiate(PlayerSpawn, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        Player.transform.Translate(Spawn.transform.position.x, Spawn.transform.position.y + 1, Spawn.transform.position.z);
    }

}
