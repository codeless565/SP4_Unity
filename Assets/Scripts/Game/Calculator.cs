using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator  {

    private static Calculator instance;
    private Calculator()
    {
    }

    public static Calculator Instance
    {
        get
        {
            if (instance == null)
                instance = new Calculator();
            return instance;
        }
    }

    public float CalculateDamage(float _myAtk, float _otherDef)
    {
        return _myAtk - _myAtk / _otherDef;
    }
}
