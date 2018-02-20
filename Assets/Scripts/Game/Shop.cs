using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject EquipmentDropdown;
    public GameObject UsesButton;
    public GameObject ShopUICanvas;
    public GameObject GoldText;

    bool ShopUI;
    bool WeaponsDisplay;
    bool UsesDisplay;

    Color ButtonActiveColour = Color.red;
    Color ButtonInactiveColour = Color.cyan;

    // Use this for initialization
    void Start()
    {
        WeaponsDisplay = true;
        UsesDisplay = false;
        ShopUI = false;

        EquipmentDropdown.GetComponent<Image>().color = ButtonActiveColour;
        UsesButton.GetComponent<Image>().color = ButtonInactiveColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponsDisplay)
            EquipmentDropdown.GetComponent<Image>().color = ButtonActiveColour;
        else
            EquipmentDropdown.GetComponent<Image>().color = ButtonInactiveColour;

        if (UsesDisplay)
            UsesButton.GetComponent<Image>().color = ButtonActiveColour;
        else
            UsesButton.GetComponent<Image>().color = ButtonInactiveColour;

        ShopUICanvas.SetActive(ShopUI);
        GoldText.GetComponent<Text>().text = "Gold: " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().gold.ToString();
    }

    public void OpenEquipment()
    {
        
        switch (EquipmentDropdown.GetComponent<Dropdown>().value)
        {
            case 1:
                gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Weapons");
                break;
            case 2:
                gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Helmets");
                break;
            case 3:
                gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Chestpieces");
                break;
            case 4:
                gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Leggings");
                break;
            case 5:
                gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Shoes");
                break;
            default:
                gameObject.GetComponent<ShopDisplay>().DisplayAllEquipment();
                break;
        }
        WeaponsDisplay = true;
        UsesDisplay = false;
    }

    public void OpenUses()
    {
        gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Uses");
        UsesDisplay = true;
        WeaponsDisplay = false;
    }
    public void CloseShopUI()
    {
        gameObject.GetComponent<ShopDisplay>().getItemDisplayCanvas().SetActive(false);
        gameObject.GetComponent<ShopDisplay>().setConfirmationDisplay(false);
        ShopUI = false;
    }
    public void OpenShopUI()
    {
        gameObject.GetComponent<ShopDisplay>().getItemDisplayCanvas().SetActive(true);
        OpenEquipment();
        ShopUI = true;
    }

}