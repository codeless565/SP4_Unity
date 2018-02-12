using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPpotion : MonoBehaviour, ItemBase
{
    Image ItemImage;

    public int getCost()
    {
        return 500;
    }

    public Image getItemImage()
    {
        return ItemImage;
    }

    public string getName()
    {
        return "MPpotion";
    }

    public string getType()
    {
        return "Uses";
    }

    public bool Owned()
    {
        Debug.Log("MPpotion Owned not done");
        return true;
    }
}