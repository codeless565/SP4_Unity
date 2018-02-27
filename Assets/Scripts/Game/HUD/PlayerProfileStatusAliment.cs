using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileStatusAliment : MonoBehaviour
{
    public float m_BlinkSpeed;
    private float m_KillTime;

	// Use this for initialization
	public void Init(float _killTime)
    {
        m_KillTime = _killTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /* Update LifeTime */
        m_KillTime -= Time.deltaTime;
        if (m_KillTime <= 0)
            Destroy(gameObject);

        /* Update "Animation" */
        Color CurrColor = GetComponent<Image>().color;
        float newColor = CurrColor.r + m_BlinkSpeed * Time.deltaTime;
        if (newColor >= 255)
            newColor = 0;
        GetComponent<Image>().color = new Color(newColor, newColor, newColor, CurrColor.a);
	}


    public float KillTime
    {
        set { m_KillTime = value; }
    }
}
