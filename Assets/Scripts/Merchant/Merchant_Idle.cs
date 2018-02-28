﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* When Merchant is in Idle, Check for TriggerStay + Button in State Machine
   When Checked, Changed to INTERACT */
public class Merchant_Idle : MonoBehaviour
{
    /* Dialouge With Merchant */
    private TextBoxManager _theManager;

    /* Getting the Buttons */
    private GameObject m_buttons;

    // Use this for initialization
    void Start()
    {
        /* Getting the Text Box Renderer */
        _theManager = GameObject.FindGameObjectWithTag("GameScript").GetComponent<TextBoxManager>();
        _theManager.DisableTextBox();

        /* Buttons */
        m_buttons = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Merchant_Interaction;
        m_buttons.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<MerchantStateMachine>().m_state != "IDLE")
            return;

        //Debug.Log(GetComponentInParent<MerchantStateMachine>().m_state);
    }
}
