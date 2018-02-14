using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    void Start ()
    {
    }

    void Update()
    {
    }

    // Interface Functions // 

    public void GameClear()
    {
        throw new NotImplementedException();
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
