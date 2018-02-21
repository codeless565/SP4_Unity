using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public void RestartGameSelected()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousGameScene"));
    }

    public void MainMenuSelected()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    public void QuitGameSelected()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }
}
