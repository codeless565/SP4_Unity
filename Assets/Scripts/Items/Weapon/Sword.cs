using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour , ItemBase
{
    Image ItemImage;

    public int getCost()
    {
        return 1000;
    }

    public Image getItemImage()
    {
        return ItemImage;
    }

    public string getName()
    {
        return "Sword";
    }

    public string getType()
    {
        return "Weapons";
    }

    public bool Owned()
    {
        Debug.Log("Sword Owned not done");
        return true;
    }
}
