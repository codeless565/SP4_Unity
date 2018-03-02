using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    public GameObject BossHPBarPrefab;
    public GameObject HUD;
    GameObject m_BossObject;

	// Use this for initialization
	public void Init(GameObject _bossObject)
    {
        m_BossObject = _bossObject;

        BossHPBarPrefab = Instantiate(BossHPBarPrefab, HUD.transform) as GameObject;
        BossHPBarPrefab.GetComponent<PlayerHealthBar>().Init(m_BossObject);
	}
}
