using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject EquipmentDropdown;
    public GameObject UsesButton;
    public GameObject InventoryUICanvas;

    bool InventoryUI;
    bool WeaponsDisplay;
    bool UsesDisplay;

    Color ButtonActiveColour = Color.red;
    Color ButtonInactiveColour = Color.cyan;

    // Use this for initialization
    void Start()
    {
        WeaponsDisplay = true;
        UsesDisplay = false;
        InventoryUI = false;

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

        InventoryUICanvas.SetActive(InventoryUI);
    }

    public void OpenEquipment()
    {
        switch (EquipmentDropdown.GetComponent<Dropdown>().value)
        {
            case 1:
                gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Weapons");
                break;
            case 2:
                gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Helmets");
                break;
            case 3:
                gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Chestpieces");
                break;
            case 4:
                gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Leggings");
                break;
            case 5:
                gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Shoes");
                break;
            default:
                gameObject.GetComponent<InventoryDisplay>().DisplayAllEquipments();
                break;
        }
        WeaponsDisplay = true;
        UsesDisplay = false;
    }

    public void OpenUses()
    {
        gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Uses");
        WeaponsDisplay = false;
        UsesDisplay = true;
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
        OpenEquipment();
        InventoryUI = true;
    }
}