using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    void Start ()
    {
        gameObject.GetComponent<BoardGenerator>().Init();
        gameObject.GetComponent<PlayerSpawn>().Init();
    }

    void Update()
    {
    }

    // Interface Functions // 

    public void GameClear()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    public void RestartGame()
    {
        throw new NotImplementedException();
    }
}
