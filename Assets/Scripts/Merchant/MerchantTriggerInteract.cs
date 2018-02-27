using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Include to Trigger Interaction wiht Merchant */
public class MerchantTriggerInteract : MonoBehaviour {

    /* Used to Trigger Attack */
    static public bool _triggered;

    /* Get Merchant */


    // Use this for initialization
    void Start()
    {
        _triggered = false;

        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                
            }
            );
        }
    }
}
