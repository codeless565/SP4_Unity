using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrapEffect : MonoBehaviour {

    private float m_damage = 1;
    private float m_duration = 5;
    private float m_elapseTime;

	// Use this for initialization
	void Start ()
    {
        m_elapseTime = m_duration;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_elapseTime -= Time.deltaTime;

        if (m_elapseTime <= 0)
            Destroy(this);

        GetComponent<StatsBase>().Health -= m_damage * Time.deltaTime;
    }

    public void SetDamage(float _value)
    {
        m_damage = _value;
    }

    public void SetDuration(float _value)
    {
        m_duration = _value;
        m_elapseTime = m_duration;
    }

    public void ResetTimer()
    {
        m_elapseTime = m_duration;
    }

}
