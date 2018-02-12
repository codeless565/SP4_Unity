using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPpotion : MonoBehaviour, ItemBase
{
    Image ItemImage;

    public Image getItemImage()
    {
        return ItemImage;
    }

    public string getName()
    {
        return "HPpotion";
    }

    public string getType()
    {
        return "Uses";
    }

    public int getCost()
    {
        return 500;
    }

    public bool Owned()
    {
        Debug.Log("HPpotion Owned not done");
        return true;
    }
}