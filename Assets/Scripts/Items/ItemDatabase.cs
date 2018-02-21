using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase {
    public List<Item> ItemList = new List<Item>();


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
            newItem.ItemRarity = "Common";
            newItem._spritename = linedata[9];
            newItem.getImage();

            ItemList.Add(newItem);

            if (newItem.ItemType != "Uses")
            {
                for (int j = 1; j <= 4; ++j)
                {
                    float multipler = 0.0f;
                    Item OtherRarityItem = new Item(newItem);
                    switch (j)
                    {
                        case 1:
                            OtherRarityItem.ItemRarity = "Uncommon";
                            OtherRarityItem.Level = 10;
                            multipler = OtherRarityItem.Level / 10 * 1.5f;
                            break;
                        case 2:
                            OtherRarityItem.ItemRarity = "Magic";
                            OtherRarityItem.Level = 20;
                            multipler = OtherRarityItem.Level / 10 * 2.0f;
                            break;
                        case 3:
                            OtherRarityItem.ItemRarity = "Ancient";
                            OtherRarityItem.Level = 30;
                            multipler = OtherRarityItem.Level / 10 * 2.5f;
                            break;
                        case 4:
                            OtherRarityItem.ItemRarity = "Relic";
                            OtherRarityItem.Level = 40;
                            multipler = OtherRarityItem.Level / 10 * 3.0f;
                            break;
                    }
                    OtherRarityItem.Health *= (int)multipler;
                    OtherRarityItem.Mana *= (int)multipler;
                    OtherRarityItem.Attack *= multipler;
                    OtherRarityItem.Defense *= multipler;
                    OtherRarityItem.MoveSpeed *= multipler;

                    ItemList.Add(OtherRarityItem);
                }
            }
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

    public Item CheckGO(GameObject go)
    {
        foreach (Item item in ItemList)
        {
            if (go.GetComponent<Image>().sprite.name == item.ItemImage.name)
                return item;
        }

        return null;
    }

    public void ShowItems()
    {
        foreach (Item item in ItemList)
        {
            string data = item.Name + " " +
                          item.ItemType + " " +
                          item.Level + " " +
                          item.ItemRarity + " " +
                          item.ItemCost + " " +
                          item.Health + " " +
                          item.Mana + " " +
                          item.Attack + " " +
                          item.Defense + " " +
                          item.MoveSpeed;
            Debug.Log(data);
        }
    }

    public Item GenerateItem(string type)
    {
        List<Item> ItemOptions = new List<Item>();

        foreach (Item item in ItemList)
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
// Add Item to ItemObjectHolder
// Go to Resource->ItemDB.csv and add in item stats