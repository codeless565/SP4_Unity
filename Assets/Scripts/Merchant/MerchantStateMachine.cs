using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the changing of states 
 * ( what condition to change to each state ) 
 * And other things like a holder ? */
public class MerchantStateMachine : MonoBehaviour
{
    /* String to store states */
    public string m_state;
    private GameObject _player;

    /* Trigger Dialog */
    private bool _triggeredDialog;
    private bool _isinRange;

    /* Interact and Attack Button */
    private GameObject Attack_Btn, Interact_Btn;

    /* GameObjects to Store states */
    public GameObject idle, interact, bye;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        /* Default state */
        m_state = "IDLE";
        idle.SetActive(true);
        interact.SetActive(false);
        bye.SetActive(false);

        /* Default Btn Layout */
        Attack_Btn = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Attack_Btn;
        Attack_Btn.SetActive(true);

        Interact_Btn = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Interact_Btn;
        Interact_Btn.SetActive(false);
    }

    /* Every Frame, Update what Merchant is doing */
    void Update ()
    {
        /* If Trigger Dialougue, change State to INTERACT */
        if (_isinRange && MerchantTriggerInteract.m_interact)
        {
            m_state = "INTERACT";
            //_player.GetComponent<Player2D_Manager>().canMove = false;
            interact.SetActive(true);
            idle.SetActive(false);
        }

        /* if INTERACT and Close Button, change State to GOODBYE */


        /* If GOODBYE and tap textbox, change State to IDLE */
        //Debug.Log("Attack : " + Attack_Btn.activeSelf);
        //Debug.Log("Interact : " + Interact_Btn.activeSelf);
    }

    /* Run only Once */
    private void OnTriggerEnter2D(Collider2D other)
    {
        /* Other then player, dont check */
        if (other.GetComponent<Player2D_Manager>() == null)
            return;

        //* When Near for Interaction, player is not able to attack/move as it will focus on interaction */
        _player.GetComponentInChildren<Player2D_Attack>().Interact = true;

        /* For now, when near test for changing states */
        //m_state = "INTERACT";

        /* Change Attack Button to Interact Button */
        Interact_Btn.SetActive(true);
        Attack_Btn.SetActive(false);


    }

    /* Response to Player entering trigger box of Merchant */
    public void OnTriggerStay2D(Collider2D other)
    {
        /* Update every frame to see for Interact Btn pressed */
        _isinRange = true;

        
    }

    /* When Player exits the area of Interaction */
    void OnTriggerExit2D(Collider2D other)
    {
        m_state = "IDLE";
        _player.GetComponentInChildren<Player2D_Attack>().Interact = false;
        Reset_Merchant();

        Attack_Btn.SetActive(true);
        Interact_Btn.SetActive(false);
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
        //_player.GetComponent<Player2D_Manager>().canMove = true;
        _isinRange = false;
        _triggeredDialog = false;
    }
}
