using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Button Press for Attack */
public class Player2D_TriggerAttack : MonoBehaviour
{
    /* Used to Trigger Attack */
    static public bool _triggered;

	// Use this for initialization
	void Start ()
    {
        _triggered = false;

        /* Click Button and Trigger Attack */
        if (gameObject.GetComponent<Button>())
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { TriggerAttack(); });
        }
    }

    private void TriggerAttack()
    {
        if (!Player2D_Attack.temp)
            _triggered = true;
    }
}
