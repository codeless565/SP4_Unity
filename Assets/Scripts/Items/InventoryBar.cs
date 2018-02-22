﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour
{
    private GameObject Bar;  
    public GameObject ItemLogoPrefab;
    public GameObject ItemHotkey;


    int maxNumOfX = 6;

    GameObject[] HotBar;
    // Use this for initialization
    void Start () {
        HotBar = new GameObject[maxNumOfX];
        int currentX = 0;
        Bar = Instantiate(GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().InventoryBar,
            GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().HUD.transform);

        for (int i =0;i<HotBar.Length;++i)
        {
            GameObject newIcon = Instantiate(ItemLogoPrefab, Bar.transform) as GameObject;
            currentX++;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            GameObject HotKeyText = Instantiate(ItemHotkey, newIcon.transform);
            HotKeyText.transform.position = new Vector3(60.0f, 30.0f) + newIcon.transform.position;
            HotKeyText.GetComponentInChildren<Text>().text = currentX.ToString();
            HotKeyText.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;
#endif
            HotBar[i] = newIcon;
        }

    }

    // Update is called once per frame
    void Update () {
    }

    public void AddPlayerHotBar(Item item)
    {
        for (int i = 0; i < HotBar.Length; ++i)
        {
            if (HotBar[i].GetComponent<Image>().sprite.name == item.ItemImage.name)
                break;

            if (HotBar[i].GetComponent<Image>().sprite.name == "UISprite")
            {
                HotBar[i].GetComponentInChildren<Text>().text = item.Quantity.ToString();
                HotBar[i].GetComponentInChildren<Text>().alignment = TextAnchor.LowerRight;
                HotBar[i].GetComponent<Image>().sprite = item.ItemImage;
                break;
            }
        }
    }
}
