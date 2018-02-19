using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject WeaponsButton;
    public GameObject UsesButton;
    public GameObject InventoryUICanvas;

    bool InventoryUI;
    bool WeaponsDisplay;
    bool UsesDisplay;

    public Color ButtonActiveColour = Color.red;
    public Color ButtonInactiveColour = Color.cyan;

    // Use this for initialization
    void Start()
    {
        WeaponsDisplay = true;
        UsesDisplay = false;
        InventoryUI = false;

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

        InventoryUICanvas.SetActive(InventoryUI);
    }

    public void OpenWeapons()
    {
        gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Weapons");
        WeaponsDisplay = true;
        UsesDisplay = false;
    }
    public void OpenUses()
    {
        gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Uses");
        UsesDisplay = true;
        WeaponsDisplay = false;
    }
    public void CloseInventoryUI()
    {
        gameObject.GetComponent<InventoryDisplay>().getItemDisplayCanvas().SetActive(false);
        gameObject.GetComponent<InventoryDisplay>().setConfirmationDisplay(false);

        InventoryUI = false;
    }
    public void OpenInventoryUI()
    {
        gameObject.GetComponent<InventoryDisplay>().getItemDisplayCanvas().SetActive(true);
        OpenWeapons();
        InventoryUI = true;
    }

}