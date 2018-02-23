using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Printing Stats of Player */
public class Player2D_StatsMenu : MonoBehaviour
{
    /* Menu for Player Stats */
    [SerializeField]
    private GameObject player_StatsMenu; // main prefab
    private GameObject Menu;

    /* Text containing the Stats */
    [SerializeField]
    private Text player_Stats; // main prefab

    /* Player */
    [SerializeField]
    private Player2D_StatsHolder player;
    private Text tempText;

    /* Variables */
    private Vector3 player_MenuScale;
    private bool isOpen;

    // Use this for initialization
    void Start ()
    {
        /* For Mobile, tap button to open stats menu*/
        //if (gameObject.GetComponent<Button>())
        //{
        //    gameObject.GetComponent<Button>().onClick.AddListener(delegate { RenderStatsMenu(); });
        //}

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_StatsHolder>();
        isOpen = false;
        player_MenuScale = new Vector3(1.0F, 1.0F, 0.0F);
    }

    // Update is called once per frame
    static bool isPressed = false;
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isPressed)
        {
            isPressed = true;

            /* Create a Stats Menu */
            Menu = Instantiate(player_StatsMenu, transform.position, transform.rotation);
            Menu.transform.SetParent(GameObject.FindGameObjectWithTag("StatsMenu").transform);
            Menu.transform.localScale = new Vector3(1.0F, 0.0F, 0.0F);
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isPressed)
        {
            isPressed = false;
            isOpen = false;
            
            if (Menu != null)
                Destroy(Menu);

            if (tempText != null)
                Destroy(tempText);
        }

        /* Open Stats Menu */
        if (isOpen)
        {
            // If local scale of Menu is not the extended scale
            if (Menu.transform.localScale.y < player_MenuScale.y)
            {
                Menu.transform.localScale += new Vector3(0f, player_MenuScale.y, 0f) * Time.deltaTime * 3f;
            }
            else
            {
                isOpen = false;

                /* Create the Stats of Player */
                tempText = Instantiate(player_Stats, transform.position, transform.rotation);
                tempText.transform.SetParent(GameObject.FindGameObjectWithTag("StatsMenu").transform);
                
            }
        }

        PrintData();
    }

    /* Print the data */
    private void PrintData()
    {
        tempText.text = "\n\n\nLevel : " + player.Level.ToString() + " \n"
                       + "Health : " + player.Health.ToString() + " / " + player.MaxHealth.ToString() + " \n"
                       + "Stamina : " + player.Stamina.ToString() + " / " + player.MaxStamina.ToString() + " \n"
                       + "Attack : " + player.Attack.ToString() + " \n"
                       + "Defense : " + player.Defense.ToString() + " \n"
                       + "MoveSpeed : " + player.MoveSpeed.ToString() + " \n";
    }
}