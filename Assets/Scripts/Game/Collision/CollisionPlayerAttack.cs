using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayerAttack : MonoBehaviour {

    GameObject m_owner;
    float m_damage;

    public void Init(float _damage, GameObject _owner)
    {
        m_owner = _owner;
        m_damage = _damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SkeletonEnemyManager>() != null || other.GetComponent<ProjectileObject>() != null)
        {
            //Get the Particle effect
            GameObject t_effect = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().SwordClashEffect;

            // Calculate the rotation from player to enemy
            float effectArc = t_effect.GetComponent<ParticleSystem>().shape.arc * 0.5f;
            Vector3 rotationVec = other.transform.position - m_owner.transform.position;
            float Angle = Mathf.Atan2(rotationVec.y, rotationVec.x) * Mathf.Rad2Deg;

            // Create sword clash effect
            GameObject tempEffect = Instantiate(t_effect, other.transform.position, Quaternion.Euler(0, 0, Angle - effectArc)) as GameObject;

            if (other.GetComponent<StatsBase>() != null)
            {// Deals damage to the enemy
                other.GetComponent<StatsBase>().Health -= Calculator.Instance.CalculateDamage(m_damage, other.GetComponent<StatsBase>().Defense);
            }
        }
    }
}
