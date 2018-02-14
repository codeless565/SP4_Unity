using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextBox : MonoBehaviour
{
    TextBoxManager textboxManager;

    public TextAsset theText;
    public int startLine;
    public int endLine;

    bool MovedW, MovedA, MovedS, MovedD;
    bool triedAttack, triedInteract, triedChangeW;
    public bool pauseBox;
    // Use this for initialization
    void Start()
    {
        textboxManager = FindObjectOfType<TextBoxManager>();
        textboxManager.ReloadScript(theText);
        textboxManager.currentLine = startLine;
        textboxManager.endAtLine = endLine;
        textboxManager.EnableTextBox();

        MovedW = false;
        MovedA = false;
        MovedS = false;
        MovedD = false;
        triedAttack = false;
        triedInteract = false;
        triedChangeW = false;

        pauseBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedUpdate();

        if (Input.GetKeyDown(KeyCode.Return) && !pauseBox)
        {
            if (!textboxManager.isTyping)
            {
                textboxManager.currentLine += 1;

                if (textboxManager.currentLine > textboxManager.endAtLine)
                {
                    textboxManager.DisableTextBox();
                }
                else
                {
                    TutorialUpdate();
                }
            }
            else if (textboxManager.isTyping && !textboxManager.cancelTyping) //interrupts typing
            {
                textboxManager.cancelTyping = true;
            }
        }
    }

    void KeyPressedUpdate()
    {
        switch (textboxManager.currentLine)
        {
            case 2:
                if (!MovedA || !MovedD || !MovedS || !MovedW)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        MovedW = true;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        MovedA = true;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        MovedS = true;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        MovedD = true;
                    }
                }
                else
                {
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                    textboxManager.currentLine = 3;
                }
                break;

            case 4:
                if(!triedAttack)
                {
                    if(Input.GetMouseButton(0))
                    {
                        triedAttack = true;
                    }
                }
                else
                {
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                    textboxManager.currentLine = 5;
                }
                break;

            case 6:
                if(!triedChangeW)
                {
                    if (Input.GetKey(KeyCode.C))
                    {
                        triedChangeW = true;
                    }
                }
                else
                {
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                    textboxManager.currentLine = 7;
                }
                break;

            case 8:
                if (!triedInteract)
                {
                    if (Input.GetKey(KeyCode.I))
                    {
                        triedInteract = true;
                    }
                }
                else
                {
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                    textboxManager.currentLine = 9;
                }
                break;
        }

    }
    void TutorialUpdate()
    {
        if(textboxManager.currentLine == 2 || textboxManager.currentLine == 4 || textboxManager.currentLine == 6 || textboxManager.currentLine == 8)
        {
                pauseBox = true;
                textboxManager.DisableTextBox();
        }  
        //show text letter by letter
        StartCoroutine(textboxManager.TypeText(textboxManager.textLines[textboxManager.currentLine]));

    }
}
