using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Check for Player - Merchant Interaction */
/* Attached to Merchant */
public class CheckMerchantCollision : MonoBehaviour
{
    /* Trigger Dialog */
    private bool _triggeredDialog;
    private bool _isinRange;

    /* Dialouge With Merchant */
    private TextBoxManager _theManager;
    private GameObject _player;

    void Start()
    {
        _triggeredDialog = false;
        _isinRange = false;

        /* Getting the Text Box Renderer */
        _theManager = GameObject.FindGameObjectWithTag("GameScript").GetComponent<TextBoxManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    /* Response to Player entering trigger box of Merchant */
    public void OnTriggerStay2D(Collider2D other)
    {
        /* Other then player, dont check */
        if (other.GetComponent<Player2D_Manager>() == null)
            return;

        /* When Near for Interaction, player is not able to attack/move as it will focus on interaction */
        _player.GetComponentInChildren<Player2D_Attack>().Interact = true;

        /* Dialouge to open */
        _isinRange = true;
    }

    /* When Player exits the area of Interaction */
    void OnTriggerExit2D(Collider2D other)
    {
        _player.GetComponentInChildren<Player2D_Attack>().Interact = false;
    }

    // Run once every frame 
    void Update()
    {
        /* Render the TextBox */
        if (_triggeredDialog)
        {
            _theManager.isActive = true;
            _player.GetComponent<Player2D_Manager>().canMove = false;
        }
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
        _theManager.isActive = false;
        _theManager.textBox.SetActive(false);
    }
}
