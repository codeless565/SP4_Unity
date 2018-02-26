using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameMode
{
    void GameStart();
    void GameClear();
    void GameOver();
    void RestartGame();
}

