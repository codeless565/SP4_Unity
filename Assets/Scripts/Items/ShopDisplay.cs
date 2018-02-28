using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    public GameObject ShopDisplayCanvas;
    public GameObject ShopConfirmationCanvas;

    private GameObject ButtonPrefab;
    private GameObject ItemPrefab;
    private GameObject TextPrefab;
    private GameObject BorderPrefab;

    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 4;

    // Pages Preview
    int StartCount; // Counter for ItemDatabase
    public int MaxCount = 5;   // Maximum number of pages
    int PageCount;  // Current page number

    string currenttag; // Tag to store currently viewed display
    GameObject[] ShopLayout;
    Item[] ShopItems;
    Item SelectedItem;

    // Confirmation
    bool ConfirmationDisplay;
    GameObject ItemNameRarity;
    GameObject ItemNameText;
    GameObject ItemNameStats;
    GameObject CostText;
    GameObject GoldText;
    GameObject BuyButton;
    GameObject CancelButton;
    
    GameObject QuantityText;
    int Quantity;
    GameObject QuantityAdd;
    GameObject QuantitySubtract;

    private GameObject Player;
    // Use this for initialization
    public void Init()
    {
        PageCount = 0;
        StartCount = 0;
        currenttag = "";
        ConfirmationDisplay = false;

        ShopLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        ShopItems = new Item[NumberOfItemsPerRow * MaxNumberOfColumn];

        ButtonPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().ButtonPrefab;
        ItemPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().ItemPrefab;

        TextPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().TextPrefab;
        BorderPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderPrefab;

        // Shop Display UI
        for (int i = 0; i < ShopLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ItemPrefab, ShopDisplayCanvas.transform) as GameObject;

            ShopLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { ShopButtonOnClick(newIcon); });
        }


        // Confirmation UI
        ItemNameRarity = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameRarity.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 300.0f);

        ItemNameText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 250.0f);

        ItemNameStats = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameStats.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 150.0f);

        CostText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        CostText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300.0f, 0.0f);

        GoldText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        GoldText.GetComponent<RectTransform>().anchoredPosition = new Vector3(150.0f, 0.0f);

        Quantity = 1;
        QuantityText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        QuantityText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300.0f, -100.0f);

        QuantityAdd = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantityAdd.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Plus;
        QuantityAdd.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantityAdd.GetComponent<Button>().onClick.AddListener(AddQuantity);
        QuantityAdd.GetComponent<RectTransform>().anchoredPosition = new Vector3(50.0f, -100.0f);

        QuantitySubtract = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantitySubtract.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Minus;
        QuantitySubtract.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantitySubtract.GetComponent<Button>().onClick.AddListener(SubtractQuantity);
        QuantitySubtract.GetComponent<RectTransform>().anchoredPosition = new Vector3(250.0f, -100.0f);

        BuyButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        BuyButton.GetComponentInChildren<Text>().text = "Buy";
        BuyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        BuyButton.GetComponent<Button>().onClick.AddListener(BuyItem);
        BuyButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100.0f, -250.0f);

        CancelButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelBuy);
        CancelButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(100.0f, -250.0f);


        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ShopConfirmationCanvas.SetActive(ConfirmationDisplay);
        if (ConfirmationDisplay) // Update Text when Confirmation is active
        {
            CostText.GetComponent<Text>().text = "Item cost: " + (SelectedItem.ItemCost * Quantity).ToString();
            QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        }
    }

    // On Click listener for shop buttons
    void ShopButtonOnClick(GameObject btn)
    {
        SelectedItem = null;
        
        for (int i = 0; i < ShopLayout.Length; ++i)
        {
            // If btn clicked is not the same as current ShopLayout[Button] 
            if (btn.name != ShopLayout[i].name)
                continue;

            SelectedItem = ShopItems[i];
            // If there is an Item, Display SelectedItem
            if (SelectedItem != null)
            {
                ConfirmationDisplay = true;
                DisplayConfirmedItem();
            }
        }
    }

    // Display Functions
    ////////////////////////////////////////
    // Display Reset
    void ResetDisplay()
    {
        foreach (GameObject go in ShopLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
            go.transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;

        }
        for (int i = 0; i < ShopItems.Length; ++i)
        {
            ShopItems[i] = null;
        }
    }
    // Display Specific type 
    public void DisplayShopMenu(string itemtype)
    {
        ResetDisplay();
        currenttag = itemtype;
        int itemcount = 0;
        foreach (Item item in ItemDatabase.Instance.ItemList)
        {
            if (item.ItemType != itemtype)
                continue;
            itemcount++;

            if (itemcount >= StartCount)
            {
                for (int i = 0; i < ShopLayout.Length; ++i)
                {
                    // If item name and rarity already exist in shop layout
                    if (ShopLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && ShopItems[i].ItemRarity == item.ItemRarity && ShopItems[i].Name == item.Name)
                        break;
                    // If current layout is not empty
                    else if (ShopLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        continue;
                    // Provide Border and add to ShopLayout/ShopItems/ShopBorder
                    else
                    {
                        if (item.ItemRarity == "Common")
                            ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                        else if (item.ItemRarity == "Uncommon")
                            ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                        else if (item.ItemRarity == "Magic")
                            ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                        else if (item.ItemRarity == "Ancient")
                            ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                        else if (item.ItemRarity == "Relic")
                            ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                        ShopItems[i] = item;
                        ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                        break;
                    }
                }
            }
        }
    } 
    // Display All Equipments
    public void DisplayAllEquipment()
    {
        ResetDisplay();

        int itemcount = 0;
        foreach (Item item in ItemDatabase.Instance.ItemList)
        {
            currenttag = "all";
            if (item.ItemType == "Uses")
                continue;

            itemcount++;
            
            if (itemcount >= StartCount)
            {
                for (int i = 0; i < ShopLayout.Length; ++i)
                {

                    if (ShopLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && ShopItems[i].ItemRarity == item.ItemRarity && ShopItems[i].Name == item.Name)
                        {
                            break;
                        }
                        else if (ShopLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        {
                            continue;
                        }
                        else
                        {
                            if (item.ItemRarity == "Common")
                                ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                            else if (item.ItemRarity == "Uncommon")
                                ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                            else if (item.ItemRarity == "Magic")
                                ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                            else if (item.ItemRarity == "Ancient")
                                ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                            else if (item.ItemRarity == "Relic")
                                ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;

                            ShopItems[i] = item;
                            ShopLayout[i].name = item.Name + " " + item.ItemRarity;
                            ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
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

        foreach (Item item in ItemDatabase.Instance.ItemList)
        {
            if (!item.Name.Contains(itemname.text) && !item.Name.ToLower().Contains(itemname.text) && !item.Name.ToUpper().Contains(itemname.text))
                continue;

            currenttag = item.ItemType;

            for (int i = 0; i < ShopLayout.Length; ++i)
            {
                if (ShopLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && ShopItems[i].ItemRarity == item.ItemRarity && ShopItems[i].Name == item.Name)
                    break;
                else if (ShopLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    if (item.ItemRarity == "Common")
                        ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                    else if (item.ItemRarity == "Uncommon")
                        ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                    else if (item.ItemRarity == "Magic")
                        ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                    else if (item.ItemRarity == "Ancient")
                        ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                    else if (item.ItemRarity == "Relic")
                        ShopLayout[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                    ShopItems[i] = item;
                    ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                    break;
                }

            }
        }
    }
    
    // Display Selected Item
    void DisplayConfirmedItem()
    {
        ItemNameText.GetComponent<Text>().text = SelectedItem.Name;
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
            ItemNameStats.GetComponent<Text>().text =   "Level: " + SelectedItem.Level + "   " +
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
        GoldText.GetComponent<Text>().text = "Your Gold: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats().gold.ToString();
    }

    // On Click Listener for Buy/Cancel Buttons
    void BuyItem()
    {
        for (int i = 1; i <= Quantity; ++i)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddItem(SelectedItem);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddGold(-SelectedItem.ItemCost * Quantity);
        GameObject.FindGameObjectWithTag("GameScript").GetComponent<CreateAnnouncement>().MakeAnnouncement("Succesfully Purchased " + SelectedItem.Name + "!");

        ConfirmationDisplay = false;
        Quantity = 1;
    }
    void CancelBuy()
    {
        ConfirmationDisplay = false;
        Quantity = 1;
    }

    // On Click Listener for Add/Subtract Buttons
    void AddQuantity()
    {
        if ((SelectedItem.ItemCost * (Quantity + 1)) <= GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats().gold)
            Quantity++;
    }
    void SubtractQuantity()
    {
        if (Quantity - 1 <= 1)
            Quantity = 1;
        else
            Quantity--;
    }

    // Function for Previous and Next Button
    public void ViewPrevious()
    {
        if (PageCount - 1 >= 0)
            PageCount--;

            StartCount = PageCount * NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipment();
        else
            DisplayShopMenu(currenttag);
        gameObject.GetComponent<Shop>().PreviousPageButton.GetComponent<Image>().color = Color.red;
        gameObject.GetComponent<Shop>().NextPageButton.GetComponent<Image>().color = Color.cyan;
    }
    public void ViewNext()
    {
        if (PageCount + 1 < MaxCount)
            PageCount++;

        StartCount = PageCount * NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipment();
        else
            DisplayShopMenu(currenttag);

        gameObject.GetComponent<Shop>().PreviousPageButton.GetComponent<Image>().color = Color.cyan;
        gameObject.GetComponent<Shop>().NextPageButton.GetComponent<Image>().color = Color.red;
    }

    public void setConfirmationDisplay(bool _display) { ConfirmationDisplay = _display; }
    public int getPageCount() { return PageCount; }
    public int getMaxCount() { return MaxCount; }
}