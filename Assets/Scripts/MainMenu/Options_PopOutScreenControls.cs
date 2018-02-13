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
    GameObject backgroundMusic, buttonPressedSFX;

    // Background Music
    public Slider musicVolumeSlider;
    private AudioSource musicSource;
    // Sound Effects
    public Slider soundEffectSlider;
    private AudioSource buttonPressedSFX_Source;


    // Use this for initialization
    void Start()
    {
        // Background Music
        musicSource = backgroundMusic.GetComponent<AudioSource>();
        // Setting the musicVolumeSlider value.
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MainMenu_BGM_Volume", 1);

        // Button Pressed Sound Effects
        buttonPressedSFX_Source = buttonPressedSFX.GetComponent<AudioSource>();
        // Setting the soundEffectsSlider value.
        soundEffectSlider.value = PlayerPrefs.GetFloat("ButtonPressedSFX_Volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Changing the BGM volume based on the Slider value.
        musicSource.volume = musicVolumeSlider.value;
        // Changing the SFX volume based on the Slider value.
        buttonPressedSFX_Source.volume = soundEffectSlider.value;
    }

    // When Apply Button is Pressed.
    public void ApplyButtonPressed()
    {
        // Save the Changes made to the MusicVolume Slider.
        PlayerPrefs.SetFloat("MainMenu_BGM_Volume", musicVolumeSlider.value);
        // Save the Changes made to the SoundEFfects Slider.
        PlayerPrefs.SetFloat("ButtonPressedSFX_Volume", soundEffectSlider.value);
    }

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        // If Back is Pressed w/o saving...
        // The music volume will stay the same.
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MainMenu_BGM_Volume", 1);
        // The sound effects volume will stay the same.
        soundEffectSlider.value = PlayerPrefs.GetFloat("ButtonPressedSFX_Volume", 1);

        // Set Active to False, Options Pop-Out Screen will not be rendered.
        Options_PopOutScreen.SetActive(false);
    }

}
