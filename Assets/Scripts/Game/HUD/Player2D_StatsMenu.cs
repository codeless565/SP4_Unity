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
    [SerializeField]
    private GameObject EQDisplay_Menu;
    private Vector3 menu_scale;

    /* Text */
    [SerializeField]
    private Text m_playerStats;
    private GameObject player;

    private GameObject SpritePrefab;
    GameObject[] EQDisplayLayout;


    // Use this for initialization
    public void Init()
    {
        Stats_Reset();
        menu_scale = new Vector3(1, 1, 1f); // save local scale of menu

        player = GameObject.FindGameObjectWithTag("Player");

        EQDisplayLayout = new GameObject[5];
        SpritePrefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().BorderPrefab;

        Vector2 AnchorMin = new Vector2(0.5f, 1);
        Vector2 AnchorMax = new Vector2(0.5f, 1);

        for (int j = 0; j < EQDisplayLayout.Length; ++j)
        {
            EQDisplayLayout[j] = Instantiate(SpritePrefab, EQDisplay_Menu.transform);
            EQDisplayLayout[j].GetComponent<RectTransform>().anchorMin = AnchorMin;
            EQDisplayLayout[j].GetComponent<RectTransform>().anchorMax = AnchorMax;
            if (j == EQDisplayLayout.Length - 1)
            {
                EQDisplayLayout[j].GetComponent<RectTransform>().anchoredPosition = new Vector3(150, (j * 0.5f - 1) * -150);
            }
            else
            {
                EQDisplayLayout[j].GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, (j * -150));
            }
        }
        

    }

    /* Reset Stats Animation */
    private void Stats_Reset()
    {
        m_isOpen = false;

        /* Menu */
        Stats_Menu.GetComponent<RectTransform>().localScale = new Vector3(Stats_Menu.transform.localScale.x, 0.0f, 1.0f); // set menu scale
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
            if (Stats_Menu.GetComponent<RectTransform>().localScale.y < menu_scale.y)
            {
                Stats_Menu.GetComponent<RectTransform>().localScale += new Vector3(0f, menu_scale.y, 0f) * Time.deltaTime;
                if (Stats_Menu.GetComponent<RectTransform>().localScale.y >= menu_scale.y)
                    Stats_Menu.GetComponent<RectTransform>().localScale = menu_scale;
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
        m_playerStats.text = "Level : " + player.GetComponent<Player2D_StatsHolder>().Level.ToString("0") + " \n"
                           + "EXP : " + player.GetComponent<Player2D_StatsHolder>().EXP.ToString("0") + " / " + player.GetComponent<Player2D_StatsHolder>().MaxEXP.ToString("0") + " \n"
                           + "Health : " + player.GetComponent<Player2D_StatsHolder>().Health.ToString("0") + " / " + player.GetComponent<Player2D_StatsHolder>().MaxHealth.ToString("0") + " \n"
                           + "Stamina : " + player.GetComponent<Player2D_StatsHolder>().Stamina.ToString("0") + " / " + player.GetComponent<Player2D_StatsHolder>().MaxStamina.ToString("0") + " \n"
                           + "Attack : " + player.GetComponent<Player2D_StatsHolder>().Attack.ToString("0") + " \n"
                           + "Defense : " + player.GetComponent<Player2D_StatsHolder>().Defense.ToString("0") + " \n"
                           + "MoveSpeed : " + player.GetComponent<Player2D_StatsHolder>().MoveSpeed.ToString("0") + " \n";
    }

    /* Open Stats Menu */
    public void OpenStatsMenu()
    {
        m_isOpen = true;
        DisplayEquipments();
        Stats_Update();
    }

    /* Close the Stats Menu */
    public void CloseStatsMenu()
    {
        Stats_Reset();
    }

    public void DisplayEquipments()
    {
        for (int i = 0; i < player.GetComponent<Player2D_Manager>().getEQList().Length; ++i)
        {
            if (player.GetComponent<Player2D_Manager>().getEQList()[i] == null)
                EQDisplayLayout[i].GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
            else
                EQDisplayLayout[i].GetComponent<Image>().sprite = player.GetComponent<Player2D_Manager>().getEQList()[i].ItemImage;
        }
    }

}
