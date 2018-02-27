using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Include to Trigger Interaction with Things (eg. Merchant) */
public class MerchantTriggerInteract : MonoBehaviour
{
    /* Get Merchant */
    static public bool m_interact = false;

    // Use this for initialization
    void Start()
    {
        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                m_interact = true;
            }
            );
        }
    }
}
