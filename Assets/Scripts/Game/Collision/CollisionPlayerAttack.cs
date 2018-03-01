using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayerAttack : MonoBehaviour {

    float m_damage;

    public void Init(float _damage)
    {
        m_damage = _damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BossStatsManager>() != null ||
           other.GetComponent<SkeletonEnemyManager>() != null)
        {
            // Deals damage to the enemy
            other.GetComponent<StatsBase>().Health -= Calculator.Instance.CalculateDamage(m_damage, other.GetComponent<StatsBase>().Defense);
        }
    }
}
