using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_PopOutScreenControls : MonoBehaviour {

    [SerializeField]
    Button m_backButton;

    [SerializeField]
    GameObject Options_PopOutScreen;


    // Use this for initialization
    void Start () {	}
	
	// Update is called once per frame
	void Update () {}

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        // Set Active to False, Options Pop-Out Screen will not be rendered.
        Options_PopOutScreen.SetActive(false);
    }

}
