﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
    public enum TileType
    {
        Wall, Floor
    }

    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;                                    // The number of rows on the board (how tall it will be).
    public IntRange numRooms       = new IntRange(15, 20);    // The range of the number of rooms there can be.
    public IntRange numCorridors   = new IntRange(15, 20);    // The range of the number of corridors there can be.
    public IntRange roomWidth  = new IntRange(3, 10);          // The range of widths rooms can have.
    public IntRange roomHeight = new IntRange(3, 10);         // The range of heights rooms can have.
    public IntRange corridorLength = new IntRange(6, 10);     // The range of lengths corridors between rooms can have.
    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;                            // An array of wall tile prefabs.
    public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private Room[] rooms;                                     // All the rooms that are created for this board.
    private List<Corridor> corridors;                             // All the corridors that connect the rooms.
    public GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
    
    public void Init()
    {
        if (boardHolder == null)
        {
            boardHolder = new GameObject("Level");
        }

        // Safety Check for numCorridor
        if (numCorridors.m_Min < numRooms.m_Max)
        {
            numCorridors.m_Min = numRooms.m_Max + 5;
            numCorridors.m_Max = numCorridors.m_Min + 5;
        }

        // Create the board holder.
        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }
    
    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        tiles = new TileType[columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            tiles[i] = new TileType[rows];
        }
    }
    
    void CreateRoomsAndCorridors()
    {
        // Create the rooms array with a random size.
        rooms = new Room[numRooms.Random];

        // There should be one less corridor than there is rooms.
        int rand = numCorridors.Random;
        corridors = new List<Corridor>(rand);
        //Debug.Log(rand +  "   " + corridors.Count);

        // Create the first room and corridor.
        rooms[0] = new Room();
        // Setup the first room, there is no previous corridor so we do not use one.
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        for (int startingCorr = 0; startingCorr < 4; ++startingCorr)
        {         // ... create a corridor.
            Corridor firstCorridor = new Corridor();
            // Setup the first corridor using the first room.
            firstCorridor.SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, startingCorr);

            corridors.Add(firstCorridor);
        }

        for (int i = 1; i < rooms.Length; i++)
        {
            // Create a room.
            rooms[i] = new Room();

            List<int> notConnectedCorrsIndex = new List<int>();

            // Setup the room base on a unConnectedTo corridor
            for (int corIndex = 0; corIndex < corridors.Count; ++corIndex)
            {
                if (corridors[corIndex].connectedTo == false)
                {
                    notConnectedCorrsIndex.Add(corIndex);
                }
            }

            //choose randomly from the list of unconnected corridors
            if (notConnectedCorrsIndex.Count > 0)
            {
                int randomChoice = Random.Range(0, notConnectedCorrsIndex.Count);
                rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[notConnectedCorrsIndex[randomChoice]]);
            }
            else
            {
                rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);
            }

            notConnectedCorrsIndex.Clear();

            // If we haven't reached the end of the corridors array...
            if (corridors.Count < corridors.Capacity)
            {
                Corridor tempCorridor = new Corridor();
                int firstDir = tempCorridor.SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
                corridors.Add(tempCorridor);
            }
        }
    }

    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; ++i)
        {
            Room currentRoom = rooms[i];

            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.roomWidth; ++j)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    if (yCoord < 0)
                        yCoord = 0;

                    // The coordinates in the jagged array are based on the room's position and it's width and height.
//                    Debug.Log("x " + xCoord + " : " + "  ||  " + " y " + yCoord + " : ");
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }
    
    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < corridors.Count; ++i)
        {
            Corridor currentCorridor = corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; ++j)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor.
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }
    
    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall.
                    InstantiateFromArray(wallTiles, i, j);
                    continue;
                }
                // If not, Instantiate a floor
                InstantiateFromArray(floorTiles, i, j);
            }
        }
    }
    
    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }
    
    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }
    
    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }
    
    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity, boardHolder.transform) as GameObject;
    }

    public Room[] GetRooms()
    { return rooms; }

    public void DestroyLevel()
    {
        Destroy(boardHolder);
    }

    public bool CheckLevel()
    {
        if (boardHolder == null)
            return false;

        return true;
    }
}

[System.Serializable]
public class IntRange
{
    public int m_Min;       // The minimum value in this range.
    public int m_Max;       // The maximum value in this range.

    // Constructor to set the values.
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }


    // Get a random value from the range.
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}
