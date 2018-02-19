using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour {

    [SerializeField]
    GameObject Bar;
    [SerializeField]
    GameObject ItemLogoPrefab;

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
