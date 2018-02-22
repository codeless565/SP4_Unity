using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public Player2D_Manager player;

    //Activate text box
    public bool isActive;
    public bool stopPlayerMovement;

    //Typewriter
    public bool isTyping = false;
    public bool cancelTyping;
    public float typeSpeed;

    // Use this for initialization
    void Start()
    {

        player = FindObjectOfType<Player2D_Manager>();
        isActive = true;
        typeSpeed = 0.01f;

        if (textfile != null)
        {
            //getting end of each line of text and store them into textLines array
            textLines = (textfile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;

        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //show text
        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    //show text letter by letter
                    StartCoroutine(TypeText(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping) //interrupts typing
            {
                cancelTyping = true;
            }
        }

    }

    public IEnumerator TypeText(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && letter < lineOfText.Length - 1)
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        //when cancelled, print everything
        theText.text = lineOfText;

        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        //Player can't move when Textbox is active
        if (stopPlayerMovement)
        {
            player.canMove = false;
        }

        //show text letter by letter
        StartCoroutine(TypeText(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }
}
