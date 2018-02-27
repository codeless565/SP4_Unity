using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour
{
    int currentLvl;
    GameObject m_player;
    Text m_level;

    public void Init(GameObject _player)
    {
        m_player = _player;
        m_level = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (currentLvl != m_player.GetComponent<StatsBase>().Level)
            UpdateLevel(m_player.GetComponent<StatsBase>().Level);
    }

    public void UpdateLevel(int _level)
    {
        currentLvl = _level;
        m_level.text = "LV " + _level.ToString();
    }

    public void UpdateStatusAliments(string _status)
    {
        switch(_status)
        {
            case "slow":
                break;
            case "poison":
                break;
        }
    }
}
