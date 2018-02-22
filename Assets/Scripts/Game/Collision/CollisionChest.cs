using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChest : MonoBehaviour, CollisionBase {

    private List<Item> m_ItemList = new List<Item>();
    
    public void CollisionResponse(string _tag)
    {
        //Give Item to player at random from the item database
        m_ItemList = ItemDatabase.Instance.ItemList;

        /* Rarity: C U M A R 
         * Type: Weapons, Helmets, Chestpieces, Leggings, Shoes, Uses         
         */

        //float diceResult = Random
    }
}
