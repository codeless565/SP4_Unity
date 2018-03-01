using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextBox : MonoBehaviour
{
    TextBoxManager textboxManager;
    TutorialMode tutMode;
    TutorialSpawn tutSpawn;
	//public GameObject checkChest;
    Player2D_StatsMenu statsMenu;
    Inventory inventory;
    MerchantStateMachine merchant;

    Player2D_Manager player;

    public bool chestOpened;

    public TextAsset theTextMobile, theTextConsole;
    public int startLine;
    public int endLine;

    public bool MovedW, MovedA, MovedS, MovedD;

    bool triedPause, triedPP, triedAttack, triedMove, triedCollecting, triedInventory, triedMerchant, triedTrap1, triedTrap2, triedTrap3, triedTrap4;
    public bool pauseBox;

    //Arrows
    public Image HealthArrow, StaminaArrow, EXPArrow, TimerArrow, PPArrow, MinimapArrow, InventoryArrow, LevelArrow;
    public GameObject Arrows;

    // Use this for initialization
    void Start()
    {
        textboxManager = FindObjectOfType<TextBoxManager>();
        tutMode = GetComponent<TutorialMode>();
        statsMenu = GetComponent<Player2D_StatsMenu>();
        tutSpawn = GetComponent<TutorialSpawn>();
        inventory = GetComponent<Inventory>();

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
        chestOpened = false;
        triedPause= triedPP= triedAttack= triedMove= triedCollecting= triedInventory= triedMerchant= triedTrap1= triedTrap2= triedTrap3= triedTrap4 = false;
        pauseBox = false;
    }

    bool getTrigger()
    {
        
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetKeyDown(KeyCode.Return);
#elif UNITY_ANDROID || UNITY_IPHONE
        bool touched = false;
           if(Input.GetTouch(0).phase == TouchPhase.Began)
            touched = true;
        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        touched = false;

        return touched;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedUpdate();
        if (getTrigger() && !pauseBox && Time.timeScale == 1)
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
                break;

            case 4: //trying stage for pausing
               if(tutMode.b_isPaused)
			    {
				    textboxManager.currentLine = 5;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                break;

            case 5:
                 MinimapArrow.gameObject.SetActive(true);
                break;

            case 6:
                HealthArrow.gameObject.SetActive(true);
                break;

            case 7:
                StaminaArrow.gameObject.SetActive(true);
                break;

            case 8:
                EXPArrow.gameObject.SetActive(true);
                break;

            case 9:
                 PPArrow.gameObject.SetActive(true);
                 break;

            case 10: //trying stage for opening PP menu
                if (statsMenu.statsMenuClosed) //check opened pp
                {
                    textboxManager.currentLine = 11;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                    break;

            case 12: //trying stage for moving
                if (!MovedA || !MovedD || !MovedS || !MovedW)
                     {

#if UNITY_EDITOR || UNITY_STANDALONE
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

#elif UNITY_ANDROID || UNITY_IPHONE
       if( player.inputX > 0)
                    MovedD = true;
       if( player.inputX < 0)
                    MovedA = true;
       if( player.inputY > 0)
                    MovedW = true;
       if( player.inputY < 0)
                    MovedS = true;
#endif

                }
                else
                     {
                         textboxManager.currentLine = 13;
                         pauseBox = false;
                         textboxManager.EnableTextBox();
                     }
                     break;

            case 13: //spawn enemy
                tutSpawn.EnemySpawn();
                break;

            case 15: //trying stage for attacking
                if(Input.GetMouseButton(0))
                {
                    textboxManager.currentLine = 16;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                break;

            case 16: //spawn chest
                tutSpawn.ItemSpawn();
                break;

            case 17: //trying stage for chest collectiond
                if(chestOpened)
                {
                    textboxManager.currentLine = 18;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                    break;

            case 18:
                InventoryArrow.gameObject.SetActive(true);
                break;

            case 19: //trying stage for opening inventory
                if(inventory.InventroyClosed)
                {
                    textboxManager.currentLine = 20;
                    pauseBox = false;
                    textboxManager.EnableTextBox();
                }
                break;

            case 21: //show slow trap
                tutSpawn.TrapSpawn1();
                break;

            case 22:
                tutSpawn.TrapSpawn2();
                break;

            case 23:
                tutSpawn.TrapSpawn3();
                break;

            case 24:
                tutSpawn.TrapSpawn4();
                break;

            case 25:
                tutSpawn.ExitSpawn();
                break;

        }

    }

    void TutorialUpdate()
    {
        if(textboxManager.currentLine == 4 || textboxManager.currentLine == 10 || textboxManager.currentLine == 12 || 
            textboxManager.currentLine == 15 || textboxManager.currentLine == 17 || textboxManager.currentLine == 19)
        {
            pauseBox = true;
            textboxManager.DisableTextBox();
        }
      
        //show text letter by letter
        StartCoroutine(textboxManager.TypeText(textboxManager.textLines[textboxManager.currentLine]));

    }
}
