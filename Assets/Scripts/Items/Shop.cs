﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject ShopUICanvas;

    public GameObject EquipmentDropdown;
    public GameObject UsesButton;
    public GameObject Page1Button;
    public GameObject Page2Button;
    public InputField SearchBar;
    public GameObject GoldText;
    public GameObject ShopPage;
    
    bool ShopUI;
    bool WeaponsDisplay;
    bool UsesDisplay;

    Color ButtonActiveColour = Color.red;
    Color ButtonInactiveColour = Color.cyan;

    private GameObject Player;
    // Use this for initialization
    public void Init()
    {
        WeaponsDisplay = true;
        UsesDisplay = false;
        ShopUI = false;

        EquipmentDropdown.GetComponent<Image>().color = ButtonActiveColour;
        UsesButton.GetComponent<Image>().color = ButtonInactiveColour;
        Page1Button.GetComponent<Image>().color = Color.red;
        Page2Button.GetComponent<Image>().color = Color.cyan;

        SearchBar.onEndEdit.AddListener(delegate { gameObject.GetComponent<ShopDisplay>().DisplaySearchMenu(SearchBar); });
        Player = GameObject.FindGameObjectWithTag("Player");
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
        GoldText.GetComponent<Text>().text = "Gold: " + Player.GetComponent<Player2D_StatsHolder>().gold.ToString();

        ShopPage.GetComponent<Text>().text = "Page: " + (GetComponent<ShopDisplay>().getPageCount()+1) + "/" + GetComponent<ShopDisplay>().getMaxCount();
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
        gameObject.GetComponent<ShopDisplay>().ShopDisplayCanvas.SetActive(false);
        gameObject.GetComponent<ShopDisplay>().setConfirmationDisplay(false);
        ShopUI = false;
    }
    public void OpenShopUI()
    {
        gameObject.GetComponent<ShopDisplay>().ShopDisplayCanvas.SetActive(true);
        OpenEquipment();
        ShopUI = true;
    }
}