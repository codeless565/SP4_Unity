using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ItemBase
{
    Image getItemImage();

    string getName();
    string getType();
    
    int getCost();

    bool Owned();
}

//  Item Types
//  Uses
//  Weapons