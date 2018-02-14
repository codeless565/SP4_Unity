using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    private ItemManager() { Awake(); }
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

    // Use this for initialization
    void Awake()
    {
        ItemList.Add(new Sword());

        ItemList.Add(new HPpotion());
        ItemList.Add(new MPpotion());

        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());
        //ItemList.Add(new Sword());


    }

    public bool CheckItem(GameObject go)
    {
        foreach (ItemBase item in ItemList)
        {
            if (go.name == item.Name)
            {
                Debug.Log("Item Exist");
                return true;
            }
        }

        return false;
    }
}