using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour {

    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject ItemLogoPrefab;

    float ButtonMarginX = 30.0f;
    float ButtonMarginY = 30.0f;

    int maxNumOfX = 6;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void DisplayPlayerEQ()
    {
        int currentX = 0;
        int currentY = 0;
        foreach (ItemWeapons weapon in gameObject.GetComponent<PlayerManager>().Equipment)
        {
            if (currentX >= maxNumOfX)
                break;

            GameObject newIcon = Instantiate(ItemLogoPrefab) as GameObject;
            newIcon.transform.SetParent(Panel.transform);
            newIcon.GetComponentInChildren<Text>().text = (currentX+1).ToString();
            newIcon.GetComponentInChildren<Text>().alignment=TextAnchor.UpperLeft;
            newIcon.GetComponent<Image>().sprite = weapon.getItemImage();

            newIcon.transform.position = new Vector3(275.0f + currentX * (newIcon.GetComponent<Image>().rectTransform.rect.width + ButtonMarginX),
                                                    50.0f + currentY * (newIcon.GetComponent<Image>().rectTransform.rect.height + ButtonMarginY));

            currentX++;
            
        }
    }
}
