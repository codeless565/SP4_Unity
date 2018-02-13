using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_PopOutScreenControls : MonoBehaviour {

    [SerializeField]
    Button m_backButton, m_applyButton;

    [SerializeField]
    GameObject Options_PopOutScreen;

    [SerializeField]
    GameObject backgroundMusic;

    // Background Music
    public Slider musicVolumeSlider;
    private AudioSource musicSource;

    // Use this for initialization
    void Start()
    {
        musicSource = backgroundMusic.GetComponent<AudioSource>();
        // Setting the musicVolumeSlider value.
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MainMenu_BGM_Volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Changing the BGM volume based on the Slider value.
        musicSource.volume = musicVolumeSlider.value;
    }

    // When Apply Button is Pressed.
    public void ApplyButtonPressed()
    {
        // Save the Changes made to the MusicVolume Slider.
        PlayerPrefs.SetFloat("MainMenu_BGM_Volume", musicVolumeSlider.value);
    }

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        // If Back is Pressed w/o saving, the music volume will stay the same.
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MainMenu_BGM_Volume", 1);

        // Set Active to False, Options Pop-Out Screen will not be rendered.
        Options_PopOutScreen.SetActive(false);
    }

}
