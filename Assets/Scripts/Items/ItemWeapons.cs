using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ItemWeapons : ItemBase
{
    bool isEquipped
    {
        get;
        set;
    }
}
