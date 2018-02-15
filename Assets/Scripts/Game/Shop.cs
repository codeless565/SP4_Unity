using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [SerializeField]
    GameObject ShopCanvas;

    [SerializeField]
    GameObject ShopConfirmationCanvas;

    [SerializeField]
    GameObject ButtonPrefab;

    [SerializeField]
    GameObject ConfirmationText;

    [SerializeField]
    int NumberOfItemsPerRow=3;

    [SerializeField]
    int MaxNumberOfColumn = 10;

    GameObject[] ShopLayout;

    float ButtonMarginX = 30.0f;
    float ButtonMarginY = 30.0f;

    bool ShopDisplay;

    GameObject ItemNameText;
    GameObject CostText;
    GameObject GoldText;
    GameObject BuyButton;
    GameObject CancelButton;
    bool ConfirmationDisplay;

    GameObject QuantityText;
    int Quantity;
    GameObject QuantityAdd;
    GameObject QuantitySubtract;

    ItemBase SelectedItem;
    // Use this for initialization
    void Start () {
        ShopDisplay = false;
        ConfirmationDisplay = false;
        ShopLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];

        // Shop Menu UI
        float currentX = 0.0f;
        float currentY = 0.0f;
        for (int i = 0; i < ShopLayout.Length; ++i)
        {
            if (currentX >= NumberOfItemsPerRow)
            {
                currentX = 0.0f;
                currentY++;
            }
            GameObject newIcon = Instantiate(ButtonPrefab, ShopCanvas.transform) as GameObject;
            newIcon.GetComponentInChildren<Text>().text = "IMAGE";


            newIcon.transform.position = new Vector3((ShopCanvas.transform.position.x - ShopCanvas.GetComponent<RectTransform>().rect.width * 0.25f) + currentX * (newIcon.GetComponent<Image>().rectTransform.rect.width + ButtonMarginX),
                                                    ShopCanvas.transform.position.y + currentY * (newIcon.GetComponent<Image>().rectTransform.rect.height + ButtonMarginY));

            currentX++;
            ShopLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { ButtonOnClick(newIcon); });
            newIcon.SetActive(ShopDisplay);
        }


        // Confirmation Menu UI
        ItemNameText = Instantiate(ConfirmationText, ShopConfirmationCanvas.transform) as GameObject;
        CostText = Instantiate(ConfirmationText, ShopConfirmationCanvas.transform) as GameObject;
        CostText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 100.0f, (ShopConfirmationCanvas.transform.position.y + 25.0f));
        GoldText = Instantiate(ConfirmationText, ShopConfirmationCanvas.transform) as GameObject;
        GoldText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 100.0f, (ShopConfirmationCanvas.transform.position.y + 25.0f));

        BuyButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        BuyButton.GetComponentInChildren<Text>().text = "Buy";
        BuyButton.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 50.0f, (ShopConfirmationCanvas.transform.position.y - 50.0f));

        CancelButton = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelBuy);
        CancelButton.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 50.0f, (ShopConfirmationCanvas.transform.position.y - 50.0f));

        QuantityText = Instantiate(ConfirmationText, ShopConfirmationCanvas.transform) as GameObject;
        QuantityText.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x - 50.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        Quantity = 1;

        QuantityAdd = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantityAdd.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("MiscellaneousHolder").GetComponent<MiscellaneousHolder>().Plus;
        QuantityAdd.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 50.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantityAdd.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantityAdd.GetComponent<Button>().onClick.AddListener(AddQuantity);


        QuantitySubtract = Instantiate(ButtonPrefab, ShopConfirmationCanvas.transform) as GameObject;
        QuantitySubtract.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("MiscellaneousHolder").GetComponent<MiscellaneousHolder>().Minus;
        QuantitySubtract.transform.position = new Vector3(ShopConfirmationCanvas.transform.position.x + 125.0f, (ShopConfirmationCanvas.transform.position.y - 25.0f));
        QuantitySubtract.GetComponent<Button>().onClick.RemoveAllListeners();
        QuantitySubtract.GetComponent<Button>().onClick.AddListener(SubtractQuantity);
        //GameObject QuantityAdd;
        //GameObject QuantitySubtract;
    }

    // Update is called once per frame
    void Update () {      
        if (Input.GetKey(KeyCode.J))
            ShopDisplay = !ShopDisplay;
        if (Input.GetKey(KeyCode.K))
            ConfirmationDisplay = !ConfirmationDisplay;

        DisplayShopMenu();

        ShopConfirmationCanvas.SetActive(ConfirmationDisplay);
        if (ConfirmationDisplay)
        {
            CostText.GetComponent<Text>().text = "Item cost: " + (SelectedItem.getCost() * Quantity).ToString();
            QuantityText.GetComponentInChildren<Text>().text = "Quantity: " + Quantity;
        }

    }

    void ButtonOnClick(GameObject btn)
    {
        SelectedItem = null;
        foreach (GameObject buttons in ShopLayout)
        {
            if (btn.GetComponent<Image>().sprite.name != buttons.GetComponent<Image>().sprite.name)
                continue;

            SelectedItem = ItemManager.Instance.CheckGO(btn);
            if (SelectedItem != null)
            {
                ConfirmationDisplay = true;
                DisplayConfirmedItem();
            }                
        }
    }

    void DisplayShopMenu()
    {
        foreach(ItemBase item in ItemManager.Instance.ItemList)
        {
            foreach (GameObject go in ShopLayout)
            {
                if (go.GetComponent<Image>().sprite.name == item.getItemImage().name)
                    break;

                if (go.GetComponent<Image>().sprite.name == "UISprite")
                {
                    go.GetComponent<Image>().sprite = item.getItemImage();
                    break;
                }

            }
        }

        foreach (GameObject go in ShopLayout)
        {
            go.SetActive(ShopDisplay);
        }
    }

    void DisplayConfirmedItem()
    {
        ItemNameText.GetComponent<Text>().text = SelectedItem.Name;
        GoldText.GetComponent<Text>().text = "Your Gold: " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().gold.ToString();
        BuyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        BuyButton.GetComponent<Button>().onClick.AddListener(BuyItem);
    }

    void BuyItem()
    {
        for (int i =1;i<=Quantity;++i)
        {
            Debug.Log(i);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AddItem(SelectedItem);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AddGold(-SelectedItem.getCost()*Quantity);

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
        if ((SelectedItem.getCost() * (Quantity + 1)) <= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().gold)
            Quantity++;
    }

    void SubtractQuantity()
    {
        if (Quantity - 1 <= 1)
            Quantity = 1;
        else
            Quantity--;           
    }
}