using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the changing of states 
 * ( what condition to change to each state ) */
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
        //Attack_Btn = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Attack_Btn;
        //Attack_Btn.SetActive(true);

        //Interact_Btn = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Interact_Btn;
        //Interact_Btn.SetActive(false);
    }

    /* Every Frame, Update what Merchant is doing */
    void Update ()
    {
        /* If Trigger Dialougue, change State to INTERACT */
        if (_isinRange && MerchantTriggerInteract.m_interact)
        {
            m_state = "INTERACT";
            MerchantTriggerInteract.m_interact = false;
            _player.GetComponent<Player2D_Manager>().canMove = false;

            interact.SetActive(true);
            idle.SetActive(false);
        }

		/* When not near the Merchant, back to idle */
		if (!_isinRange)
		{
            m_state = "IDLE";
            interact.SetActive(false);
			idle.SetActive(true);
		}

        /* if INTERACT and Close Button, change State to GOODBYE */
        if (MerchantTriggerClose.m_close && m_state == "INTERACT")
        {
            m_state = "GOODBYE";
			MerchantTriggerClose.m_close = false;

            interact.SetActive(false);
            bye.SetActive(true);
        }
        
        /* If GOODBYE and Duration , change State to IDLE */
        if (m_state == "GOODBYE" && GetComponentInChildren<Merchant_GoodBye>().isBackIdle)
        {
            m_state = "IDLE";
            GetComponentInChildren<Merchant_GoodBye>().isBackIdle = false;
            _player.GetComponent<Player2D_Manager>().canMove = true;

            bye.SetActive(false);
            idle.SetActive(true);
        }
    }

    /* Run only Once */
    private void OnTriggerEnter2D(Collider2D other)
    {
        /* Other then player, dont check */
        if (other.GetComponent<Player2D_Manager>() == null)
            return;

        //* When Near for Interaction, player is not able to attack/move as it will focus on interaction */
        //_player.GetComponentInChildren<Player2D_Attack>().Interact = true;

        /* Change Attack Button to Interact Button */
        //Interact_Btn.SetActive(true);
        //Attack_Btn.SetActive(false);
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
        _player.GetComponentInChildren<Player2D_Attack>().Interact = false;
		_isinRange = false;

        //Attack_Btn.SetActive(true);
        //Interact_Btn.SetActive(false);
    }
}
