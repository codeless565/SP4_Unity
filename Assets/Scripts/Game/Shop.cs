using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject WeaponsButton;
    public GameObject UsesButton;
    public GameObject ShopUICanvas;

    bool ShopUI;
    bool WeaponsDisplay;
    bool UsesDisplay;

    public Color ButtonActiveColour = Color.red;
    public Color ButtonInactiveColour = Color.cyan;

    // Use this for initialization
    void Start()
    {
        WeaponsDisplay = true;
        UsesDisplay = false;
        ShopUI = false;

        WeaponsButton.GetComponent<Image>().color = ButtonActiveColour;
        UsesButton.GetComponent<Image>().color = ButtonInactiveColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponsDisplay)
            WeaponsButton.GetComponent<Image>().color = ButtonActiveColour;
        else
            WeaponsButton.GetComponent<Image>().color = ButtonInactiveColour;

        if (UsesDisplay)
            UsesButton.GetComponent<Image>().color = ButtonActiveColour;
        else
            UsesButton.GetComponent<Image>().color = ButtonInactiveColour;

        ShopUICanvas.SetActive(ShopUI);
    }

    public void OpenWeapons()
    {
        gameObject.GetComponent<ShopDisplay>().DisplayShopMenu("Weapons");
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
        OpenWeapons();
        ShopUI = true;
    }

}