using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public GameObject Profile;
    public GameObject HealthBar;
    public GameObject StaminaBar;
    public GameObject ExperienceBar;

    private GameObject m_player;

	// Use this for initialization
	public void Init ()
    {
        /* This Script is to be called after the spawning of the player */
        m_player = GameObject.FindGameObjectWithTag("Player");

        // Initialize all HUD's Script
        Profile      .GetComponent<PlayerProfile>()   .Init(m_player);
        HealthBar    .GetComponent<PlayerHealthBar>() .Init(m_player);
        StaminaBar   .GetComponent<PlayerStaminaBar>().Init(m_player);
        ExperienceBar.GetComponent<PlayerExpBar>()    .Init(m_player);
    }
}
