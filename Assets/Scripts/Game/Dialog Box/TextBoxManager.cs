﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Handles TextBoxes ( eg. Dialouges ) */
public class TextBoxManager : MonoBehaviour
{
    /* Text Box to render Text */
    public GameObject textBox;
    /* The Text to Load */
    public Text theText;

    /* File to read from */
    public TextAsset textfile;
    public string[] textLines;

    /* Limit what lines to show */
    public int currentLine;
    public int endAtLine;

    /* To move player or not */
    public Player2D_Manager player;

    // Activate text box
    public bool isActive;
    private bool stopPlayerMovement;

    // Typewriter
    public bool isTyping = false;
    public bool cancelTyping;
    private float typeSpeed;

    /* Bool to Trigger when finish typing */
    public bool _canShow;

    // Use this for initialization
    void Start()
    {
        /* Find Player */
        player = FindObjectOfType<Player2D_Manager>();
        typeSpeed = 0.01f;

        /* Getting end of each line of text and store them into textLines array */
        if (textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }

        /* To set as the last line in the text file */
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        /* Rendering Text Box */
        isActive = false;

        /* Can Show Things */
        _canShow = false;
    }

    void Update()
    {
        if (!isActive)
            return;

        /* Checking for Active of Text Box */
        if (isActive)
            EnableTextBox();
        else
            DisableTextBox();

        /* Go to next Line */
        if (Input.GetKeyDown(KeyCode.Return) /* Or tap the screen */)
        {
            if (!isTyping)
            {
                ++currentLine;

                /* When more then the line to generate, close Dialouge */
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

    /* Letter by Letter */
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

            /* Finish Typing, set true to render Buttons */
            _canShow = true;
        }
        //when cancelled, print everything
        theText.text = lineOfText;

        isTyping = false;
        cancelTyping = false;
    }

    /* Render Text box */
    public void EnableTextBox()
    {
        textBox.SetActive(true);

        //Player can't move when Textbox is active
        if (stopPlayerMovement)
            player.canMove = false;

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
