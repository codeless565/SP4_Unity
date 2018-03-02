using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAudio : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundMusic;

    // Background Music
    private AudioSource musicSource;
    
	void Start ()
    {
        // Background Music
        musicSource = backgroundMusic.GetComponent<AudioSource>();
        // Get Background Music
        musicSource.volume = PlayerPrefs.GetFloat("BGM_Volume", 1);
	}
	
	void Update ()
    {
        // Get Background Music
        musicSource.volume = PlayerPrefs.GetFloat("BGM_Volume", 1);
    }
}
