using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Effects when Collided With Confusion Trap */
public class ConfusedEffect : MonoBehaviour
{
    /* Duration of Effect */
    private float m_fDuration;

	// Use this for initialization
	void Start ()
    {
        m_fDuration = 5.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /* Set the Direction of the movement to be opposite */
        CollisionConfusionTrap.m_confusedModifier = -1;

        m_fDuration -= Time.deltaTime;
        /* Duration Up, Script will be removed */
        if (m_fDuration < 0)
        {
            CollisionConfusionTrap.m_confusedModifier = 1;
            Destroy(this);
        }
    }

    /* Set Duration */
    public void SetDuration(float _value)
    {
        m_fDuration = _value;
    }
    public void Resets()
    {
        m_fDuration = 5.0f;
    }
}
