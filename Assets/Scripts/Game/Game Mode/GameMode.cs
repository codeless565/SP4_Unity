using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameMode
{
    void GameStart();
    void GamePause();
    void GameClear();
    void GameOver();
    void RestartGame();

    int CurrentFloor
    { get; }

    bool Pause
    { get; set; }
}

