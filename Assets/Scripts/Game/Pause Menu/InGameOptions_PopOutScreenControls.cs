using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameOptions_PopOutScreenControls : MonoBehaviour
{
    [SerializeField]
    Button m_backButton, m_applyButton;

    [SerializeField]
    GameObject Options_PopOutScreen, Pause_PopOutScreen;

    [SerializeField]
    GameObject backgroundMusic, buttonPressedSFX;
    
    // Tutorial Background Music
    public Slider musicVolumeSlider;
    private AudioSource musicSource;
    // Sound Effects
    public Slider soundEffectsSlider;
    private AudioSource buttonPressedSFX_Source;

	// Use this for initialization
	void Start ()
    {
        // Background Music
        musicSource = backgroundMusic.GetComponent<AudioSource>();
        // Setting the musicVolumeSlider value.
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM_Volume", 1);

        // Button Pressed Sound Effects

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Changing the BGM volume based on the Slider value.
        musicSource.volume = musicVolumeSlider.value;
    }

    // When Apply Button is Pressed.
    public void ApplyButtonPressed()
    {
        // Save the Changes made to the MusicVolume Slider.
        PlayerPrefs.SetFloat("BGM_Volume", musicVolumeSlider.value);
    }

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        // If Back is Prssed w/o saving...
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM_Volume", 1);

        // Set OptionsPopOutScreen active to False.
        Options_PopOutScreen.SetActive(false);
        // Set PausePopOutScreen active to True.
        Pause_PopOutScreen.SetActive(true);
    }
}
