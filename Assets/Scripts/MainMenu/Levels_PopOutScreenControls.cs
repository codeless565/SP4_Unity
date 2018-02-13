using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels_PopOutScreenControls : MonoBehaviour {

    [SerializeField]
    Button m_backButton, m_playButton, m_tutorialButton;

    [SerializeField]
    GameObject Levels_PopOutScreen, LoadingScreen;

    [SerializeField]
    Slider m_progressBar;

    AsyncOperation async;

    // Use this for initialization
    void Start () { }
	
	// Update is called once per frame
	void Update () { }

    // When Play Button is Pressed.
    public void PlayButtonPressed()
    {
        // Transit to LoadingScreen
        StartCoroutine(LoadingScreenToSceneGame());
    }

    // When Tutorial Button is Pressed.
    public void TutorialButtonPressed()
    {
        // Transit to LoadingScreen
        //StartCoroutine(LoadingScreenToSceneTutorial());
    }

    // When Back Button is Pressed.
    public void BackButtonPressed()
    {
        // Set Active to False, Levels Pop-Out Screen will not be rendered.
        Levels_PopOutScreen.SetActive(false);
    }


    // Loading Screen ----------------------------------
    // Loading to SceneGame.
    IEnumerator LoadingScreenToSceneGame()
    {
        LoadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync("SceneGame");
        async.allowSceneActivation = false;

        // If SceneGame is not loaded fully.
        while (async.isDone == false)
        {
            m_progressBar.value = async.progress;
            // When GameScene is loaded.
            if (async.progress == 0.9f)
            {
                m_progressBar.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Loading to SceneTutorial
    //IEnumerator LoadingScreenToSceneTutorial()
    //{
    //    LoadingScreen.SetActive(true);
    //    async = SceneManager.LoadSceneAsync("SceneTutorial");
    //    async.allowSceneActivation = false;

    //    // If SceneGame is not loaded fully.
    //    while (async.isDone == false)
    //    {
    //        m_progressBar.value = async.progress;
    //        // When GameScene is loaded.
    //        if (async.progress == 0.9f)
    //        {
    //            m_progressBar.value = 1f;
    //            async.allowSceneActivation = true;
    //        }
    //        yield return null;
    //    }
    //}
}
