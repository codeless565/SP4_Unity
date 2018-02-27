using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    public GameObject PauseMenu;

    bool b_isPaused;
    int t_CurrFloor;

    void Start ()
    {
        GameStart();

        if (PauseMenu != null)
            PauseMenu.SetActive(false);
    }

    // Interface Functions // 
    public void GameStart()
    {
        //check player's curr florr and init accordingly
        t_CurrFloor = PlayerPrefs.GetInt("CurrentLevel");// set from player's curr floor
        if (t_CurrFloor <= 0)
        {
            t_CurrFloor = 1;
        }
        Debug.Log("Current Floor: " + t_CurrFloor);

        /* Initialize Level */
        GetComponent<BoardGenerator>().Init();

        /* Spawn Objects in Level */
        GetComponent<ObjectSpawn>().Init(t_CurrFloor);

        /* Start Timer */
        GetComponent<GameTimer>().Init();

        /* Initialize Player Required Scripts */
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
        //player curr floor + 1
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats());

        PlayerPrefs.SetInt("CurrentLevel", t_CurrFloor + 1);
        SceneManager.LoadScene("SceneGame_2D");
    }

    public void GameOver()
    {
        // Set Previous Scene and Delete level progression
        PlayerPrefs.SetString("PreviousGameScene", "SceneGame_2D");
        PlayerPrefs.DeleteKey("CurrentLevel");

        // Clear player's inventory
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());
        
        int TotalItems = PlayerPrefs.GetInt("NumStoredItems");
        for (int i = 0; i < TotalItems; ++i) 
        {
            PlayerPrefs.DeleteKey("item " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredItems");

        // Clear player's stats
        PlayerSaviour.Instance.SavePref(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerStats());
        PlayerPrefs.DeleteKey("Player_Stats");

        SceneManager.LoadScene("SceneGameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SceneGame_2D");
    }
}
