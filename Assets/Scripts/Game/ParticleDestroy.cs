using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour 
{
    float m_Duration;
	// Use this for initialization
	void Start ()
    {
        if (GetComponent<ParticleSystem>() == null)
        {
            if (transform.parent.GetComponent<ParticleSystem>() != null)
            {
                m_Duration = transform.parent.GetComponent<ParticleSystem>().main.duration;
                return;
            }
        }

        m_Duration = GetComponent<ParticleSystem>().main.duration;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Duration -= Time.deltaTime;
        if (m_Duration <= 0)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent);
                return;
            }
            Destroy(gameObject);
        }
	}
}
