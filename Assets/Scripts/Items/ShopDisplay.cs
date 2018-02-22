using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    public GameObject ShopDisplayCanvas;
    public GameObject ShopConfirmationCanvas;

    public GameObject ButtonPrefab;
    public GameObject TextPrefab;
    public GameObject BorderPrefab;

    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 4;

    int StartCount;
    int MaxCount;
    int PageCount;

    string currenttag;
    GameObject[] ShopLayout;
    GameObject[] ShopBorders;
    Item[] ShopItems;
    Item SelectedItem;

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

    // Use this for initialization
    public void Init()
    {
        PageCount = 0;
        MaxCount = 5;
        StartCount = 0;
        currenttag = "";
        ConfirmationDisplay = false;
        ShopLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        ShopBorders = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        ShopItems = new Item[NumberOfItemsPerRow * MaxNumberOfColumn];
        
        
        ShopDisplayCanvas.SetActive(false);

        // Shop Menu UI
        for (int i = 0; i < ShopLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ButtonPrefab, ShopDisplayCanvas.transform) as GameObject;

            ShopLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { ShopButtonOnClick(newIcon); });

            GameObject newBorder = Instantiate(BorderPrefab, ShopLayout[i].transform);
            newBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(55.0f, 55.0f);
            ShopBorders[i] = newBorder;
        }


        // Confirmation Menu UI
        ItemNameRarity = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameRarity.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x, (ShopConfirmationCanvas.transform.position.y + 125.0f));

        ItemNameText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x, (ShopConfirmationCanvas.transform.position.y + 100.0f));

        ItemNameStats = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        ItemNameStats.GetComponent<RectTransform>().sizeDelta = new Vector2(700.0f, 30.0f);
        ItemNameStats.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x, (ShopConfirmationCanvas.transform.position.y + 50.0f));
        
        CostText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        CostText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 100.0f, (ShopConfirmationCanvas.transform.position.y + 25.0f));
        GoldText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        GoldText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 100.0f, (ShopConfirmationCanvas.transform.position.y + 25.0f));

        BuyButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        BuyButton.GetComponentInChildren<Text>().text = "Buy";
        BuyButton.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 50.0f, (ShopConfirmationCanvas.transform.position.y - 100.0f));
        BuyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        BuyButton.GetComponent<Button>().onClick.AddListener(BuyItem);

        CancelButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelBuy);
        CancelButton.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 50.0f, (ShopConfirmationCanvas.transform.position.y - 100.0f));

        QuantityText = Instantiate(TextPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantityText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 50.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        Quantity = 1;

        QuantityAdd = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantityAdd.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Plus;
        QuantityAdd.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 50.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantityAdd.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantityAdd.GetComponent<Button>().onClick.AddListener(AddQuantity);


        QuantitySubtract = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantitySubtract.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Minus;
        QuantitySubtract.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 125.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantitySubtract.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantitySubtract.GetComponent<Button>().onClick.AddListener(SubtractQuantity);
    }

    // Update is called once per frame
    void Update()
    {
        ShopConfirmationCanvas.SetActive(ConfirmationDisplay);
        if (ConfirmationDisplay)
        {
            CostText.GetComponent<Text>().text = "Item cost: " + (SelectedItem.ItemCost * Quantity).ToString();
            QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        }
    }

    void ShopButtonOnClick(GameObject btn)
    {
        SelectedItem = null;
        
        for (int i = 0; i < ShopLayout.Length; ++i)
        {
            if (btn.GetComponent<Image>().sprite.name != ShopLayout[i].GetComponent<Image>().sprite.name)
                continue;

            if (btn.name == ShopLayout[i].name)
                SelectedItem = ShopItems[i];

            if (SelectedItem != null)
            {
                ConfirmationDisplay = true;
                DisplayConfirmedItem();
            }
        }
    }

    void ResetDisplay()
    {
        foreach (GameObject go in ShopLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
        for (int i = 0; i < ShopItems.Length;++i)
        {
            ShopItems[i] = null;
        }
        foreach (GameObject go in ShopBorders)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
        }
    }
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
                    if (ShopLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && ShopItems[i].ItemRarity == item.ItemRarity && ShopItems[i].Name == item.Name)
                        break;
                    else if (ShopLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                        continue;
                    else
                    {
                        if (item.ItemRarity == "Common")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                        else if (item.ItemRarity == "Uncommon")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                        else if (item.ItemRarity == "Magic")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                        else if (item.ItemRarity == "Ancient")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                        else if (item.ItemRarity == "Relic")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                        ShopItems[i] = item;
                        ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                        break;
                    }
                }
            }
        }
    }
    public void DisplaySearchMenu(InputField itemname)
    {
        ResetDisplay();
        
        foreach (Item item in ItemDatabase.Instance.ItemList)
        {
            if (!item.Name.Contains(itemname.text) && !item.Name.ToLower().Contains(itemname.text) && !item.Name.ToUpper().Contains(itemname.text))
                continue;

            for (int i = 0; i < ShopLayout.Length; ++i)
            {
                if (ShopLayout[i].GetComponent<Image>().sprite.name == item.ItemImage.name && ShopItems[i].ItemRarity == item.ItemRarity && ShopItems[i].Name == item.Name)
                    break;
                else if (ShopLayout[i].GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    if (item.ItemRarity == "Common")
                        ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                    else if (item.ItemRarity == "Uncommon")
                        ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                    else if (item.ItemRarity == "Magic")
                        ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                    else if (item.ItemRarity == "Ancient")
                        ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                    else if (item.ItemRarity == "Relic")
                        ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;


                    ShopItems[i] = item;
                    ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                    break;
                }

            }
        }
    }
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
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderCommon;
                        else if (item.ItemRarity == "Uncommon")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderUncommon;
                        else if (item.ItemRarity == "Magic")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderMagic;
                        else if (item.ItemRarity == "Ancient")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderAncient;
                        else if (item.ItemRarity == "Relic")
                            ShopBorders[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderRelic;

                        ShopItems[i] = item;
                        ShopLayout[i].name = item.Name + " " + item.ItemRarity;
                        ShopLayout[i].GetComponent<Image>().sprite = item.ItemImage;
                        break;
                    }
                }
            }
            
        }
    }
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


        ItemNameStats.GetComponent<Text>().text =   "Level: " + SelectedItem.Level + "   " +
                                                    "Health: " + SelectedItem.Health + "   " +
                                                    "Stamina: " + SelectedItem.Stamina + "   " +
                                                    "Attack: " + SelectedItem.Attack + "   " +
                                                    "Defense: " + SelectedItem.Defense + "   " +
                                                    "Move Speed: " + SelectedItem.MoveSpeed;
        //GoldText.GetComponent<Text>().text = "Your Gold: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gold.ToString();
        //TODO

    }

    void BuyItem()
    {
        for (int i = 1; i <= Quantity; ++i)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddItem(SelectedItem);
        }
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().AddGold(-SelectedItem.ItemCost * Quantity);
        //TODO

        ConfirmationDisplay = false;
        Quantity = 1;
    }
    void CancelBuy()
    {
        ConfirmationDisplay = false;
        Quantity = 1;
    }

    void AddQuantity()
    {
        //if ((SelectedItem.ItemCost * (Quantity + 1)) <= GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gold)
        //    Quantity++;
        //TODO
    }
    void SubtractQuantity()
    {
        if (Quantity - 1 <= 1)
            Quantity = 1;
        else
            Quantity--;
    }

    public void ViewPage1()
    {
        if (PageCount - 1 >= 0)
            PageCount--;

            StartCount = PageCount * NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipment();
        else
            DisplayShopMenu(currenttag);
        gameObject.GetComponent<Shop>().Page1Button.GetComponent<Image>().color = Color.red;
        gameObject.GetComponent<Shop>().Page2Button.GetComponent<Image>().color = Color.cyan;
    }
    public void ViewPage2()
    {
        if (PageCount + 1 < MaxCount)
            PageCount++;

        StartCount = PageCount * NumberOfItemsPerRow * MaxNumberOfColumn + 1;
        if (currenttag == "all")
            DisplayAllEquipment();
        else
            DisplayShopMenu(currenttag);

        gameObject.GetComponent<Shop>().Page1Button.GetComponent<Image>().color = Color.cyan;
        gameObject.GetComponent<Shop>().Page2Button.GetComponent<Image>().color = Color.red;
    }

    public void setConfirmationDisplay(bool _display) { ConfirmationDisplay = _display; }
    public int getPageCount() { return PageCount; }
    public int getMaxCount() { return MaxCount; }
}