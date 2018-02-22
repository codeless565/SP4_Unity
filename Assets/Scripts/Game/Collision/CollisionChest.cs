using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChest : MonoBehaviour, CollisionBase {

    private List<Item> m_ItemList = new List<Item>();
    
    public void CollisionResponse(string _tag)
    {
        //Give Item to player at random from the item database

        /* Rarity: C U M A R 
         * Type: Weapons, Helmets, Chestpieces, Leggings, Shoes, Uses         
         */

        float diceResult = Random.Range(0.0f, 1.0f);
        string selectedRarity;

        if (diceResult <= 0.4f)
            selectedRarity = "Common";
        else if (diceResult > 0.4f && diceResult <= 0.7f)
            selectedRarity = "Uncommon";
        else if (diceResult > 0.7f && diceResult <= 0.9f)
            selectedRarity = "Magic";
        else if (diceResult > 0.9f && diceResult <= 0.98f)
            selectedRarity = "Ancient";
        else
            selectedRarity = "Relic";

        m_ItemList = ItemDatabase.Instance.GenerateItem(selectedRarity);

        if (m_ItemList.Count <= 0)
        {
            Debug.Log("Chest is Empty");
        }
        else
        {
            Item RandomItem = m_ItemList[Random.Range(0, m_ItemList.Count)];
            Debug.Log("Chest item: " + RandomItem.Name + " - " + RandomItem.ItemRarity);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddItem(RandomItem);
        }

        Destroy(gameObject);
    }
}
