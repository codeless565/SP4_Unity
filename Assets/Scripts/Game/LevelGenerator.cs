using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /* TODO
     * Spawn Check with other structures
     * spawn exit last and check with all existing structures
     */
    public GameObject Player;

    private GameObject GameLevel;
    private GameObject Floor;
    private GameObject Exit;

    float m_FloorGridSize;
    float m_FloorGridOffSet;

    //Item Occupancies
    bool[] m_gridX_occupied;
    bool[] m_gridY_occupied;

    // Use this for initialization
    void Start()
    {
        GameLevel = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().GameLevel;
        Floor = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;

        //Size of grid 10x10
        m_FloorGridSize = Floor.transform.localScale.x * 0.1f;
        m_FloorGridOffSet = Floor.transform.localScale.x * 0.05f;

        //Init Item Occupancies Array
        m_gridX_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];
        m_gridY_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];

        GenerateWalls();
        GenerateItem();
        GeneratePlayerSpawn();
        GenerateExit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateWalls()
    {
        GameObject Wall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;

        //Horizontal Walls
        bool[] H_gridX = new bool[(int)(Floor.transform.localScale.x * 0.1)];
        bool[] H_gridY = new bool[(int)(Floor.transform.localScale.x * 0.1)];

        int posX;
        int posY;

        for (int i = 0; i < 50; ++i)
        {
            int safetyCount = 0;    //to avoid Infinite loop
            do
            {
                ++safetyCount;
                posX = Random.Range(0, 10);
                posY = Random.Range(0, 10);
            } while (H_gridX[posX] && H_gridY[posY] && safetyCount < 100);

            if (H_gridX[posX] == false || H_gridY[posY] == false)
            {
                H_gridX[posX] = true;
                H_gridY[posY] = true;

                Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize);
                Instantiate(Wall, pos, transform.rotation, GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
            }
        }

        //Vertical Walls
        bool[] V_gridX = new bool[(int)(Floor.transform.localScale.x * 0.1)];
        bool[] V_gridY = new bool[(int)(Floor.transform.localScale.x * 0.1)];

        for (int i = 0; i < 50; ++i)
        {
            int safetyCount = 0;    //to avoid Infinite loop
            do
            {
                ++safetyCount;
                posX = Random.Range(0, 10);
                posY = Random.Range(0, 10);
            } while (V_gridX[posX] && V_gridY[posY] && safetyCount < 100);

            if (V_gridX[posX] == false || V_gridY[posY] == false)
            {
                V_gridX[posX] = true;
                V_gridY[posY] = true;

                Vector3 pos = new Vector3(posX * m_FloorGridSize, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
                Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
            }
        }
    }

    private void GenerateItem()
    {
        //Chests
        GameObject Chest = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().WoodenChest;

        int ChestSpawns = (int)(Random.value * 10);
        int posX;
        int posY;

        for (int i = 0; i < ChestSpawns; ++i)
        {
            int safetyCount = 0;    //to avoid Infinite loop
            do
            {
                ++safetyCount;
                posX = Random.Range(0, 10);
                posY = Random.Range(0, 10);
            } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) && safetyCount < 50);

            m_gridX_occupied[posX] = true;
            m_gridY_occupied[posY] = true;

            Vector3 temppos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Chest, temppos, Quaternion.Euler(new Vector3(0, 180, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void GeneratePlayerSpawn()
    {
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
            posX = Random.Range(0, 10);
            posY = Random.Range(0, 10);
            pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
        } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) && safetyCount < 50);

        Debug.Log("SqrdMagnitude: " + (pos - Exit.transform.position).sqrMagnitude.ToString());
        Debug.Log("Floor halved LScale: " + (Floor.transform.localScale.x * 0.5f).ToString());
        m_gridX_occupied[posX] = true;
        m_gridY_occupied[posY] = true;

        Instantiate(PlayerSpawn, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
    }

    private void GenerateExit()
    {
        //Exit
        GameObject Exit = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Exit;


        //Randomise and create an Exit
        int posX;
        int posY;
        int safetyCount = 0; //to avoid Infinite loop

        do
        {
            ++safetyCount;
            posX = Random.Range(0, 10);
            posY = Random.Range(0, 10);
        } while ((m_gridX_occupied[posX] && m_gridY_occupied[posY]) && safetyCount < 50);

        m_gridX_occupied[posX] = true;
        m_gridY_occupied[posY] = true;

        float ExitOffset = m_FloorGridOffSet * 0.5f;

        Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet + ExitOffset, 0, posY * m_FloorGridSize + m_FloorGridOffSet + ExitOffset);
        Exit = Instantiate(Exit, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
    }
}
