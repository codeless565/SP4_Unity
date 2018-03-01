using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    GameObject m_owner;

    Vector3 m_dir;
    float m_damage;
    float m_speed;

	// Use this for initialization
	public void Init (GameObject _owner, Vector3 _dir, float _damage, float _speed = 10)
    {
        m_owner = _owner;
        m_dir = Vector3.Normalize(_dir);
//        Debug.Log(m_dir);
        m_damage = _damage;
        m_speed = _speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += m_dir * m_speed * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == m_owner)
            return;

        if (other.GetComponent<StatsBase>() != null)
        {
            other.GetComponent<StatsBase>().Health -= m_damage;
        }

        Destroy(gameObject); // if it hits wall also destroy
        //can instatiate particles here
    }
}
