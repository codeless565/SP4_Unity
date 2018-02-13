using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    /* TODO
     * Spawn Check with other structures
     * spawn exit last and check with all existing structures
     */

    private GameObject GameLevel;
    private GameObject Floor;

    float m_FloorGridSize;
    float m_FloorGridOffSet;

    // Use this for initialization
    void Start () {
        GameLevel = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().GameLevel;
        Floor     = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;

        //Size of grid 10x10
        m_FloorGridSize   = Floor.transform.localScale.x * 0.1f;
        m_FloorGridOffSet = Floor.transform.localScale.x * 0.05f;

        GenerateWalls();
        GenerateExit_Item();
    }

    // Update is called once per frame
    void Update () {
		
	}
        
    private void GenerateWalls()
    {
        GameObject Wall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;

        //Grid Occupancies
        bool[] gridX_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];
        bool[] gridY_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];

        //Vertical Walls
        for (int i = 0; i < 40; ++i)
        {
            int safetyCount = 0;
            int posX;
            int posY;
            do
            {
                ++safetyCount;
                posX = (int)(Random.value * 10);
                posY = (int)(Random.value * 10);
            } while ((gridX_occupied[posX] && gridY_occupied[posY]) && safetyCount < 50);

            gridX_occupied[posX] = true;
            gridY_occupied[posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize);
            Instantiate(Wall, pos, transform.rotation, GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Horizontal Walls
        for (int i = 0; i < 40; ++i)
        {
            int safetyCount = 0;
            int posX;
            int posY;
            do
            {
                ++safetyCount;
                posX = (int)(Random.value * 10);
                posY = (int)(Random.value * 10);
            } while ((gridX_occupied[posX] && gridY_occupied[posY]) && safetyCount < 50);

            gridX_occupied[posX] = true;
            gridY_occupied[posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void GenerateExit_Item()
    {

        //Grid Occupancies
        bool[] gridX_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];
        bool[] gridY_occupied = new bool[(int)(Floor.transform.localScale.x * 0.1)];

        //Exit
        GameObject Exit = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Exit;
        for (int i = 0; i < 1; ++i)
        {
            int safetyCount = 0;
            int posX;
            int posY;
            do
            {
                ++safetyCount;
                posX = (int)(Random.value * 10);
                posY = (int)(Random.value * 10);
            } while ((gridX_occupied[posX] && gridY_occupied[posY]) && safetyCount < 50);

            gridX_occupied[posX] = true;
            gridY_occupied[posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Exit, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Chests
        GameObject Chest = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().WoodenChest;

        int ChestSpawns = (int)(Random.value * 10);

        for (int i = 0; i < ChestSpawns; ++i)
        {
            int safetyCount = 0;
            int posX;
            int posY;
            do
            {
                ++safetyCount;
                posX = (int)(Random.value * 10);
                posY = (int)(Random.value * 10);
            } while ((gridX_occupied[posX] && gridY_occupied[posY]) && safetyCount < 50);

            gridX_occupied[posX] = true;
            gridY_occupied[posY] = true;

            Vector3 pos = new Vector3(posX * m_FloorGridSize + m_FloorGridOffSet, 0, posY * m_FloorGridSize + m_FloorGridOffSet);
            Instantiate(Chest, pos, Quaternion.Euler(new Vector3(0, 180, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

    }
}
