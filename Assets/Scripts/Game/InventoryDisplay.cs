using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
    public GameObject InventoryDisplayCanvas;
    public GameObject ItemButton;
    public GameObject ItemText;
    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 3;

    GameObject[] InventoryLayout;
    Item[] InventoryItems;
    Item SelectedItem;

    // Confirmation Canvas
    public GameObject EquipConfirmationCanvas;
    bool ConfirmationCanvas;
    GameObject ItemNameRarity;
    GameObject ItemNameText;
    GameObject ItemNameStats;
    GameObject ConfirmButton;
    GameObject CancelButton;
    // Use this for initialization
    void Start () {
        InventoryLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryItems = new Item[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryDisplayCanvas.SetActive(false);

        // Shop Menu UI
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ItemButton, InventoryDisplayCanvas.transform) as GameObject;

            InventoryLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { InventoryButtonOnClick(newIcon); });
        }
        ItemNameRarity = Instantiate(ItemText, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameRarity.transform.position = new Vector3(EquipConfirmationCanvas.transform.position.x, (EquipConfirmationCanvas.transform.position.y + 125.0f));


        ItemNameText = Instantiate(ItemText, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameText.transform.position = new Vector3(EquipConfirmationCanvas.transform.position.x, (EquipConfirmationCanvas.transform.position.y + 100.0f));

        ItemNameStats = Instantiate(ItemText, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameStats.GetComponent<RectTransform>().sizeDelta = new Vector2(700.0f, 30.0f);
        ItemNameStats.transform.position = new Vector3(EquipConfirmationCanvas.transform.position.x, (EquipConfirmationCanvas.transform.position.y + 50.0f));


        ConfirmationCanvas = false;
        ConfirmButton = Instantiate(ItemButton, EquipConfirmationCanvas.transform) as GameObject;
        ConfirmButton.GetComponentInChildren<Text>().text = "Confirm";
        ConfirmButton.transform.position = new Vector3(-50.0f,  - 100.0f) + EquipConfirmationCanvas.transform.position;
        ConfirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(ConfirmEquip);

        CancelButton = Instantiate(ItemButton, EquipConfirmationCanvas.transform) as GameObject;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelEquip);
        CancelButton.transform.position = new Vector3(50.0f,- 100.0f) + EquipConfirmationCanvas.transform.position;

    }

    // Update is called once per frame
    void Update () {
        EquipConfirmationCanvas.SetActive(ConfirmationCanvas);
	}

    void ResetDisplay()
    {
        foreach (GameObject go in InventoryLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("MiscellaneousHolder").GetComponent<MiscellaneousHolder>().Empty;
        }
    }

    public void DisplayInventoryMenu(string itemtype)
    {
        ResetDisplay();
        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (item.ItemType != itemtype)
                continue;

            for (int i = 0; i < InventoryLayout.Length; ++i)
            {
                if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity)
                    break;
                else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    InventoryItems[i] = item;
                    InventoryLayout[i].name = item.Name + " " + item.ItemRarity;
                    InventoryLayout[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                    InventoryLayout[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                    InventoryLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                    break;
                }

            }
        }
    }
    public void DisplayAllEquipments()
    {
        ResetDisplay();
        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (item.ItemType == "Uses")
                continue;

            for (int i = 0;i < InventoryLayout.Length;++i)
            {
                if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity)
                    break;
                else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    InventoryItems[i] = item;
                    InventoryLayout[i].name = item.Name + " " + item.ItemRarity;
                    InventoryLayout[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                    InventoryLayout[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                    InventoryLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                    break;
                }

            }
        }

    }

    void DisplayConfirmedItem()
    {
        ItemNameText.GetComponent<Text>().text = "Confirm Equip " + SelectedItem.Name + " ? ";
        ItemNameRarity.GetComponent<Text>().text = SelectedItem.ItemRarity;


        if (SelectedItem.ItemRarity == "Common")
            ItemNameRarity.GetComponent<Text>().color = Color.white;
        else if (SelectedItem.ItemRarity == "Uncommon")
            ItemNameRarity.GetComponent<Text>().color = Color.grey;
        else if (SelectedItem.ItemRarity == "Magic")
            ItemNameRarity.GetComponent<Text>().color = Color.green;
        else if (SelectedItem.ItemRarity == "Ancient")
            ItemNameRarity.GetComponent<Text>().color = Color.yellow;
        else if (SelectedItem.ItemRarity == "Relic")
            ItemNameRarity.GetComponent<Text>().color = Color.red;


        ItemNameStats.GetComponent<Text>().text = "Level: " + SelectedItem.Level + "   " +
                                            "Health: " + SelectedItem.Health + "   " +
                                            "Mana: " + SelectedItem.Mana + "   " +
                                            "Attack: " + SelectedItem.Attack + "   " +
                                            "Defense: " + SelectedItem.Defense + "   " +
                                            "Move Speed: " + SelectedItem.MoveSpeed;

    }

    void InventoryButtonOnClick(GameObject btn)
    {
        SelectedItem = null;
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            if (btn.GetComponent<Image>().sprite.name != InventoryLayout[i].GetComponent<Image>().sprite.name)
                continue;

            if (btn.name == InventoryLayout[i].name)
                SelectedItem = InventoryItems[i];

            //SelectedItem = ItemDatabase.Instance.CheckGO(btn);
            if (SelectedItem != null)
            {
                ConfirmationCanvas = true;
                DisplayConfirmedItem();
            }
        }
    }

    void ConfirmEquip()
    {
        if (SelectedItem == null)
            return;

        if (SelectedItem.ItemType == "Uses")
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().GetComponent<InventoryBar>().AddPlayerHotBar(SelectedItem);
        else
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().EquipEQ(SelectedItem);
        ConfirmationCanvas = false;
    }
    void CancelEquip()
    {
        ConfirmationCanvas = false;
    }
    
    public void setConfirmationDisplay(bool _display) { ConfirmationCanvas = _display; }
    public GameObject getItemDisplayCanvas() { return InventoryDisplayCanvas; }
}
