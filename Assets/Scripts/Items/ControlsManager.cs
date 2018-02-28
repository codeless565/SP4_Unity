using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlsManager : MonoBehaviour
{
    public GameObject DisplayCanvas;
    private GameObject ButtonPrefab;
    private GameObject player;
    GameObject[] ControlButtons;
    public KeyCode[] ControlsKeyCodes;
    public enum EControls
    {
        MOVEFORWARD,
        MOVEBACKWARD,
        MOVELEFT,
        MOVERIGHT,
        INVENTORY,
        OPTIONS,
        TOTAL
    }

    bool CanvasActive;
    bool editingkey;
    int SelectedControl;
    // Use this for initialization
    public void Init()
    {
        ControlsKeyCodes = new KeyCode[(int)EControls.TOTAL];
        ControlButtons = new GameObject[(int)EControls.TOTAL];
        SelectedControl = (int)EControls.TOTAL;
        editingkey = false;
        CanvasActive = false;

        ButtonPrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().ButtonPrefab;
        for (int i = 0; i < (int)EControls.TOTAL; ++i)
        {
            GameObject newIcon = Instantiate(ButtonPrefab, DisplayCanvas.transform);

            ControlButtons[i] = newIcon;
            newIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            newIcon.GetComponent<Button>().onClick.AddListener(delegate { ControlClicked(newIcon); });
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.GetString("Player_Controls") != "")
            PlayerSaviour.Instance.LoadControls(ControlsKeyCodes);
        else
            InitDefaultControls();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCanvas.SetActive(CanvasActive);
        ControlButtonsUpdate();

        if (editingkey)
        {
            if (Input.anyKeyDown)
                if (CheckKey(Input.inputString))
                {
                    ControlsKeyCodes[SelectedControl] = ReturnKey(Input.inputString);
                    player.GetComponent<Player2D_Manager>().canMove = true;
                    editingkey = false;
                    CanvasActive = false;
                    PlayerSaviour.Instance.SavePref(ControlsKeyCodes);
                }
        }

        if(CanvasActive)
            player.GetComponent<Player2D_Manager>().canMove = false;
    }
    public void setCanvasActive() { CanvasActive = !CanvasActive; }
    public void ControlClicked(GameObject icon)
    {
        SelectedControl = 0;
        for (int i = 0; i < ControlButtons.Length; ++i)
        {
            if (ControlButtons[i].GetComponentInChildren<Text>().text == icon.GetComponentInChildren<Text>().text)
            {
                SelectedControl = i;
            }
        }
        if (!editingkey)
            editingkey = true;
    }

    public void ControlButtonsUpdate()
    {
        for (int i = 0; i < (int)EControls.TOTAL; ++i)
        {
            switch ((EControls)i)
            {
                case EControls.MOVEFORWARD:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Move forward " + ControlsKeyCodes[i];
                    break;
                case EControls.MOVEBACKWARD:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Move backward " + ControlsKeyCodes[i];
                    break;
                case EControls.MOVELEFT:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Move left " + ControlsKeyCodes[i];
                    break;
                case EControls.MOVERIGHT:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Move right " + ControlsKeyCodes[i];
                    break;
                case EControls.INVENTORY:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Inventory " + ControlsKeyCodes[i];
                    break;
                case EControls.OPTIONS:
                    ControlButtons[i].GetComponentInChildren<Text>().text = "Options " + ControlsKeyCodes[i];
                    break;
            }

        }
    }

    void InitDefaultControls()
    {
        ControlsKeyCodes[(int)EControls.MOVEFORWARD] = KeyCode.W;
        ControlsKeyCodes[(int)EControls.MOVEBACKWARD] = KeyCode.S;
        ControlsKeyCodes[(int)EControls.MOVELEFT] = KeyCode.A;
        ControlsKeyCodes[(int)EControls.MOVERIGHT] = KeyCode.D;
        ControlsKeyCodes[(int)EControls.INVENTORY] = KeyCode.I;
        ControlsKeyCodes[(int)EControls.OPTIONS] = KeyCode.O;
    }

    public KeyCode ReturnKey(string input)
    {
        KeyCode thisKeyCode = KeyCode.None;


        thisKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), input.ToUpper());

        return thisKeyCode;
    }

    public bool CheckKey(string input)
    {
        if (input[0] >= 'a' && input[0] <= 'z')
            return true;

        return false;
    }

    public KeyCode GetKey(string _control)
    {
        KeyCode thisKeyCode = KeyCode.None;

        if (_control.ToUpper() == EControls.MOVEFORWARD.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.MOVEFORWARD];
        else if (_control.ToUpper() == EControls.MOVEBACKWARD.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.MOVEBACKWARD];
        else if (_control.ToUpper() == EControls.MOVELEFT.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.MOVELEFT];
        else if (_control.ToUpper() == EControls.MOVERIGHT.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.MOVERIGHT];
        else if (_control.ToUpper() == EControls.INVENTORY.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.INVENTORY];
        else if (_control.ToUpper() == EControls.OPTIONS.ToString())
            thisKeyCode = ControlsKeyCodes[(int)EControls.OPTIONS];

        return thisKeyCode;
    }
}
