using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRoyalChest : MonoBehaviour, CollisionBase
{
    private List<Item> m_ItemList = new List<Item>();

    public void CollisionResponse(string _tag)
    {
        //Give Item to player at random from the item database

        /* Rarity: C U M A R 
         * Type: Weapons, Helmets, Chestpieces, Leggings, Shoes, Uses         
         */
        float diceResult = Random.Range(0.0f, 1.0f);
        string selectedRarity;
        string monsterHouse = "MonsterHouse";

        //if (diceResult <= 0.6f)
        //    selectedRarity = "Magic";
        //else if (diceResult > 0.6f && diceResult <= 0.8f)
        //    selectedRarity = "Ancient";
        //else if (diceResult > 0.8f && diceResult <= 0.9f)
        //    selectedRarity = "Relic";
        //else
        {
            //trigger monster house 10%
            selectedRarity = monsterHouse;
        }

        if (selectedRarity != monsterHouse)
        {
            m_ItemList = ItemDatabase.Instance.GenerateItem(selectedRarity);

            if (m_ItemList.Count <= 0)
            {
                Debug.Log("Chest is Empty");
            }
            else
            {
                Item RandomItem = m_ItemList[Random.Range(0, m_ItemList.Count)];
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddItem(RandomItem);

                string Input = "You've got " + RandomItem.Name + "(" + RandomItem.ItemRarity + ")!";
                GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement(Input);
            }
        }
        else
        {
            //trigger monster house;
            GameObject go_enemy = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().EnemySkeleton;
            Room thisRoom = GetComponent<ObjectInfo>().RoomDetail;

            GameObject go_floorholder;
            int currentLevel;

            if (GameObject.FindGameObjectWithTag("GameScript") == null)
                return;

            go_floorholder = GameObject.FindGameObjectWithTag("GameScript").GetComponent<ObjectSpawn>().GameLevel;
            currentLevel = GameObject.FindGameObjectWithTag("GameScript").GetComponent<ObjectSpawn>().CurrentFloor;

            Vector3[] Waypoint = new Vector3[4];
            Waypoint[0] = new Vector3(thisRoom.xPos, thisRoom.yPos);
            Waypoint[1] = new Vector3(thisRoom.xPos, thisRoom.yPos + thisRoom.roomHeight - 1);
            Waypoint[2] = new Vector3(thisRoom.xPos + thisRoom.roomWidth - 1, thisRoom.yPos + thisRoom.roomHeight - 1);
            Waypoint[3] = new Vector3(thisRoom.xPos + thisRoom.roomWidth - 1, thisRoom.yPos);

            //spawn amt of enemy equivilent to half / 30% of the room size ||| minimal room size is 4 * 4 = 16, .'. 5 to 8 enemies at minimal
            int RoomSize = thisRoom.roomHeight * thisRoom.roomWidth;
            int AmtOfEnemy = Random.Range((int)(RoomSize * 0.3f), (int)(RoomSize * 0.5f));

            for (int i = 0; i < AmtOfEnemy; ++i)
            {
                Debug.Log("Spawning " + i);
                int randomID;
                Vector2 tempPos;

                randomID = Random.Range(0, Waypoint.Length - 1);
                tempPos = Waypoint[randomID];

                // Instantiate Object, Set waypoint and Set room info into ObjectInfo
                GameObject tempEnemy = Instantiate(go_enemy, tempPos, Quaternion.identity, go_floorholder.transform);
                if (tempEnemy.GetComponent<SkeletonEnemyManager>() != null)
                {
                    //Set EXP Reward for enemy 
                    tempEnemy.GetComponent<SkeletonEnemyManager>().EXPReward = tempEnemy.GetComponent<SkeletonEnemyManager>().EXPRewardScaling * currentLevel;
                    tempEnemy.GetComponent<SkeletonEnemyManager>().Waypoint = Waypoint;
                    tempEnemy.GetComponent<SkeletonEnemyManager>().CurrWaypointID = randomID;
                }

                if (tempEnemy.GetComponent<ObjectInfo>() != null)
                    tempEnemy.GetComponent<ObjectInfo>().Init(0, thisRoom, tempPos);
            }
            GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement("You have triggered a trap!");
        }

        Destroy(gameObject);
    }


    bool OverlapCheck(Vector3 _pos, Vector3[] _array, int _max)
    {
        for (int i = 0; i < _max; ++i)
        {
            if (_pos == _array[i])
                return true;
        }

        return false;
    }
}
