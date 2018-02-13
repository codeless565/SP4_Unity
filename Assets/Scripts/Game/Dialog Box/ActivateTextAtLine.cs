using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for item/NPC interaction
public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;
    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;
    public bool requireKeyPress;
    private bool waitForPress;

	// Use this for initialization
	void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();

	}
	
	// Update is called once per frame
	void Update () {
        if (waitForPress && Input.GetKeyDown(KeyCode.I))
        {
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
	}

    //triggered collison with item/NPC
    void onTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            if(requireKeyPress)
            {
                waitForPress = true;
                return;
            }

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if(destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
   
    //player walks away from item/NPC
    void onTriggerExit(Collider other)
    {
        if(other.name == "player")
        {
            waitForPress = false;
        }
    }
}
