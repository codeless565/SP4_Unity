using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [SerializeField]
    GameObject ShopCanvas;

    [SerializeField]
    GameObject ButtonPrefab;

    [SerializeField]
    int NumberOfItemsPerRow=3;

    [SerializeField]
    int MaxNumberOfColumn = 10;

    float ButtonMarginX = 30.0f;
    float ButtonMarginY = 30.0f;

    List<GameObject> btnList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        DisplayEquipment();
    }

    // Update is called once per frame
    void Update () {
        
        //if (btnList.Count == 0)
        //    Debug.Log("There are no buttons");
	}

    void ButtonOnClick(GameObject btn)
    {
        foreach (GameObject buttons in btnList)
        {
            if (btn.GetComponent<Image>().sprite.name != buttons.GetComponent<Image>().sprite.name)
                continue;

            //Debug.Log(btn.GetComponent<Image>().sprite.name + " is pressed");
            // Check player gold
            
            ItemBase newitem = ItemManager.Instance.CheckGO(btn);
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().gold - newitem.getCost() >= 0)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AddItem(newitem);
            else
            {
                Debug.Log("Not enough gold");
                Debug.Log("TO BE IMPLEMENTED");
                // Dialog/Alert box
            }
                
        }
    }

    void DisplayEquipment()
    {
        int count = 0;
        float currentX = 0.0f; // Margin off left
        float  currentY = 0.0f; // Margin off bottom

        foreach (ItemBase item in ItemManager.Instance.ItemList)
        {
            if (count >= MaxNumberOfColumn)
                break;

            if (currentX >= NumberOfItemsPerRow)
            {
                currentX = 0.0f;
                currentY++;
            }
            GameObject newBtn = Instantiate(ButtonPrefab) as GameObject;
            newBtn.transform.SetParent(ShopCanvas.transform);
            newBtn.GetComponentInChildren<Text>().text = item.Name;
            newBtn.GetComponent<Image>().sprite = item.getItemImage();
            
            newBtn.transform.position = new Vector3(100.0f + currentX * (newBtn.GetComponent<Image>().rectTransform.rect.width + ButtonMarginX),
                                                    50.0f + currentY * (newBtn.GetComponent<Image>().rectTransform.rect.height + ButtonMarginY));
            
            currentX++;
            count++;
            newBtn.GetComponent<Button>().onClick.AddListener(delegate { ButtonOnClick(newBtn); });
            
            btnList.Add(newBtn);
        }
    }
}