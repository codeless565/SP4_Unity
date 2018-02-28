using UnityEngine;
using UnityEngine.UI;

/* Include to Trigger Interaction with Things (eg. Merchant) */
public class MerchantTriggerInteract : MonoBehaviour
{
    static public bool m_interact = false;
    void Start()
    {
        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
            gameObject.GetComponent<Button>().onClick.AddListener( delegate { m_interact = true; } );
    }
}
