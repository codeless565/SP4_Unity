using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager
{
    private static ItemManager instance;
    private ItemManager()
    {
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