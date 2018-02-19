using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager
{
    private static ItemManager instance;
    private ItemManager()
    {
        ItemList.Add(new Helmet());

        ItemList.Add(new Sword());
        ItemList.Add(new Axe());

        ItemList.Add(new HPpotion());
        ItemList.Add(new MPpotion());
    }
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ItemManager();
            return instance;
        }
    }

    public List<ItemBase> ItemList = new List<ItemBase>();

    public ItemBase CheckGO(GameObject go)
    {
        foreach(ItemBase item in ItemList)
        {
            if (go.GetComponent<Image>().sprite.name == item.getItemImage().name)
                return item;
        }

        return null;
    }
}

// Adding Items?
// Get the image
// Change Sprite Texture Type to Sprite (2D and UI)
//Go to holder and declare sprite/item name
// Throw sprite into holder in editor 
// Create new Item Class 
// Make sure type is correct (Check on ItemBase)
// Throw into ItemManager in Constructor