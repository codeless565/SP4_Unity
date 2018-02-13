using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ItemBase : StatsBase
{
    Image getItemImage();

    string getType();
    
    int getCost();
}

//  Item Types
//  Uses
//  Weapons