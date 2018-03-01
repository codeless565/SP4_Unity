using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsProperties
{
    public string PropertyName;
    public string PropertyDetails;
    public float Counter;
    public float CompletionCounter;
    public bool PropertyComplete;

    // Use this for initialization
    public AchievementsProperties()
    {
        PropertyName = "";
        PropertyDetails = "";
        Counter = 0.0f;
        CompletionCounter = 0.0f;
        PropertyComplete = false;
    }

    public AchievementsProperties(string _name, string _details, float _completioncounter)
    {
        PropertyName = _name;
        PropertyDetails = _details;
        Counter = 0;
        CompletionCounter = _completioncounter;
        PropertyComplete = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Counter >= CompletionCounter)
            PropertyComplete = true;
    }

    public void AddCounter(int value)
    {
        Counter += value;
    }
}
