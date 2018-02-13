using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject GameLevel = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().GameLevel;
        GameObject Floor  = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;
    
        GameObject Wall = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;
        GameObject Pillar = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StonePillar;

        //Size of grid 10x10
        float FloorGridSize = Floor.transform.localScale.x * 0.1f;
        float FloorGridOffSet = Floor.transform.localScale.x * 0.05f;

        ////Pillars
        //for (int i = 0; i < 10; ++i)
        //{
        //    int posX = (int)((Random.value * 0.8f) * 10);
        //    int posY = (int)((Random.value * 0.8f) * 10);
        //    Vector3 pos = new Vector3(posX * FloorGridSize, 0, posY * FloorGridSize);
        //    Instantiate(Pillar, pos, transform.rotation, GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        //}

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

            Vector3 pos = new Vector3(posX * FloorGridSize + FloorGridOffSet, 0, posY * FloorGridSize);
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

            Vector3 pos = new Vector3(posX * FloorGridSize, 0, posY * FloorGridSize + FloorGridOffSet);
            Instantiate(Wall, pos, Quaternion.Euler(new Vector3(0, 90, 0)), GameLevel.transform); //Instantiate(GameObject, Position, quaternion, Parent)
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    /* TODO
     * Spawn Check with other structures
     * spawn exit last and check with all existing structures
     */
}
