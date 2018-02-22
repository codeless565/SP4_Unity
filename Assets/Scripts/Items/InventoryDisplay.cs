using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
    public GameObject InventoryDisplayCanvas;
    public GameObject ItemButton;
    public GameObject ItemText;
    public GameObject BorderPrefab;


    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 4;

    int StartCount;
    string currenttag;
    GameObject[] InventoryLayout;
    GameObject[] InventoryBorders;
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

    private GameObject Player;
    // Use this for initialization
    void Start ()
    { 
        StartCount = 0;
        currenttag = "";

        InventoryLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryBorders = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryItems = new Item[NumberOfItemsPerRow * MaxNumberOfColumn];


        // Shop Menu UI
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ItemButton, InventoryDisplayCanvas.transform) as GameObject;

            InventoryLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { InventoryButtonOnClick(newIcon); });

            GameObject newBorder = Instantiate(BorderPrefab, InventoryLayout[i].transform);
            newBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(55.0f, 55.0f);
            InventoryBorders[i] = newBorder;

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

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        EquipConfirmationCanvas.SetActive(ConfirmationCanvas);
	}

    void ResetDisplay()
    {
        foreach (GameObject go in InventoryLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
        for (int i = 0; i < InventoryItems.Length; ++i)
        {
            InventoryItems[i] = null;
        }
        foreach (GameObject go in InventoryBorders)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
    }

    public void DisplayInventoryMenu(string itemtype)
    {
        ResetDisplay();
        currenttag = itemtype;
        int itemcount = 0;

        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (item.ItemType != itemtype)
                continue;

            itemcount++;

            if (itemcount >= StartCount)
            {
                for (int i = 0; i < InventoryLayout.Length; ++i)
                {
                    if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity && InventoryItems[i].Name == item.Name)
                        break;
                    else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        continue;
                    else
                    {
                        if (item.ItemRarity == "Common")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                        else if (item.ItemRarity == "Uncommon")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                        else if (item.ItemRarity == "Magic")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                        else if (item.ItemRarity == "Ancient")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                        else if (item.ItemRarity == "Relic")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                        InventoryItems[i] = item;
                        InventoryLayout[i].name = item.Name + " " + item.ItemRarity;

                        if (item.Quantity > 1)
                        {
                            InventoryLayout[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                            InventoryLayout[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                        }
                        InventoryLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                        break;
                    }
                }
            }
        }
    }
    public void DisplaySearchMenu(InputField itemname)
    {
        ResetDisplay();

        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (!item.Name.Contains(itemname.text) && !item.Name.ToLower().Contains(itemname.text) && !item.Name.ToUpper().Contains(itemname.text))
                continue;

            for (int i = 0; i < InventoryLayout.Length; ++i)
            {
                if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity && InventoryItems[i].Name == item.Name)
                    break;
                else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    if (item.ItemRarity == "Common")
                        InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                    else if (item.ItemRarity == "Uncommon")
                        InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                    else if (item.ItemRarity == "Magic")
                        InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                    else if (item.ItemRarity == "Ancient")
                        InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                    else if (item.ItemRarity == "Relic")
                        InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                    InventoryItems[i] = item;
                    InventoryLayout[i].name = item.Name + " " + item.ItemRarity;

                    if (item.Quantity > 1)
                    {
                        InventoryLayout[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                        InventoryLayout[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                    }
                    InventoryLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                    break;
                }

            }
        }
    }

    public void DisplayAllEquipments()
    {
        ResetDisplay();
        currenttag = "all";
        int itemcount = 0;

        foreach (Item item in Player.GetComponent<Player2D_Manager>().getPlayerInventory())
        {
            if (item.ItemType == "Uses")
                continue;
            itemcount++;

            if (itemcount >= StartCount)
            {
                for (int i = 0; i < InventoryLayout.Length; ++i)
                {
                    if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity && InventoryItems[i].Name == item.Name)
                        break;
                    else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        continue;
                    else
                    {
                        if (item.ItemRarity == "Common")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                        else if (item.ItemRarity == "Uncommon")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                        else if (item.ItemRarity == "Magic")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                        else if (item.ItemRarity == "Ancient")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                        else if (item.ItemRarity == "Relic")
                            InventoryBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                        InventoryItems[i] = item;
                        InventoryLayout[i].name = item.Name + " " + item.ItemRarity;
                        if (item.Quantity > 1)
                        {
                            InventoryLayout[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                            InventoryLayout[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                        }
                        InventoryLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                        break;
                    }
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

    public void ViewPage1()
    {
        StartCount = 0;
        if (currenttag == "all")
            DisplayAllEquipments();
        else
            DisplayInventoryMenu(currenttag);
        gameObject.GetComponent<Inventory>().Page1Button.GetComponent<Image>().color = Color.red;
        gameObject.GetComponent<Inventory>().Page2Button.GetComponent<Image>().color = Color.cyan;
    }
    public void ViewPage2()
    {
        StartCount = NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipments();
        else
            DisplayInventoryMenu(currenttag);
        gameObject.GetComponent<Inventory>().Page1Button.GetComponent<Image>().color = Color.cyan;
        gameObject.GetComponent<Inventory>().Page2Button.GetComponent<Image>().color = Color.red;
    }

    public void setConfirmationDisplay(bool _display) { ConfirmationCanvas = _display; }
}
