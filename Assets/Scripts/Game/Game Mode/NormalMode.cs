using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMode : MonoBehaviour, GameMode
{
    private float elapseTime;

    void Start ()
    {
        gameObject.GetComponent<BoardGenerator>().Init();
        gameObject.GetComponent<ObjectSpawn>().Init();
    }

    void Update()
    {
        elapseTime += Time.deltaTime;

        if (elapseTime >= 5)
        {
            elapseTime = 0;
            //ReGenerateLevel();
        }

    }

    private void ReGenerateLevel()
    {
        gameObject.GetComponent<BoardGenerator>().DestroyLevel();
        gameObject.GetComponent<BoardGenerator>().Init();
        gameObject.GetComponent<ObjectSpawn>().Init();
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
