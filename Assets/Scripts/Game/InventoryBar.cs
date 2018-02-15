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

    GameObject[] HotBar;
    // Use this for initialization
    void Start () {
        HotBar = new GameObject[6];
        int currentX = 0;
        int currentY = 0;


        for (int i =0;i<HotBar.Length;++i)
        {
            if (currentX >= maxNumOfX)
                break;

            GameObject newIcon = Instantiate(ItemLogoPrefab, Panel.transform) as GameObject;
            newIcon.GetComponentInChildren<Text>().text = (currentX + 1).ToString();
            newIcon.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;


            newIcon.transform.position = new Vector3((Panel.transform.position.x - Panel.GetComponent<RectTransform>().rect.width * 0.25f) + currentX * (newIcon.GetComponent<Image>().rectTransform.rect.width + ButtonMarginX),
                                                    Panel.transform.position.y + currentY * (newIcon.GetComponent<Image>().rectTransform.rect.height + ButtonMarginY));

            currentX++;

            HotBar[i] = newIcon;
        }

    }

    // Update is called once per frame
    void Update () {
    }

    public void AddPlayerHotBar(ItemBase item)
    {
        // TODO Make sure that hotbar item is 'uses'
        //if (item.getType() != "Uses") 
        //    return;

        for (int i = 0; i < HotBar.Length; ++i)
        {
            if (HotBar[i].GetComponent<Image>().sprite.name == item.getItemImage().name)
                break; // TODO add quantity at btm right, not just not render more

            if (HotBar[i].GetComponent<Image>().sprite.name == "UISprite")
            {
                HotBar[i].GetComponent<Image>().sprite = item.getItemImage();
                break;
            }
        }
    }
}
