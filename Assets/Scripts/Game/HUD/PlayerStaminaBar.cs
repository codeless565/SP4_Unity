﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    /* This is a Script for Slider */

    GameObject m_player;

    float m_currvalue;
    float m_movingvalue;
    float m_newvalue;

    public void Init(GameObject _player)
    {
        m_player = _player;
        m_movingvalue = m_player.GetComponent<StatsBase>().Stamina;
        m_newvalue = m_movingvalue;
    }

    void Update()
    {
        m_currvalue = m_player.GetComponent<StatsBase>().Stamina;

        //Deduct the hp overtime for animation
        if (m_movingvalue != m_newvalue)
        {
            if (m_movingvalue >= m_newvalue)
            {
                m_movingvalue -= m_player.GetComponent<StatsBase>().MaxStamina * Time.deltaTime;
                if (m_movingvalue <= m_newvalue)
                    m_movingvalue = m_newvalue;
            }
            else if (m_movingvalue <= m_newvalue)
            {
                m_movingvalue += m_player.GetComponent<StatsBase>().MaxStamina * Time.deltaTime;
                if (m_movingvalue >= m_newvalue)
                    m_movingvalue = m_newvalue;
            }
        }

        // Update HP Bar
        GetComponent<Slider>().value = m_movingvalue / m_player.GetComponent<StatsBase>().MaxStamina;
    }

    void LateUpdate()
    {
        if (m_player.GetComponent<StatsBase>().Stamina <= m_currvalue)
        {
            m_newvalue = m_player.GetComponent<StatsBase>().Stamina;
        }
    }
}
