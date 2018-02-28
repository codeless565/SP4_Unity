using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* When INTERACT, Render Display UI
   - Text Box
   - Shop
   - Close */
public class Merchant_Interact : MonoBehaviour
{
    /* Dialouge With Merchant */
    private TextBoxManager _theManager;
	public TextAsset theText;
	public int startLine;
	public int endLine;

    /* Getting the Buttons */
    private GameObject m_buttons;
    
    // Use this for initialization
    void Start ()
    {
        /* Getting the Text Box Renderer */
        _theManager = GameObject.FindGameObjectWithTag("GameScript").GetComponent<TextBoxManager>();

        /* Buttons */
        m_buttons = GameObject.FindGameObjectWithTag("Holder").GetComponent<MerchantHolder>().Merchant_Interaction;
    }

    // Update is called once per frame
    void Update ()
    {
        if (GetComponentInParent<MerchantStateMachine>().m_state != "INTERACT")
            return;
		
		_theManager.ReloadScript(theText);
		_theManager.currentLine = startLine;
		_theManager.endAtLine = endLine;
        _theManager.EnableTextBox();
        m_buttons.SetActive(true);
    }
}
