using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_PopOutScreenControls : MonoBehaviour
{
    [SerializeField]
    Button m_backButton;

    [SerializeField]
    GameObject Pause_PopOutScreen;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

    // When Pause Button is Pressed.
    public void PauseButtonPressed()
    {
        Pause_PopOutScreen.SetActive(true);
    }

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        Pause_PopOutScreen.SetActive(false);
    }
}
