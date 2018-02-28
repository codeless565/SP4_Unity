using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    GameObject m_enemy;
    float Health;
    float MaxHealth;

    void Start()
    {
        m_enemy = transform.parent.parent.gameObject;
    }

    void Update()
    {
        Health = m_enemy.GetComponent<StatsBase>().Health;
        MaxHealth = m_enemy.GetComponent<StatsBase>().MaxHealth;

        GetComponent<Slider>().value = Health / MaxHealth;
    }
}
