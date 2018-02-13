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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Display()
    {
        int currentX = 0;
        int currentY = 0;

        foreach(ItemWeapons weapon in ItemManager.Instance.WeaponList)
        {
            if (currentX >= NumberOfItemsPerRow)
            {
                currentX = 0;
                currentY++;
            }
            GameObject newBtn = Instantiate(ButtonPrefab) as GameObject;
            newBtn.transform.SetParent(ShopCanvas.transform);
            newBtn.GetComponentInChildren<Text>().text = weapon.Name;
            // Sprite for button
            //Image ItemImage=GameObject.Find("Sword01").GetComponent<Image>(); -- in Sword.cs
            // transform button to canvas and equal spacing       
            newBtn.transform.position = new Vector3(currentX * 150, currentY * 150);
            //newBtn.transform.position = new Vector3(currentX * newBtn.GetComponentInChildren<RectTransform>().localScale.x, currentY * newBtn.GetComponentInChildren<RectTransform>().localScale.y);
        }
    }
}
