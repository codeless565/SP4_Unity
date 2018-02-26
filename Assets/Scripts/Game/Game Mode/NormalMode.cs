using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    private float elapseTime;
    int t_CurrFloor;

    void Start ()
    {
        t_CurrFloor = PlayerPrefs.GetInt("CurrentLevel");// set from player's curr floor
        //check player's curr florr and init accordingly

        Debug.Log("Current Floor: " + t_CurrFloor);

        GetComponent<BoardGenerator>().Init();
        GetComponent<ObjectSpawn>().Init(t_CurrFloor);
        GetComponent<Player2D_StatsMenu>().Init();

        GetComponent<Inventory>().Init();
        GetComponent<InventoryDisplay>().Init();

        GetComponent<Shop>().Init();
        GetComponent<ShopDisplay>().Init();
    }

    // Interface Functions // 
    public void GameStart()
    {
        //New Progression
        if (t_CurrFloor <= 0)
        {
            t_CurrFloor = 1;

        }
    }

    public void GameClear()
    {
        //player curr floor + 1
        //CSVSaviour.Instance.Save(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());
        PlayerInvSaviour.Instance.SaveInv(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());

        PlayerPrefs.SetInt("CurrentLevel", t_CurrFloor + 1);
        SceneManager.LoadScene("SceneGame_2D");
    }

    public void GameOver()
    {
        // Set Previous Scene and Delete level progression
        PlayerPrefs.SetString("PreviousGameScene", "SceneGame_2D");
        PlayerPrefs.DeleteKey("CurrentLevel");

        // Clear player's inventory
        PlayerInvSaviour.Instance.SaveInv(GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().getPlayerInventory());

        int TotalItems = PlayerPrefs.GetInt("NumStoredItems");
        for (int i =0;i<TotalItems;++i)
        {
            //Debug.Log
            PlayerPrefs.DeleteKey("item " + i);
        }
        PlayerPrefs.DeleteKey("NumStoredItems");
        SceneManager.LoadScene("SceneGameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SceneGame_2D");
    }
}
