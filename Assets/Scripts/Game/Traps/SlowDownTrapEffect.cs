using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTrapEffect : MonoBehaviour {

    private float m_multiplier = 0.2f;
    private float m_duration = 5;
    private float m_elapseTime;
    private float m_originalSpeed;
    private float m_slowedSpeed;

    // Use this for initialization
    void Start()
    {
        m_elapseTime = m_duration;
        m_originalSpeed = GetComponent<StatsBase>().MoveSpeed;
        m_slowedSpeed = m_originalSpeed * m_multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        m_elapseTime -= Time.deltaTime;

        if (m_elapseTime <= 0)
        {
            RemoveEffect();
            return;
        }

        GetComponent<StatsBase>().MoveSpeed = m_slowedSpeed;
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

    public void RemoveEffect()
    {
        GetComponent<StatsBase>().MoveSpeed = m_originalSpeed;
        Destroy(this);
    }
}
