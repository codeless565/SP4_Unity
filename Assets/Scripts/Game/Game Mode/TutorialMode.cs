using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialMode : MonoBehaviour, GameMode
{
    public GameObject LoadingInScreen;
    public GameObject LoadingNextScreen;
    public GameObject PauseMenu;
    public Text FloorDetails;

    public bool b_isPaused;
    int t_CurrFloor;

    /* mobile things*/
    public GameObject Mobile;
    void Awake()
    {
        GameStart();

        if (PauseMenu != null)
            PauseMenu.SetActive(false);

        FloorDetails.text = "Tutorial";
    }

    // Interface Functions // 
    public void GameStart()
    {
        //check player's curr florr and init accordingly
        t_CurrFloor = 1;// set from player's curr floor
        LoadingInScreen.GetComponent<LoadingIntoGame>().Init(gameObject, -1);

        /* Initialize Level */
        GetComponent<ArenaGenerator>().Init();

        /* Spawn Objects in Level */
        GetComponent<TutorialSpawn>().Init();

        /* Start Timer */
        GetComponent<GameTimer>().Init();

        /* Initialize Player Required Scripts */
        GetComponent<ControlsManager>().Init();
        GetComponent<PlayerHUD>().Init();
        GetComponent<Player2D_StatsMenu>().Init();

        GetComponent<Inventory>().Init();
        GetComponent<InventoryDisplay>().Init();

        GetComponent<Shop>().Init();
        GetComponent<ShopDisplay>().Init();

        GetComponent<TextBoxManager>().Init();


#if UNITY_ANDROID || UNITY_IPHONE
        if (Mobile != null)
        {
            foreach (object obj in Mobile.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(true);
            }
        }
#endif
    }

    public void GamePause()
    {
        if (!b_isPaused)
        {
            Time.timeScale = 0;
            b_isPaused = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            b_isPaused = false;
            PauseMenu.SetActive(false);
        }
    }

    IEnumerator LoadingMainMenuScreen()
    {
        LoadingNextScreen.GetComponent<LoadingNextFloor>().Init();
        AsyncOperation async = SceneManager.LoadSceneAsync("SceneMainMenu");
        async.allowSceneActivation = false;

        // If SceneGame is not loaded fully.
        while (async.isDone == false)
        {
            LoadingNextScreen.GetComponent<LoadingNextFloor>().UpdateLoadAnimation();

            yield return new WaitForSecondsRealtime(1f);

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync(4);
            }
            yield return null;
        }
    }

    public void GameClear()
    {
		Time.timeScale = 1;
        StartCoroutine(LoadingMainMenuScreen());
       // SceneManager.LoadScene("SceneMainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 1;

        /* Set PreviousGameScene before going next scene*/
        PlayerPrefs.SetString("PreviousGameScene", "SceneTutorial_2D");
        SceneManager.LoadScene("SceneGameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SceneTutorial_2D");
    }

    public int CurrentFloor
    {
        get
        {
            return t_CurrFloor;
        }
    }

    public bool Pause
    {
        get
        {
            return b_isPaused;
        }

        set
        {
            b_isPaused = !value; // set opposing effect to trigger desired effect
            GamePause();
        }
    }
}