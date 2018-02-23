using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Check for Player - Merchant Interaction */
/* Attached to Merchant */
public class CheckMerchantCollision : MonoBehaviour, CollisionBase
{
    /* Dialog with Merchant */
    private TextBoxManager _textboxmanager;
    [SerializeField]
    private TextAsset _textAssets;
    public int startLine;
    public int endLine;

    /* Trigger Dialog */
    private bool _triggeredDialog;
    public bool pauseBox;
    
    void Start()
    {
        /* Setting Up Dialog Box */
        _textboxmanager = FindObjectOfType<TextBoxManager>();
        _textboxmanager.ReloadScript(_textAssets);
        _textboxmanager.currentLine = startLine;
        _textboxmanager.endAtLine = endLine;
        _textboxmanager.EnableTextBox();
        
        pauseBox = false;
    }

    /* Response to Player entering trigger box of Mercahnt */
    public void CollisionResponse(string _tag)
    {
        Debug.Log("Within range of merchant interaction ");

        /* When Near for Interaction, player is not able to attack as it will focus on interaction */
        GameObject.FindGameObjectWithTag("MeleePoint").GetComponent<Player2D_Attack>().Interact = true;
        _triggeredDialog = true;
    }

    /* When Player exits the area of Interaction */
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject.FindGameObjectWithTag("MeleePoint").GetComponent<Player2D_Attack>().Interact = false;
    }

    // Run once every frame 
    void Update()
    {
        if(_triggeredDialog)
        {
            
        }
    }
}
