using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour {

    [SerializeField]
    Button m_startButton, m_optionButton;

    [SerializeField]
    GameObject Levels_PopOutScreen, Options_PopOutScreen;
     

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    // When Play Button is Pressed.
    public void PlayButtonPressed()
    {
        // Set Active to True, Levels Pop-Out Screen will appear.
        Levels_PopOutScreen.SetActive(true);
    }

    // When Options Button is Pressed.
    public void OptionsButtonPressed()
    {
        // Set Active to True, Options Pop-Out Screen will appear.
        Options_PopOutScreen.SetActive(true);
    }
}
