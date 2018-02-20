using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase {
    public List<ItemBase> ItemList = new List<ItemBase>();


    private static ItemDatabase instance;
    
    private ItemDatabase()
    {
        TextAsset ItemDatabase = Resources.Load<TextAsset>("ItemDB");

        string[] rowdata = ItemDatabase.text.Split(new char[] { '\n' });

        for (int i = 0; i < rowdata.Length - 1; ++i)
        {
            string[] linedata = rowdata[i].Split(new char[] { ',' });
            if (linedata[0] == "")
                break;

            if (linedata[0] == "Name")
                continue;

            Item newItem = new Item();
            newItem.Name = linedata[0];
            newItem.ItemType = linedata[1];

            int temp = 0;
            float temp2 = 0.0f;

            int.TryParse(linedata[2], out temp);
            newItem.Level = temp;
            int.TryParse(linedata[3], out temp);
            newItem.ItemCost = temp;
            int.TryParse(linedata[4], out temp);
            newItem.Health = temp;
            int.TryParse(linedata[5], out temp);
            newItem.Mana = temp;
            float.TryParse(linedata[6], out temp2);
            newItem.Attack = temp2;
            float.TryParse(linedata[7], out temp2);
            newItem.Defense = temp2;
            float.TryParse(linedata[8], out temp2);
            newItem.MoveSpeed = temp2;

            newItem._spritename = linedata[9];
            newItem.getImage();

            ItemList.Add(newItem);
        }
    }
    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = new ItemDatabase();
            return instance;
        }
    }

    void ShowData(string[] data)
    {
        foreach (string s in data)
        {
            Debug.Log(s);
        }
    }

    public ItemBase CheckGO(GameObject go)
    {
        foreach (ItemBase item in ItemList)
        {
            if (go.GetComponent<Image>().sprite.name == item.ItemImage.name)
                return item;
        }

        return null;
    }

    public void ShowItems()
    {
        foreach (ItemBase item in ItemList)
        {
            string data = item.Name + " " + item.ItemType + " " + item.ItemCost;
            Debug.Log(data);
        }
    }

    public ItemBase GenerateItem(string type)
    {
        List<ItemBase> ItemOptions = new List<ItemBase>();

        foreach (ItemBase item in ItemList)
        {
            if (item.ItemType != type)
                continue;

            ItemOptions.Add(item);
        }

        if (ItemOptions.Count > 0)
        {
            int selected = Random.Range(0, ItemOptions.Count);
            return ItemOptions[selected];
        }

        return null;
    }
}

// Adding Items?
// Add Item image into UI folder
// Go to Resource->ItemDB.csv and add in item stats