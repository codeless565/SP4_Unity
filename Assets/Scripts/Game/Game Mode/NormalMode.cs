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

        GetComponent<Inventory>().Init();

        GetComponent<Shop>().Init();
        GetComponent<ShopDisplay>().Init();
    }

    // Interface Functions // 
    public void GameStart()
    {
        //Initialize Interface and clock
    }

    public void GameClear()
    {
        //player curr floor + 1
        PlayerPrefs.SetInt("CurrentLevel", t_CurrFloor + 1);
        SceneManager.LoadScene("SceneGame_2D");
    }

    public void GameOver()
    {
        PlayerPrefs.SetString("PreviousGameScene", "SceneGame_2D");
        PlayerPrefs.DeleteKey("CurrentLevel");
        SceneManager.LoadScene("SceneGameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SceneGame_2D");
    }
}
