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
    [SerializeField]
    private GameObject m_buttons;

    /* Limit Update Once */
    private bool check;

    // Use this for initialization
    void Start ()
    {
        /* Getting the Text Box Renderer */
        _theManager = GameObject.FindGameObjectWithTag("GameScript").GetComponent<TextBoxManager>();
        check = false;

        /* Buttons */
        m_buttons.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponentInParent<MerchantStateMachine>().m_state != "INTERACT")
            return;

        if (check)
            return;
        
        _theManager.EnableTextBox();
        m_buttons.SetActive(true);
        check = true;
            
        //Debug.Log(GetComponentInParent<MerchantStateMachine>().m_state);
    }


}
