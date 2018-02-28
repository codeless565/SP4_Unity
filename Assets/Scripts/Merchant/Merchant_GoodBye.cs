using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* When Merchant is in GoodBye, Render GoodBye Text
   When Press Text Box, Changed to IDLE */
public class Merchant_GoodBye : MonoBehaviour
{
    /* Dialouge With Merchant */
    private TextBoxManager _theManager;
    public TextAsset theText;
    public int startLine;
    public int endLine;

	/* Getting the Buttons */
	private GameObject m_buttons;

    private float m_timer = 3.0f;

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
        if (GetComponentInParent<MerchantStateMachine>().m_state != "GOODBYE")
            return;
		m_buttons.SetActive(false);

		m_timer -= Time.deltaTime;
		if (m_timer >= 0.0f)
		{
			_theManager.ReloadScript(theText);
			_theManager.currentLine = startLine;
			_theManager.endAtLine = endLine;
			_theManager.EnableTextBox();
		}
		else 
		{
			m_timer = 3.0f;
            GetComponentInParent<MerchantStateMachine>().IsBackIdle = true;
		}
    }

}
