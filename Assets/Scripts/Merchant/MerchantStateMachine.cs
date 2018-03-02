using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Handles the changing of states 
 * ( what condition to change to each state ) */
public class MerchantStateMachine : MonoBehaviour
{
    /* String to store states */
    public string m_state;
    private GameObject _player;

    /* Trigger Dialog */
    private bool _isinRange;
    private bool _goBackIdle = false;
    public bool IsBackIdle
    {
        get
        { return _goBackIdle; }
        set
        { _goBackIdle = value; }
    }

    /* GameObjects to Store states */
    public GameObject idle, interact, bye;
    public bool merchantClosed;

    /* Merchant Text Mesh */
    [SerializeField]
    public TextMesh m_merchantName;
    
    /* Interact and Attack Button */
    private GameObject Attack_Btn, Interact_Btn;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("MeleeHitbox");

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

        /* Default Merchant Name */
        //Debug.Log("INSTART");
        m_merchantName.text = "";
        //Debug.Log("IN START :" + m_merchantName.text);
    }

    /* Every Frame, Update what Merchant is doing */
    void Update ()
    {
        /* If Trigger Dialougue, change State to INTERACT */
        if (_isinRange && GetTriggerForInteraction() && m_state == "IDLE")
        {
            m_state = "INTERACT";

//#if UNITY_ANDROID || UNITY_IPHONE
//            MerchantTriggerInteract.m_interact = false;
//#endif

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
        if (m_state == "GOODBYE" && _goBackIdle)
        {
            m_state = "IDLE";
            _goBackIdle = false;

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

        //m_merchantName.text = "Merchant";
        //Debug.Log("Merchant name : " + m_merchantName.text);

        //* When Near for Interaction, player is not able to attack/move as it will focus on interaction */
        //_player.GetComponent<Player2D_Attack>().Interact = true;

//#if UNITY_ANDROID || UNITY_IPHONE
        /* Change Attack Button to Interact Button */
        Interact_Btn.SetActive(true);
        Attack_Btn.SetActive(false);
//#endif
    }

    /* Response to Player Staying in trigger box of Merchant */
    public void OnTriggerStay2D(Collider2D other)
    {
        /* Update every frame to see for Interact Btn pressed */
        _isinRange = true;
        Debug.Log("ON stay : " + _isinRange);

    }

    /* When Player exits the area of Interaction */
    void OnTriggerExit2D(Collider2D other)
    {
        //m_merchantName.text = "";
        //Debug.Log("Merchant name : " + m_merchantName.text);
        _isinRange = false;
        Debug.Log("ON exit : " + _isinRange);
        //_player.GetComponent<Player2D_Attack>().Interact = true;

//#if UNITY_ANDROID || UNITY_IPHONE
        /* Change Attack Button to Interact Button */
        Attack_Btn.SetActive(true);
        Interact_Btn.SetActive(false);
//#endif
    }

    /* Get Trigger for Merchant Interaction */
    private bool GetTriggerForInteraction()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#elif UNITY_ANDROID || UNITY_IPHONE
        return MerchantTriggerInteract.m_interact;
#else
        return false;
#endif
    }
}
