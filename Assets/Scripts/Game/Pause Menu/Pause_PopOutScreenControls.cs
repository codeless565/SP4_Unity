using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_PopOutScreenControls : MonoBehaviour
{
    [SerializeField]
    GameObject Pause_PopOutScreen, Options_PopOutScreen, Exit_PopOutScreen;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

    // When Pause Button is Pressed.
    public void PauseButtonPressed()
    {
        Pause_PopOutScreen.SetActive(true);
    }

    // When Options Button is Pressed.
    public void OptionButtonPressed()
    {
        // Set PausePopOutScreen active to False.
        Pause_PopOutScreen.SetActive(false);
        // Set OptionsPopOutScreen active to true.
        Options_PopOutScreen.SetActive(true);
    }

    // When Back Button is Pressed.
    public void ResumeButtonPressed()
    {
        Pause_PopOutScreen.SetActive(false);
    }

    // When Exit Button is Pressed.
    public void ExitButtonPressed()
    {
        Pause_PopOutScreen.SetActive(false);
        Exit_PopOutScreen.SetActive(true);
    }
}
