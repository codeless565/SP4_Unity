﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNonDeflect : MonoBehaviour, Projectile
{
    GameObject m_owner;

    Vector3 m_dir;
    float m_damage;
    float m_speed;

    // Use this for initialization
    public void Init(GameObject _owner, Vector3 _dir, float _damage, float _speed = 10)
    {
        m_owner = _owner;
        m_dir = Vector3.Normalize(_dir);
        m_damage = _damage * 10;
        m_speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_dir * m_speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == m_owner)
            return;

        if (other.GetComponent<CollisionPlayerAttack>() != null) // Does not get deflected or destoryed by player's attack
        {
            if (m_damage <= other.GetComponentInParent<StatsBase>().Attack) // if player's damage is higher, damage is converted
                m_damage = other.GetComponentInParent<StatsBase>().Attack;

            return;
        }

        if (other.GetComponent<StatsBase>() != null)
        {
            GameObject t_effect = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ShadowBlastEffect;

            // Create blast effect
            GameObject tempEffect = Instantiate(t_effect, other.transform.position, Quaternion.identity) as GameObject;

            //Deal damage if other has stats
            if (other.GetComponent<StatsBase>() != null)
                other.GetComponent<StatsBase>().Health -= m_damage;
        }

        Destroy(gameObject); // if it hits wall also destroy
    }
}