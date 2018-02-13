using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBoxManager : MonoBehaviour{

    public GameObject textBox;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerManager player;

    //Activate text box
    public bool isActive;
    public bool stopPlayerMovement;

    //Typewriter
    private bool isTyping = false;
    private bool cancelTyping;
    public float typeSpeed;

    //For tutorial
    bool tut;
    private bool movedW, movedA, movedS, movedD;

    // Use this for initialization
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "SceneTutorial")
        {
            tut = true;
            movedW = false;
            movedA = false;
            movedS = false;
            movedD = false;
        }

        player = FindObjectOfType<PlayerManager>();
        isActive = true;
        typeSpeed = 0.01f;

        if (textfile != null)
        {
            //getting end of each line of text and store them into textLines array
            textLines = (textfile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;

        }

        if(isActive)
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
        if(!isActive)
        {
            return;
        }

        //show text
        //theText.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    if(tut)
                    {
                        TutorialUpdate();
                    }
                    else
                    //show text letter by letter
                    StartCoroutine(TypeText(textLines[currentLine]));
                }
            }
            else if(isTyping && !cancelTyping) //interrupts typing
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TypeText(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while(isTyping && !cancelTyping && letter < lineOfText.Length - 1)
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
        if(stopPlayerMovement)
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

    void TutorialUpdate()
    {
        switch(currentLine)
        {
            case 2: //after movement message
                DisableTextBox();
                if (Input.GetKeyDown(KeyCode.W))
                {
                    movedW = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    movedA = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    movedS = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    movedD = true;
                }

                if (movedW && movedS && movedD && movedA) //tried movement
                {
                    currentLine = 2; //give instructions about attack
                    EnableTextBox();
                }
                break;

            case 3: //attack enemy
                DisableTextBox();
                if(Input.GetMouseButton(0)) //tried weapon //will change trigger to when enemy killed when enemy spawning is available
                {
                    currentLine = 3; //give instructions about changing weapon
                    EnableTextBox();
                }
                break;

            case 4: //change weapon
                if(Input.GetKeyDown(KeyCode.C))
                {
                    currentLine = 4; //give instructions about interactions
                    EnableTextBox();
                }
                break;

            case 5: //interact
                if(Input.GetKeyDown(KeyCode.I)) //will change to when item has been succesfully interacted
                {
                    currentLine = 5; //end message
                    EnableTextBox();
                }
                break;
        }
        //show text letter by letter
        StartCoroutine(TypeText(textLines[currentLine]));
    }
}
