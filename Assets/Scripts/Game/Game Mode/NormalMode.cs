using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    public GameObject LoadingInScreen;
    public GameObject LoadingNextScreen;
    public GameObject PauseMenu;
    public Text FloorDetails;

    bool m_isBossFloor;

    bool b_isPaused;
    int t_CurrFloor;

    void Start ()
    {
        GameStart();

        if (PauseMenu != null)
            PauseMenu.SetActive(false);

        FloorDetails.text = "Floor " + t_CurrFloor.ToString();
        LoadingInScreen.GetComponent<LoadingIntoGame>().Init(gameObject, t_CurrFloor, m_isBossFloor);
    }

    // Interface Functions // 
    public void GameStart()
    {
        //check player's curr florr and init accordingly
        t_CurrFloor = PlayerPrefs.GetInt("CurrentLevel");// set from player's curr floor
        if (t_CurrFloor <= 0)
        { t_CurrFloor = 1; }

        /* Initialize Level
         * set different time if it is on Boss level;
         * Spawn Objects in Level */
        m_isBossFloor = false;
        if (t_CurrFloor % 5 == 0)
        {
            // Spawn Boss Level
            GetComponent<ArenaGenerator>().Init();
            GetComponent<ArenaBossSpawn>().Init(t_CurrFloor);
            m_isBossFloor = true;
        }
        else
        {
            // Spawn Normal Level
            GetComponent<BoardGenerator>().Init();
            GetComponent<ObjectSpawn>().Init(t_CurrFloor);
        }

        /* Start Timer */
        GetComponent<GameTimer>().Init(m_isBossFloor);

        /* Initialize Player Required Scripts */
        GetComponent<ControlsManager>().Init();
        GetComponent<AchievementsManager>().Init();
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
        /* Save Player Inventory */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());

        /* Save Player Stats */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats());

        /* Save Player Equipment */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getEQList());

        /* Save Player Equipment */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().AchievementsList);

        /* Save Player Equipment */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().PropertiesList);


        /* Set next level */
        PlayerPrefs.SetInt("CurrentLevel", t_CurrFloor + 1);

        /* Load next level */
        //SceneManager.LoadScene("SceneGame_2D");
        //GamePause();
        StartCoroutine(LoadingNextLevelScreen());
    }

    public void GameOver()
    {
        /* Reset Progression */
        PlayerPrefs.DeleteKey("CurrentLevel");

        /* Clear player's inventory */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());
        
        int TotalItems = PlayerPrefs.GetInt("NumStoredItems");
        for (int i = 0; i < TotalItems; ++i) 
        {
            PlayerPrefs.DeleteKey("item " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredItems");

        /* Clear player's stats */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats());
        PlayerPrefs.DeleteKey("Player_Stats");

        /* Clear player's equipments */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getEQList());
        int TotalEquipments = PlayerPrefs.GetInt("NumStoredEquipments");
        for (int i = 0; i < TotalEquipments; ++i)
        {
            PlayerPrefs.DeleteKey("equipment " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredEquipments");

        /* Clear Player Achievements */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().AchievementsList);
        int TotalAchievements = PlayerPrefs.GetInt("NumStoredAchievements");
        for (int i = 0; i < TotalAchievements; ++i)
        {
            PlayerPrefs.DeleteKey("achievement " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredAchievements");

        /* Clear Player Properties */
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("GameScript").GetComponent<AchievementsManager>().PropertiesList);
        int TotalProperties = PlayerPrefs.GetInt("NumStoredProperties");
        for (int i = 0; i < TotalAchievements; ++i)
        {
            PlayerPrefs.DeleteKey("property " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredProperties");

        /* Set PreviousGameScene before going next scene*/
        PlayerPrefs.SetString("PreviousGameScene", "SceneGame_2D");
        SceneManager.LoadScene("SceneGameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SceneGame_2D");
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


    IEnumerator LoadingNextLevelScreen()
    {
        LoadingNextScreen.GetComponent<LoadingNextFloor>().Init();
        AsyncOperation async = SceneManager.LoadSceneAsync("SceneGame_2D");
        async.allowSceneActivation = false;

        // If SceneGame is not loaded fully.
        while (async.isDone == false)
        {
            LoadingNextScreen.GetComponent<LoadingNextFloor>().UpdateLoadAnimation();

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
