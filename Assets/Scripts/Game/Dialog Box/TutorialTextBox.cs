using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextBox : MonoBehaviour
{
    TextBoxManager textboxManager;
    TutorialSpawn tutSpawn;
    public TextAsset theTextMobile, theTextConsole;
    public int startLine;
    public int endLine;

    public bool MovedW, MovedA, MovedS, MovedD;

    bool triedPause, triedPP, triedAttack, triedMove, triedCollecting, triedInventory, triedMerchant, triedTrap1, triedTrap2, triedTrap3, triedTrap4;
    public bool pauseBox;

    //Arrows
  //  public bool showHealthArrow, showStaminaArrow, showEXPArrow, showTimerArrow, showPPArrow, showLevelArrow, showMinimapArrow, showInventoryArrow;
    public Image HealthArrow, StaminaArrow, EXPArrow, TimerArrow, PPArrow, MinimapArrow, InventoryArrow, LevelArrow;
    public GameObject Arrows, ArrowShown;
    // Use this for initialization
    void Start()
    {
      //  showEXPArrow = showHealthArrow = showInventoryArrow = showLevelArrow = showMinimapArrow = showPPArrow = showStaminaArrow = showTimerArrow = false;

        textboxManager = FindObjectOfType<TextBoxManager>();
#if UNITY_EDITOR || UNITY_STANDALONE
        textboxManager.ReloadScript(theTextConsole);
#elif UNITY_ANDROID || UNITY_IPHONE
        textboxManager.ReloadScript(theTextMobile);
#endif
        textboxManager.currentLine = startLine;
        textboxManager.endAtLine = endLine;
        textboxManager.EnableTextBox();

        MovedW = false;
        MovedA = false;
        MovedS = false;
        MovedD = false;
        triedPause= triedPP= triedAttack= triedMove= triedCollecting= triedInventory= triedMerchant= triedTrap1= triedTrap2= triedTrap3= triedTrap4 = false;
        pauseBox = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        KeyPressedUpdate();
//#elif UNITY_ANDROID || UNITY_IPHONE
    //    AccMove();
#endif

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
        if (Arrows != null)
        {
            foreach (object obj in Arrows.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        switch (textboxManager.currentLine)
        {
            case 1:
                LevelArrow.gameObject.SetActive(true);
                break;

            case 2:
                TimerArrow.gameObject.SetActive(true);
                break;

            case 3:
                TimerArrow.gameObject.SetActive(true);
                if(triedPause)
                {
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                break;

            case 5:
                MinimapArrow.gameObject.SetActive(true);
                break;

            //case 5:
            //    HealthArrow.gameObject.SetActive(true);
            //    break;

            //case 6:
            //    StaminaArrow.gameObject.SetActive(true);
            //    break;
                
            //case 7:
            //    EXPArrow.gameObject.SetActive(true);
            //    break;

            //case 8:
            //    PPArrow.gameObject.SetActive(true);
            //    break;

            //case 13:
            //    InventoryArrow.gameObject.SetActive(true);
            //    break;


                //case 2:
                //    if (!MovedA || !MovedD || !MovedS || !MovedW)
                //    {
                //        if (Input.GetKey(KeyCode.A))
                //        {
                //            MovedA = true;
                //        }

                //        if (Input.GetKey(KeyCode.D))
                //        {
                //            MovedD = true;
                //        }

                //        if (Input.GetKey(KeyCode.S))
                //        {
                //            MovedS = true;
                //        }

                //        if (Input.GetKey(KeyCode.W))
                //        {
                //            MovedW = true;
                //        }
                //    }
                //    else
                //    {
                //        textboxManager.currentLine = 3;
                //        pauseBox = false;
                //        textboxManager.EnableTextBox();
                //    }
                //    break;

                //case 4:
                //    if (!triedAttack)
                //    {
                //        if (Input.GetMouseButton(0))
                //        {
                //            triedAttack = true;
                //        }
                //    }
                //    else
                //    {
                //        textboxManager.currentLine = 5;
                //        pauseBox = false;
                //        textboxManager.EnableTextBox();
                //    }
                //    break;

                //case 6:
                //    if (!triedChangeW)
                //    {
                //        if (Input.GetKey(KeyCode.C))
                //        {
                //            triedChangeW = true;
                //        }
                //    }
                //    else
                //    {
                //        textboxManager.currentLine = 7;
                //        pauseBox = false;
                //        textboxManager.EnableTextBox();
                //    }
                //    break;

                //case 8:
                //    if (!triedInteract)
                //    {
                //        if (Input.GetKey(KeyCode.I))
                //        {
                //            triedInteract = true;
                //        }
                //    }
                //    else
                //    {
                //        textboxManager.currentLine = 9;
                //        pauseBox = false;
                //        textboxManager.EnableTextBox();
                //    }
                //    break;
        }

    }

    void MobileTappedUpdate()
    {

    }
    void TutorialUpdate()
    {
        switch(textboxManager.currentLine)
        {
            case 4:
                if(!triedPause)
                {
                    pauseBox = true;
                    textboxManager.DisableTextBox();
                }
                break;
        }
        //if(textboxManager.currentLine == 2 || textboxManager.currentLine == 4 || textboxManager.currentLine == 6 || textboxManager.currentLine == 8)
        //{
        //    if(!MovedA || !MovedD || !MovedS || !MovedW)
        //    {
        //        pauseBox = true;
        //        textboxManager.DisableTextBox();
        //    }
        //    else if(!triedAttack)
        //    {
        //        pauseBox = true;
        //        textboxManager.DisableTextBox();
        //    }
        //    else if(!triedChangeW)
        //    {
        //        pauseBox = true;
        //        textboxManager.DisableTextBox();
        //    }
        //    else if(!triedInteract)
        //    {
        //        pauseBox = true;
        //        textboxManager.DisableTextBox();
        //    }
        //}  
        //show text letter by letter
        StartCoroutine(textboxManager.TypeText(textboxManager.textLines[textboxManager.currentLine]));

    }
}
