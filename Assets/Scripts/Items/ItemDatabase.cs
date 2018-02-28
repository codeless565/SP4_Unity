using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase
{
    public List<Item> ItemList = new List<Item>();

    private static ItemDatabase instance;
    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = new ItemDatabase();
            return instance;
        }
    }
    private ItemDatabase()
    {
        TextAsset ItemSpecialName = Resources.Load<TextAsset>("ItemNames");
        string[] listofspecialname = ItemSpecialName.text.Split(new char[] { '\n' });

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
            newItem.ItemRarity = linedata[2];
            float temp2 = 0.0f;

            float.TryParse(linedata[3], out temp2);
            newItem.Level = (int)temp2;
            float.TryParse(linedata[4], out temp2);
            newItem.ItemCost = (int)temp2;
            float.TryParse(linedata[5], out temp2);
            newItem.EXP = temp2;
            float.TryParse(linedata[6], out temp2);
            newItem.Health = temp2;
            float.TryParse(linedata[7], out temp2);
            newItem.MaxHealth = temp2;
            float.TryParse(linedata[8], out temp2);
            newItem.Stamina = temp2;
            float.TryParse(linedata[9], out temp2);
            newItem.MaxStamina = temp2;
            float.TryParse(linedata[10], out temp2);
            newItem.Attack = temp2;
            float.TryParse(linedata[11], out temp2);
            newItem.Defense = temp2;
            float.TryParse(linedata[12], out temp2);
            newItem.MoveSpeed = temp2;
            
            newItem._spritename = linedata[13];
            newItem.getImage();

            ItemList.Add(newItem);

            bool massproduce;
            bool.TryParse(linedata[14], out massproduce);
            bool special;
            bool.TryParse(linedata[15], out special);
            if (newItem.ItemRarity == "Common" && massproduce)
            {
                for (int j = 1; j <= 4; ++j)
                {
                    float multipler = 0.0f;
                    Item OtherRarityItem = new Item(newItem);
                    switch (j)
                    {
                        case 1:
                            OtherRarityItem.ItemRarity = "Uncommon";
                            multipler = OtherRarityItem.Level / 10 * 1.5f;
                            break;
                        case 2:
                            OtherRarityItem.ItemRarity = "Magic";
                            multipler = OtherRarityItem.Level / 10 * 2.0f;
                            break;
                        case 3:
                            OtherRarityItem.ItemRarity = "Ancient";
                            multipler = OtherRarityItem.Level / 10 * 2.5f;
                            break;
                        case 4:
                            OtherRarityItem.ItemRarity = "Relic";
                            multipler = OtherRarityItem.Level / 10 * 3.0f;
                            break;
                    }
                    OtherRarityItem.Level = newItem.Level;
                    OtherRarityItem._spritename = newItem._spritename;
                    OtherRarityItem.Health *= multipler;
                    OtherRarityItem.MaxHealth *= multipler;
                    OtherRarityItem.Stamina *= multipler;
                    OtherRarityItem.MaxStamina *= multipler;
                    OtherRarityItem.Attack *= multipler;
                    OtherRarityItem.Defense *= multipler;
                    OtherRarityItem.MoveSpeed *= multipler;

                    ItemList.Add(OtherRarityItem);
                }
            }

            if (newItem.ItemType != "Uses" && special)
            {
                // Special Items
                for (int k = 1; k < listofspecialname.Length - 1; ++k)
                {
                    float multipler = 0.0f;
                    float specialmultipler = 1.0f;
                    string[] itemdetails = listofspecialname[k].Split(new char[] { ',' });

                    Item OtherRarityItem = new Item(newItem);

                    if (itemdetails[1] == "0") // suffix
                        OtherRarityItem.Name = itemdetails[0] + " " + OtherRarityItem.Name;
                    else if (itemdetails[1] == "1") // prefix
                        OtherRarityItem.Name = OtherRarityItem.Name + " " + itemdetails[0];

                    float.TryParse(itemdetails[2], out specialmultipler);
                    if (specialmultipler >= 10.0f)
                        specialmultipler = 10.0f;

                    OtherRarityItem.ItemRarity = "Relic";
                    multipler = OtherRarityItem.Level / 10 * 3.0f * specialmultipler;

                    OtherRarityItem.Health *= multipler;
                    OtherRarityItem.MaxHealth *= multipler;
                    OtherRarityItem.Stamina *= multipler;
                    OtherRarityItem.MaxStamina *= multipler;
                    OtherRarityItem.Attack *= multipler;
                    OtherRarityItem.Defense *= multipler;
                    OtherRarityItem.MoveSpeed *= multipler;

                    ItemList.Add(OtherRarityItem);
                }
            }
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
                          item.Stamina + " " +
                          item.Attack + " " +
                          item.Defense + " " +
                          item.MoveSpeed;
            Debug.Log(data);
        }
    }

    public List<Item> GenerateItem(string _rarity) // Generating Items
    {
        List<Item> ItemOptions = new List<Item>(); // List of Possible Items

        foreach (Item item in ItemList)
        {
            if (item.ItemRarity != _rarity)
                continue;

            ItemOptions.Add(item);                  // If item rarity is as requested add into List of Possible Items
        }

        return ItemOptions;
    }

    public Item getItem(string itemname, string _rarity)
    {
        foreach (Item item in ItemList)
        {
            if (item.Name == itemname)
            {
                if (item.ItemRarity == _rarity)
                    return item;
            }
        }
        return null;
    }
}

// Adding Items?
// Add Item image into UI folder
// Add Item to ItemObjectHolder
// Go to Resource->ItemDB.csv and add in item stats