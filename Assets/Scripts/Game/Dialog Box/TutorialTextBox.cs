using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextBox : MonoBehaviour
{
    TextBoxManager textboxManager;

    public TextAsset theText;
    public int startLine;
    public int endLine;

    public bool MovedW, MovedA, MovedS, MovedD;
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

        if (Input.GetKeyDown(KeyCode.Return) && !pauseBox && Time.timeScale == 1)
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
                if(!MovedA || !MovedD || !MovedS || !MovedW)
                {
                        if (Input.GetKey(KeyCode.A))
                        {
                            MovedA = true;
                        }
                    
                        if (Input.GetKey(KeyCode.D))
                        {
                            MovedD = true;
                        }
                  
                        if (Input.GetKey(KeyCode.S))
                        {
                            MovedS = true;
                        }
                  
                        if (Input.GetKey(KeyCode.W))
                        {
                            MovedW = true;
                        }
                        
                   
                }
                else
                {
                    textboxManager.currentLine = 3;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
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
                    textboxManager.currentLine = 5;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
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
                    textboxManager.currentLine = 7;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
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
                    textboxManager.currentLine = 9;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                break;
        }

    }

    void TutorialUpdate()
    {
        if(textboxManager.currentLine == 2 || textboxManager.currentLine == 4 || textboxManager.currentLine == 6 || textboxManager.currentLine == 8)
        {
            if(!MovedA || !MovedD || !MovedS || !MovedW)
            {
                pauseBox = true;
                textboxManager.DisableTextBox();
            }
            else if(!triedAttack)
            {
                pauseBox = true;
                textboxManager.DisableTextBox();
            }
            else if(!triedChangeW)
            {
                pauseBox = true;
                textboxManager.DisableTextBox();
            }
            else if(!triedInteract)
            {
                pauseBox = true;
                textboxManager.DisableTextBox();
            }
        }  
        //show text letter by letter
        StartCoroutine(textboxManager.TypeText(textboxManager.textLines[textboxManager.currentLine]));

    }
}
