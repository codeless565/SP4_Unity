using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
    public GameObject InventoryDisplayCanvas;
    public GameObject EquipConfirmationCanvas;

    private GameObject ButtonPrefab;
    private GameObject TextPrefab;
    private GameObject BorderPrefab;

    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 4;

    // Pages Preview
    int StartCount; // Counter for ItemDatabase
    int MaxCount;   // Maximum number of pages
    int PageCount;  // Current page number

    string currenttag; // Tag to store currently viewed display
    GameObject[] InventoryLayout;
    GameObject[] InventoryBorders;
    Item[] InventoryItems;
    Item SelectedItem;

    // Confirmation
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
        PageCount = 0;
        MaxCount = 5;
        StartCount = 0;
        currenttag = "";
        ConfirmationCanvas = false;

        InventoryLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryBorders = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryItems = new Item[NumberOfItemsPerRow * MaxNumberOfColumn];

        ButtonPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().ButtonPrefab;
        TextPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().TextPrefab;
        BorderPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderPrefab;

        // Inventory Display UI
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ButtonPrefab, InventoryDisplayCanvas.transform) as GameObject;

            InventoryLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { InventoryButtonOnClick(newIcon); });

            GameObject newBorder = Instantiate(BorderPrefab, InventoryLayout[i].transform);
            newBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(55.0f, 55.0f);
            InventoryBorders[i] = newBorder;
        }

        ItemNameRarity = Instantiate(TextPrefab, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameRarity.transform.position = new Vector3(0.0f, 125.0f) + EquipConfirmationCanvas.transform.position;

        ItemNameText = Instantiate(TextPrefab, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameText.transform.position = new Vector3(0.0f,100.0f) + EquipConfirmationCanvas.transform.position;

        ItemNameStats = Instantiate(TextPrefab, EquipConfirmationCanvas.transform) as GameObject;
        ItemNameStats.GetComponent<RectTransform>().sizeDelta = new Vector2(700.0f, 30.0f);
        ItemNameStats.transform.position = new Vector3(0.0f, 50.0f) + EquipConfirmationCanvas.transform.position;

        // Confirmation
        ConfirmButton = Instantiate(ButtonPrefab, EquipConfirmationCanvas.transform) as GameObject;
        ConfirmButton.transform.position = new Vector3(-50.0f, -100.0f) + EquipConfirmationCanvas.transform.position;
        ConfirmButton.GetComponentInChildren<Text>().text = "Confirm";
        ConfirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(ConfirmEquip);
        

        CancelButton = Instantiate(ButtonPrefab, EquipConfirmationCanvas.transform) as GameObject;
        CancelButton.transform.position = new Vector3(50.0f, -100.0f) + EquipConfirmationCanvas.transform.position;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelEquip);
        
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        EquipConfirmationCanvas.SetActive(ConfirmationCanvas);
    }
    
    // On Click listener for inventory buttons
    void InventoryButtonOnClick(GameObject btn)
    {
        SelectedItem = null;
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            // If btn clicked is not the same as current InventoryLayout[Button] 
            if (btn.name != InventoryLayout[i].name)
                continue;

            SelectedItem = InventoryItems[i];

            // If there is an Item, Display SelectedItem
            if (SelectedItem != null)
            {
                ConfirmationCanvas = true;
                DisplayConfirmedItem();
            }
        }
    }

    // Display Functions
    ////////////////////////////////////////
    // Display Reset
    void ResetDisplay()
    {
        foreach (GameObject go in InventoryLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
        for (int i = 0; i < InventoryItems.Length; ++i)
        {
            if (InventoryItems[i] != null)
                InventoryItems[i] = null;
        }
        foreach (GameObject go in InventoryBorders)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
    }
    // Display Specific type 
    public void DisplayInventoryMenu(string itemtype)
    {
        ResetDisplay();
        currenttag = itemtype;
        int itemcount = 0;

        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory())
        {
            if (item.ItemType != itemtype)
                continue;

            itemcount++;

            if (itemcount >= StartCount)
            {
                for (int i = 0; i < InventoryLayout.Length; ++i)
                {
                    // If item name and rarity already exist in Inventory layout
                    if (InventoryLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && InventoryItems[i].ItemRarity == item.ItemRarity && InventoryItems[i].Name == item.Name)
                        break;
                    // If current layout is not empty
                    else if (InventoryLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        continue;
                    // Provide Border and add to InventoryLayout/InventoryItems/InventoryBorder
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
    // Display All Equipments
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
    // Display Search Result
    public void DisplaySearchMenu(InputField itemname)
    {
        ResetDisplay();

        foreach (Item item in GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory())
        {
            if (!item.Name.Contains(itemname.text) && !item.Name.ToLower().Contains(itemname.text) && !item.Name.ToUpper().Contains(itemname.text))
                continue;

            currenttag = item.ItemType;

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

    // Display Selected Item
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


        if (SelectedItem.ItemType == "Uses")
            ItemNameStats.GetComponent<Text>().text = "Level: " + SelectedItem.Level + "   " +
                                                    "Health: " + SelectedItem.Health + "   " +
                                                    "Stamina: " + SelectedItem.Stamina + "   " +
                                                    "Attack: " + SelectedItem.Attack + "   " +
                                                    "Defense: " + SelectedItem.Defense + "   " +
                                                    "Move Speed: " + SelectedItem.MoveSpeed;
        else
            ItemNameStats.GetComponent<Text>().text = "Level: " + SelectedItem.Level + "   " +
                                                    "Health: " + SelectedItem.MaxHealth + "   " +
                                                    "Stamina: " + SelectedItem.MaxStamina + "   " +
                                                    "Attack: " + SelectedItem.Attack + "   " +
                                                    "Defense: " + SelectedItem.Defense + "   " +
                                                    "Move Speed: " + SelectedItem.MoveSpeed;
    }

    // On Click Listener for Confirm/Cancel Buttons
    void ConfirmEquip()
    {
        if (SelectedItem == null)
            return;

        if (SelectedItem.ItemType == "Uses")
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().GetComponent<InventoryBar>().AddPlayerHotBar(SelectedItem);
        else
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().EquipEQ(SelectedItem);
        ConfirmationCanvas = false;
    }
    void CancelEquip()
    {
        ConfirmationCanvas = false;
    }

    // Function for Previous and Next Button
    public void ViewPrevious()
    {
        if (PageCount - 1 >= 0)
            PageCount--;

            StartCount = PageCount * NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipments();
        else
            DisplayInventoryMenu(currenttag);
        gameObject.GetComponent<Inventory>().PreviousPageButton.GetComponent<Image>().color = Color.red;
        gameObject.GetComponent<Inventory>().NextPageButton.GetComponent<Image>().color = Color.cyan;
    }
    public void ViewNext()
    {
        if (PageCount + 1 < MaxCount)
            PageCount++;

        StartCount = PageCount*NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipments();
        else
            DisplayInventoryMenu(currenttag);
        gameObject.GetComponent<Inventory>().PreviousPageButton.GetComponent<Image>().color = Color.cyan;
        gameObject.GetComponent<Inventory>().NextPageButton.GetComponent<Image>().color = Color.red;
    }

    public void setConfirmationDisplay(bool _display) { ConfirmationCanvas = _display; }
    public int getPageCount() { return PageCount; }
    public int getMaxCount() { return MaxCount; }
}
