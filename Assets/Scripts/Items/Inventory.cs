using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUICanvas;

    public GameObject EquipmentDropdown;
    public GameObject UsesButton;
    public GameObject NextPageButton;
    public GameObject PreviousPageButton;
    public InputField SearchBar;
    public GameObject InventoryPage;

    bool InventoryUI;
    bool EquipmentDisplay;
    bool UsesDisplay;

    Color ButtonActiveColour = Color.red;
    Color ButtonInactiveColour = Color.cyan;

    private GameObject Player;
    // Use this for initialization
    public void Init()
    {
        EquipmentDisplay = true;
        UsesDisplay = false;
        InventoryUI = false;

        EquipmentDropdown.GetComponent<Image>().color = ButtonActiveColour;
        UsesButton.GetComponent<Image>().color = ButtonInactiveColour;
        SearchBar.onEndEdit.AddListener(delegate { gameObject.GetComponent<InventoryDisplay>().DisplaySearchMenu(SearchBar); });
        PreviousPageButton.GetComponent<Image>().color = Color.red;
        NextPageButton.GetComponent<Image>().color = Color.cyan;

        Player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(Player);
    }

    // Update is called once per frame
    void Update()
    {
        if (EquipmentDisplay)
            EquipmentDropdown.GetComponent<Image>().color = ButtonActiveColour;
        else
            EquipmentDropdown.GetComponent<Image>().color = ButtonInactiveColour;

        if (UsesDisplay)
            UsesButton.GetComponent<Image>().color = ButtonActiveColour;
        else
            UsesButton.GetComponent<Image>().color = ButtonInactiveColour;

        InventoryUICanvas.SetActive(InventoryUI);
        
        InventoryPage.GetComponent<Text>().text = "Page: " + (GetComponent<InventoryDisplay>().getPageCount() + 1) + "/" + GetComponent<InventoryDisplay>().getMaxCount();
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
        EquipmentDisplay = true;
        UsesDisplay = false;
    }

    public void OpenUses()
    {
        gameObject.GetComponent<InventoryDisplay>().DisplayInventoryMenu("Uses");
        EquipmentDisplay = false;
        UsesDisplay = true;
    }
    public void CloseInventoryUI()
    {
        gameObject.GetComponent<InventoryDisplay>().InventoryDisplayCanvas.SetActive(false);
        gameObject.GetComponent<InventoryDisplay>().setConfirmationDisplay(false);
        InventoryUI = false;
        Player.GetComponent<Player2D_Manager>().canMove = true;
    }
    public void OpenInventoryUI()
    {
        gameObject.GetComponent<InventoryDisplay>().InventoryDisplayCanvas.SetActive(true);
        OpenEquipment();
        InventoryUI = true;
        Player.GetComponent<Player2D_Manager>().canMove = false;

    }
}