using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour{

    public GameObject textBox;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerManager player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();

        if (textfile != null)
        {
            //getting line of text
            textLines = (textfile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;

        }
    }

    void Update()
    {
        theText.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }
    }
}
