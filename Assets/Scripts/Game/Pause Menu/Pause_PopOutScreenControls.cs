using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_PopOutScreenControls : MonoBehaviour
{
    [SerializeField]
    Button m_resumeButton;

    [SerializeField]
    GameObject Pause_PopOutScreen, Options_PopOutScreen;

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
}
