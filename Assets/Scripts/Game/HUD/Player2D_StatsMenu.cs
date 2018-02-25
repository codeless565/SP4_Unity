using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* When Button with Player icon is tapped, Open Stats Menu */
public class Player2D_StatsMenu : MonoBehaviour
{
    /* Open/ Close Stats Menu */
    private bool m_isOpen;

    /* Menu to open */
    [SerializeField]
    private GameObject Stats_Menu;
    private Vector3 menu_scale;

    /* Text */
    [SerializeField]
    private Text m_playerStats;
    private Player2D_StatsHolder player;

	// Use this for initialization
	private void Start ()
    {
        Stats_Reset();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
    }

    /* Reset Stats Animation */
    private void Stats_Reset()
    {
        m_isOpen = false;

        /* Menu */
        menu_scale = new Vector3(Stats_Menu.transform.localScale.x, Stats_Menu.transform.localScale.y, 0f); // save local scale of menu
        Stats_Menu.transform.localScale = new Vector3(Stats_Menu.transform.localScale.x, 0.0F, 1.0F); // set menu scale
        Stats_Menu.SetActive(false);

        /* Text */
        m_playerStats.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update ()
    {
        Stats_Update();
        PrintData();
    }

    /* Update Stats Animation */
    private void Stats_Update()
    {
        if (m_isOpen)
        {
            /* Render Stats Menu */
            Stats_Menu.SetActive(true);

            // If scale.y of Skill Screen is not local scale
            if (Stats_Menu.transform.localScale.y < menu_scale.y)
            {
                Stats_Menu.transform.localScale += new Vector3(0f, menu_scale.y, 0f) * Time.deltaTime * 2f;
            }
            else
            {
                /* Stop Animation */
                m_isOpen = false;

                /* Render Stats of Player */
                m_playerStats.gameObject.SetActive(true);
            }
        }
    }

    /* Print Data of Player */
    private void PrintData()
    {
        m_playerStats.text = "Level : " + player.Level.ToString() + " \n"
                       + "Health : " + player.Health.ToString() + " / " + player.MaxHealth.ToString() + " \n"
                       + "Stamina : " + player.Stamina.ToString() + " / " + player.MaxStamina.ToString() + " \n"
                       + "Attack : " + player.Attack.ToString() + " \n"
                       + "Defense : " + player.Defense.ToString() + " \n"
                       + "MoveSpeed : " + player.MoveSpeed.ToString() + " \n";
    }

    /* Open Stats Menu */
    public void OpenStatsMenu()
    {
        m_isOpen = true;
        Stats_Update();
    }

    /* Close the Stats Menu */
    public void CloseStatsMenu()
    {
        Stats_Reset();
    }
}
