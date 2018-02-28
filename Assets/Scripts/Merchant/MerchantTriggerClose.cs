using UnityEngine;
using UnityEngine.UI;

/* This is to Trigger the close Button in Merchant */
public class MerchantTriggerClose : MonoBehaviour
{
    static public bool m_close = false;
    void Start()
    {
        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { m_close = true; });
    }
}
