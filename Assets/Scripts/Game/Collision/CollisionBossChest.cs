using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBossChest : MonoBehaviour, CollisionBase
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

        if (diceResult <= 0.5f)
            selectedRarity = "Magic";
        else if (diceResult > 0.5f && diceResult <= 0.8f)
            selectedRarity = "Ancient";
        else
            selectedRarity = "Relic";

        m_ItemList = ItemDatabase.Instance.GenerateItem(selectedRarity);

        if (m_ItemList.Count > 0)
        {
            Item RandomItem = m_ItemList[Random.Range(0, m_ItemList.Count)];
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddItem(RandomItem);

            string Input = "You've got " + RandomItem.Name + "(" + RandomItem.ItemRarity + ")!";
            GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement(Input);
        }

        Destroy(gameObject);
    }
}