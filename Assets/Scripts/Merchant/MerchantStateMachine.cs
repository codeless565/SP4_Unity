using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the changing of states 
 * ( what condition to change to each state ) */
public class MerchantStateMachine : MonoBehaviour
{
    /* states 
      - INTERACTING
      - GOODBYE 
      - IDLE
    */

    /* String to store states */
    public string m_state;
    private GameObject _player;

    /* Trigger Dialog */
    private bool _triggeredDialog;
    private bool _isinRange;
    // and the many cancerous bools to go... kmn

    /* GameObjects to Store states */
    public GameObject idle, interact, bye;

    void Start()
    {
        /* Default state */
        m_state = "IDLE";

        _player = GameObject.FindGameObjectWithTag("Player");

        idle.SetActive(true);
        interact.SetActive(false);
        bye.SetActive(false);
    }

    /* Every Frame, Update what Merchant is doing */
    void Update ()
    {
        /* If Trigger Dialougue, change State to INTERACT */
        if (_triggeredDialog)
        {
            m_state = "INTERACT";
            _player.GetComponent<Player2D_Manager>().canMove = false;
            interact.SetActive(true);
            idle.SetActive(false);
        }
        else
        {
            m_state = "IDLE";
            _player.GetComponent<Player2D_Manager>().canMove = true;
            idle.SetActive(true);
            interact.SetActive(false);
        }


        /* if INTERACT and Close Button, change State to GOODBYE */


        /* If GOODBYE and tap textbox, change State to IDLE */

    }

    /* Response to Player entering trigger box of Merchant */
    public void OnTriggerStay2D(Collider2D other)
    {
        /* Other then player, dont check */
        if (other.GetComponent<Player2D_Manager>() == null)
            return;

        /* For now, when near test for changing states */
        //m_state = "INTERACT";
        
        _isinRange = true;

        ///* When Near for Interaction, player is not able to attack/move as it will focus on interaction */
        _player.GetComponentInChildren<Player2D_Attack>().Interact = true;
    }

    /* When Player exits the area of Interaction */
    void OnTriggerExit2D(Collider2D other)
    {
        _player.GetComponentInChildren<Player2D_Attack>().Interact = false;
        //Reset_Merchant();

        _triggeredDialog = false;
    }

    /* Set Trigger Dialog */
    public void SetTriggerDialougeTrue()
    {
        if (_isinRange)
            _triggeredDialog = true;
    }
    
    /* Close Button - Reset Merchant */
    public void Reset_Merchant()
    {
        _player.GetComponent<Player2D_Manager>().canMove = true;
        _isinRange = false;
        _triggeredDialog = false;
    }
}
