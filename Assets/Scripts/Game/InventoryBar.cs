using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour {

    [SerializeField]
    GameObject Bar;
    [SerializeField]
    GameObject ItemLogoPrefab;

    float PanelMargin = 35.0f;
    float ButtonMarginX = 30.0f;
    float ButtonMarginY = 30.0f;

    int maxNumOfX = 6;

    GameObject[] HotBar;
    // Use this for initialization
    void Start () {
        HotBar = new GameObject[maxNumOfX];
        int currentX = 0;


        for (int i =0;i<HotBar.Length;++i)
        {
            GameObject newIcon = Instantiate(ItemLogoPrefab, Bar.transform) as GameObject;
            newIcon.GetComponentInChildren<Text>().text = (currentX + 1).ToString();
            newIcon.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;


            newIcon.transform.position = new Vector3(PanelMargin + Bar.GetComponent<RectTransform>().rect.xMin + currentX * (newIcon.GetComponent<Image>().rectTransform.rect.width + ButtonMarginX),
                                                   0) + Bar.transform.position;

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
