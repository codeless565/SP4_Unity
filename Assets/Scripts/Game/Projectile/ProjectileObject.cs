using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileObject : MonoBehaviour, Projectile
{
    GameObject m_owner;

    Vector3 m_dir;
    float m_damage;
    float m_speed;
    bool m_reflected;

	// Use this for initialization
	public void Init (GameObject _owner, Vector3 _dir, float _damage, float _speed = 10)
    {
        m_owner = _owner;
        m_dir = Vector3.Normalize(_dir);
//        Debug.Log(m_dir);
        m_damage = _damage;
        m_speed = _speed;
        m_reflected = false;
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
            GameObject t_effect = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().ShadowBlastEffect;

            // Create blast effect
            GameObject tempEffect = Instantiate(t_effect, transform.position, Quaternion.identity) as GameObject;

            other.GetComponent<StatsBase>().Health -= m_damage;

            if (other.GetComponent<Player2D_StatsHolder>() != null)
                if (other.GetComponent<Player2D_StatsHolder>().Health <= 0)
                    PlayerPrefs.SetString("KilledBy", "Projectile");
            
            Destroy(gameObject); // if it hits wall also destroy
       }

        if (other.GetComponent<CollisionPlayerAttack>() != null && !m_reflected)
        {
            m_reflected = true;
            m_owner = other.transform.parent.gameObject;
            m_dir = other.transform.up;     //Reverse the direction of the ball
            Debug.Log("Changed Dir: " + m_dir);
            m_damage = other.GetComponentInParent<StatsBase>().Attack;
            GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().PlayerCounterBall;
        }
    }
}
