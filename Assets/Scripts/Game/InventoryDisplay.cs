using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
    public GameObject InventoryDisplayCanvas;
    public GameObject ItemButton;
    public GameObject ItemText;
    public int NumberOfItemsPerRow = 5;
    public int MaxNumberOfColumn = 3;

    // Confirmation Canvas
    public GameObject EquipConfirmationCanvas;
    bool ConfirmationCanvas;
    GameObject ItemNameText;
    GameObject ConfirmButton;
    GameObject CancelButton;

    

    GameObject[] InventoryLayout;
    ItemBase SelectedItem;

    // Use this for initialization
    void Start () {
        InventoryLayout = new GameObject[NumberOfItemsPerRow * MaxNumberOfColumn];
        InventoryDisplayCanvas.SetActive(false);

        // Shop Menu UI
        for (int i = 0; i < InventoryLayout.Length; ++i)
        {
            GameObject newIcon = Instantiate(ItemButton, InventoryDisplayCanvas.transform) as GameObject;

            InventoryLayout[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { Clicking(newIcon); });
        }
        ItemNameText = Instantiate(ItemText, EquipConfirmationCanvas.transform) as GameObject;
        ConfirmationCanvas = false;
        ConfirmButton = Instantiate(ItemButton, EquipConfirmationCanvas.transform) as GameObject;
        ConfirmButton.GetComponentInChildren<Text>().text = "Confirm";
        ConfirmButton.transform.position = new Vector3(-50.0f,  - 100.0f) + EquipConfirmationCanvas.transform.position;
        ConfirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(ConfirmEquip);

        CancelButton = Instantiate(ItemButton, EquipConfirmationCanvas.transform) as GameObject;
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";
        CancelButton.GetComponent<Button>().onClick.AddListener(CancelEquip);
        CancelButton.transform.position = new Vector3(50.0f,- 100.0f) + EquipConfirmationCanvas.transform.position;

    }

    // Update is called once per frame
    void Update () {
        EquipConfirmationCanvas.SetActive(ConfirmationCanvas);
	}

    void ResetDisplay()
    {
        foreach (GameObject go in InventoryLayout)
        {
            go.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("MiscellaneousHolder").GetComponent<MiscellaneousHolder>().Empty;
        }
    }
    public void DisplayInventoryMenu(string itemtype)
    {
        ResetDisplay();
        foreach (ItemBase item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (item.getType() != itemtype)
                continue;

            foreach (GameObject go in InventoryLayout)
            {
                if (go.GetComponent<Image>().sprite.name == item.getItemImage().name)
                    break;
                else if (go.GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    go.GetComponent<Image>().sprite = item.getItemImage();
                    break;
                }

            }
        }
    }
    public void DisplayAllEquipments()
    {
        ResetDisplay();
        foreach (ItemBase item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().getPlayerInventory())
        {
            if (item.getType() == "Uses")
                continue;

            foreach (GameObject go in InventoryLayout)
            {
                if (go.GetComponent<Image>().sprite.name == item.getItemImage().name)
                    break;
                else if (go.GetComponent<Image>().sprite.name != "UISprite")
                    continue;
                else
                {
                    go.GetComponent<Image>().sprite = item.getItemImage();
                    break;
                }

            }
        }

    }

    public void setConfirmationDisplay(bool _display) { ConfirmationCanvas = _display; }
    public GameObject getItemDisplayCanvas() { return InventoryDisplayCanvas; }

    void Clicking(GameObject btn)
    {
        SelectedItem = null;
        foreach (GameObject buttons in InventoryLayout)
        {
            if (btn.GetComponent<Image>().sprite.name != buttons.GetComponent<Image>().sprite.name)
                continue;

            SelectedItem = ItemManager.Instance.CheckGO(btn);
            if (SelectedItem != null)
            {
                ConfirmationCanvas = true;
                DisplayConfirmedItem();
            }
        }
    }

    void CancelEquip()
    {
        ConfirmationCanvas = false;
    }
    void ConfirmEquip()
    {
        if (SelectedItem == null)
            return;

        if (SelectedItem.getType() == "Uses")
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().GetComponent<InventoryBar>().AddPlayerHotBar(SelectedItem);
        else
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().EquipEQ(SelectedItem);
        ConfirmationCanvas = false;
    }

    void DisplayConfirmedItem()
    {
        ItemNameText.GetComponent<Text>().text = "Confirm Equip " + SelectedItem.Name + " ? ";
    }

}
