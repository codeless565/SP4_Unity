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

    bool b_isPaused;
    int t_CurrFloor;

    void Start()
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

    public void GameClear()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    public void GameOver()
    {
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