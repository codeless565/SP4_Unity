using System.Collections;
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
    private bool[,] m_HorizontalWall_occupied; //this is a 2D array
    private bool[,] m_VerticalWall_occupied; //this is a 2D array
    private bool[,] m_item_occupied; //this is a 2D array
    private bool[,] m_visited;
    private Stack<Vector2> m_stack;
    private List<DeadEnd> m_deadEnds;

    //Game Object Holder
    private GameObject[,] m_horizontalWalls;
    private GameObject[,] m_verticalWalls;

    int m_deleteCount = 0;

    // Use this for initialization
    void Start()
    {
        m_Wall = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().StoneWall;

        //Size of grid 10x10
        m_GridSize = m_Wall.GetComponent<BoxCollider2D>().size.x;
        m_GridSingleOffSet = -(m_GridSize / (m_Wall.transform.childCount - 1) * 0.5f);
        m_GridHalfSize = m_GridSize * 0.5f + m_GridSingleOffSet;

        //Init Grid Occupancies Array
        int ArraySize = (int)(m_Floor.GetComponent<SpriteRenderer>().size.x / m_GridSize);
        m_HorizontalWall_occupied = new bool[ArraySize, ArraySize];
        m_VerticalWall_occupied   = new bool[ArraySize, ArraySize];
        m_item_occupied = new bool[ArraySize, ArraySize];
        m_visited = new bool[ArraySize, ArraySize];

        m_horizontalWalls = new GameObject[ArraySize, ArraySize];
        m_verticalWalls   = new GameObject[ArraySize, ArraySize];
        m_deadEnds = new List<DeadEnd>();

        GenerateWalls();
        DeadEndCheck(0, 0);
        //GenerateItem();
        Generatem_Exit();
        GeneratePlayerSpawn();
        Debug.Log(m_deleteCount);
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
            m_horizontalWalls[posX, posY] = Instantiate(m_Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
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
            m_verticalWalls[posX, posY] = Instantiate(m_Wall, pos, Quaternion.Euler(new Vector3(0, 0, 90)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
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
            m_horizontalWalls[posX, posY] = Instantiate(Wall, pos, transform.rotation, m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)

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
            m_verticalWalls[posX, posY] = Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 0, 90)), m_GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)

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
                posX = Random.Range(0, m_item_occupied.GetLength(0) - 1);
                posY = Random.Range(0, m_item_occupied.GetLength(1) - 1);
            } while (m_item_occupied[posX, posY] && safetyCount < 50);

            if (m_item_occupied[posX, posY])
                continue;

            m_item_occupied[posX, posY] = true;

            Vector3 temppos = new Vector3(posX * m_GridSize + m_GridHalfSize + m_GridSingleOffSet, posY * m_GridSize + m_GridHalfSize + m_GridSingleOffSet);
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
            posY = Random.Range(0, m_item_occupied.GetLength(1) - 1);
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

        do
        {
            ++safetyCount;
            posX = Random.Range(0, m_item_occupied.GetLength(0) - 1);
            posY = Random.Range(0, m_item_occupied.GetLength(1) - 1);
            pos = new Vector3(posX * m_GridSize + m_GridHalfSize + m_GridSingleOffSet, posY * m_GridSize + m_GridHalfSize + m_GridSingleOffSet);
        } while (m_item_occupied[posX, posY] || (pos - m_Exit.transform.position).sqrMagnitude <= MinDistanceFromExit);

        Debug.Log("SqrdMagnitude: " + (pos - m_Exit.transform.position).sqrMagnitude);
        Debug.Log("Spawn Position: " + pos);

        Player.transform.Translate(pos - Player.transform.position);
    }

    /*********************************
     * CHECKS *
     *********************************/
    private void DeadEndCheck(int posX, int posY)
    {
        //using DFS
        //add curr to stack, mark as visited, call self to move next
        //if is dead end and is not at the ends, save to list
        //move back

        //if no more possible move, check if all has been visited, if not, randomly delete 1 dead end and reset visit, call self agn from start

        m_visited[posX, posY] = true;

        //up
        if (posY + 1 < m_HorizontalWall_occupied.GetLength(1) && !m_visited[posX, posY + 1])
            if (m_HorizontalWall_occupied[posX, posY + 1])
            {
                m_deadEnds.Add(new DeadEnd(posX, posY + 1, 0));
            }
            else
            {//Move

                DeadEndCheck(posX, posY + 1);
            }

        //right
        if (posX + 1 < m_VerticalWall_occupied.GetLength(0) && !m_visited[posX + 1, posY])
            if (m_VerticalWall_occupied[posX + 1, posY])
            {
                m_deadEnds.Add(new DeadEnd(posX + 1, posY, 1));
            }
            else
            {//Move
                DeadEndCheck(posX + 1, posY);
            }

        //down
        if (posY - 1 > 0)
            if (m_HorizontalWall_occupied[posX, posY - 1] && !m_visited[posX, posY - 1])
            {
                m_deadEnds.Add(new DeadEnd(posX, posY - 1, 0));
            }
            else
            {//Move 
                DeadEndCheck(posX, posY - 1);
            }

        //left
        if (posX - 1 > 0)
            if (m_VerticalWall_occupied[posX - 1, posY] && !m_visited[posX - 1, posY])
            {
                m_deadEnds.Add(new DeadEnd(posX - 1, posY, 1));
            }
            else
            {//Move 
                DeadEndCheck(posX - 1, posY);
            }

        if (posX == 0 && posY == 0 && m_deleteCount < 100)
        {
            ++m_deleteCount;
            bool notAllVisited = false;
            for (int x = 0;x< m_visited.GetLength(0); ++x)
                for (int y = 0; y < m_visited.GetLength(1); ++y)
                {
                    if (m_visited[x, y])
                    {
                        notAllVisited = true;
                        break;
                    }
                }

            //delete 1 random deadend wall and check
            if(notAllVisited)
            {
                int chosenWall = Random.Range(0, m_deadEnds.Count);

                //Delete the wall
                if(m_deadEnds[chosenWall].type == 0)
                {
                    Destroy(m_horizontalWalls[m_deadEnds[chosenWall].GridX, m_deadEnds[chosenWall].GridY]);
                }
                else if (m_deadEnds[chosenWall].type == 1)
                {
                    Destroy(m_verticalWalls[m_deadEnds[chosenWall].GridX, m_deadEnds[chosenWall].GridY]);
                }

                ResetArray(m_visited, false);
                m_deadEnds.Clear();
                DeadEndCheck(0, 0);
            }
        }
    }

    private void ResetArray(bool[,] _array, bool value)
    {
        for (int x = 0; x < _array.GetLength(0); ++x)
            for (int y = 0; y < _array.GetLength(1); ++y)
            {
                _array[x, y] = value;
            }
    }


}

struct DeadEnd
{
    public int GridX;
    public int GridY;

    // 0 - Horizontal, 1 - Vertical
    public int type;

    public DeadEnd(int _posX, int _posY, int _type)
    {
        GridX = _posX;
        GridY = _posY;
        type = _type;
    }
}

