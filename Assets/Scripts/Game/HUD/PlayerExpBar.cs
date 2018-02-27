using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour
{
    /* This is a Script for Slider */

    GameObject m_player;

    public void Init(GameObject _player)
    {
        m_player = _player;
    }

    void Update()
    {
        // Update HP Bar
        GetComponent<Slider>().value = m_player.GetComponent<StatsBase>().EXP / m_player.GetComponent<StatsBase>().MaxEXP;
    }
}

