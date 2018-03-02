using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Trigger Sprint via Button Press */
public class TriggerSprint : MonoBehaviour
{
    static public bool m_sprint_btn = false;
    void Start()
    {
        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { m_sprint_btn = true; });
    }
}
