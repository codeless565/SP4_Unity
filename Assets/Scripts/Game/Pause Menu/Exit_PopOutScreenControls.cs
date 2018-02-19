using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit_PopOutScreenControls : MonoBehaviour
{
    [SerializeField]
    GameObject Pause_PopOutScreen, Exit_PopOutScreen, LoadingScreen;

    [SerializeField]
    Slider m_progressBar;

    AsyncOperation async;

    // Timer Delay to finish playing sound effect
    private float m_delayTime;

    // Use this for initialization
    void Start ()
    {
        m_delayTime = 0.25f;
    }
	
	// Update is called once per frame
	void Update () { }

    // When No Button is Pressed.
    public void NoButtonPressed()
    {
        Pause_PopOutScreen.SetActive(true);
        Exit_PopOutScreen.SetActive(false);
    }

    // When Yes Button is Pressed.
    public void YesButtonPressed()
    {
        // Transit to MainMenuScreen
        StartCoroutine(TutorialScreenToMainMenu());
    }

    IEnumerator TutorialScreenToMainMenu()
    {
        yield return new WaitForSecondsRealtime(m_delayTime);

        LoadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync("SceneMainMenu");
        async.allowSceneActivation = false;

        // If MainMenu is not loaded fully
        while(async.isDone == false)
        {
            m_progressBar.value = async.progress;
            // While MainMenu is loaded.
            if(async.progress >= 0.9f)
            {
                m_progressBar.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
