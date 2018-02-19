﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TODO
 * Dead End checks
 */

public class LevelGenerator2D : MonoBehaviour
{
    /* TODO
     * Spawn Check with other structures
     * spawn m_Exit last and check with all existing structures
     */
    public GameObject Player;

    public int MaxNoOfHorizontalWalls = 10;
    public int MaxNoOfVerticalWalls = 10;
    public int MinNoOfChest = 5;
    public int MaxNoOfChest = 5;
    public float MinDistanceFromExit = 500;

    public GameObject m_GameLevel;
    public GameObject m_Floor;
    private GameObject m_Wall;
    private GameObject m_Exit;

    float m_GridSize;
    float m_GridSingleOffSet;
    float m_GridHalfSize;

    //Grid Occupancies
    bool[,] m_HorizontalWall_occupied; //this is a 3D array
    bool[,] m_VerticalWall_occupied; //this is a 3D array
    bool[,] m_item_occupied; //this is a 3D array

    // Use this for initialization
    void Start()
    {
        m_Wall = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().StoneWall;

        //Size of grid 10x10
        m_GridSize = m_Wall.GetComponent<BoxCollider2D>().size.x;
        m_GridSingleOffSet = -(m_GridSize / (m_Wall.transform.childCount - 1) * 0.5f);
        m_GridHalfSize = m_GridSingleOffSet * (m_Wall.transform.childCount * 0.5f);

        Debug.Log("m_GridSingleOffSet: " + m_GridSingleOffSet);

        //Init Grid Occupancies Array
        int ArraySize = (int)(m_Floor.GetComponent<SpriteRenderer>().size.x / m_GridSize);
        m_HorizontalWall_occupied = new bool[ArraySize, ArraySize];
        m_VerticalWall_occupied = new bool[ArraySize, ArraySize];
        m_item_occupied = new bool[ArraySize, ArraySize];

        GenerateWalls();
        //GenerateItem();
        Generatem_Exit();
        GeneratePlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*********************************
     * GENERATORS *
     *********************************/
    private void GenerateWalls()
    {
        // Outer Walls
        GenerateBorder();

        // Inner Walls
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
                posX = Random.Range(0, m_HorizontalWall_occupied.GetLength(0) - 1);
                posY = Random.Range(1, m_HorizontalWall_occupied.GetLength(1));
            } while (m_HorizontalWall_occupied[posX, posY] && safetyCount < 50);

            m_HorizontalWall_occupied[posX, posY] = true;

            Vector3 pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(m_Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Vertical Walls
        for (int i = 0; i < MaxNoOfVerticalWalls; ++i)
        {
            safetyCount = 0;    //to avoid Infinite loop

            do
            {
                ++safetyCount;
                posX = Random.Range(1, m_VerticalWall_occupied.GetLength(0));
                posY = Random.Range(0, m_VerticalWall_occupied.GetLength(1) - 1);
            } while (m_VerticalWall_occupied[posX, posY] && safetyCount < 50);

            m_VerticalWall_occupied[posX, posY] = true;

            Vector3 pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(m_Wall, pos, Quaternion.Euler(new Vector3(0, 0, 90)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void GenerateBorder()
    {
        GameObject Wall = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().StoneWall;
        int posX;
        int posY;
        Vector3 pos;

        //Horizontal Border ==
        for (int x = 0; x < m_HorizontalWall_occupied.GetLength(0); ++x)
        {
            posX = x;
            posY = 0;

            m_HorizontalWall_occupied[posX, posY] = true;
            pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)

            //out of range (back border)
            posX = x;
            posY = m_HorizontalWall_occupied.GetLength(1);

            pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }

        //Vertical Border ||
        for (int y = 0; y < m_VerticalWall_occupied.GetLength(1); ++y)
        {
            posX = 0;
            posY = y;

            m_VerticalWall_occupied[posX, posY] = true;
            pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 0, 90)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)

            //out of range (back border)
            posX = m_VerticalWall_occupied.GetLength(0);
            posY = y;

            pos = new Vector3(posX * m_GridSize + m_GridSingleOffSet, posY * m_GridSize + m_GridSingleOffSet);
            Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 0, 90)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void GenerateItem()
    {
        //Chests
        GameObject Chest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().WoodenChest;

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

            Vector3 temppos = new Vector3(posX * m_GridSize + m_GridSingleOffSet + m_GridHalfSize, posY * m_GridSize + m_GridSingleOffSet + m_GridHalfSize);
            Instantiate(Chest, temppos, Quaternion.Euler(new Vector3(0, 180, 0)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    private void Generatem_Exit()
    {
        //m_Exit
        GameObject Exit = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().CloseDoor;

        //Randomise and create an m_Exit
        int posX;
        int posY;
        int safetyCount = 0; //to avoid Infinite loop

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_item_occupied.GetLength(0) - 1);
            posY = Random.Range(1, m_item_occupied.GetLength(1));
        } while (m_item_occupied[posX, posY] && safetyCount < 50);

        m_item_occupied[posX, posY] = true;

        //float m_ExitOffset = m_GridSingleOffSet * 0.5f;

        Vector3 pos = new Vector3(posX * m_GridSize + m_GridHalfSize + m_GridSingleOffSet, posY * m_GridSize + m_GridHalfSize + m_GridSingleOffSet);
        m_Exit = Instantiate(Exit, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
    }

    private void GeneratePlayerSpawn()
    {
        // Safety check
        if (MinDistanceFromExit >= 5000)
            MinDistanceFromExit = 5000;

        // Randomise and create an spawn location
        int safetyCount = 0; //to avoid Infinite loop
        int posX;
        int posY;
        Vector3 pos;

        float halfGridSize = m_GridSize * 0.5f;

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_item_occupied.GetLength(0) - 1);
            posY = Random.Range(1, m_item_occupied.GetLength(1));
            pos = new Vector3(posX * m_GridSize + halfGridSize, posY * m_GridSize + halfGridSize);
        } while (m_item_occupied[posX, posY] || (pos - m_Exit.transform.position).sqrMagnitude <= MinDistanceFromExit);

        Debug.Log("SqrdMagnitude: " + (pos - m_Exit.transform.position).sqrMagnitude);
        Debug.Log("Spawn Position: " + pos);

        Player.transform.Translate(pos - Player.transform.position);
    }

    /*********************************
     * CHECKS *
     *********************************/
    private void DeadEndCheck()
    {

    }
}

