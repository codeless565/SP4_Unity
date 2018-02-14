using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attack System for the Player */
public class PlayerAttack : MonoBehaviour
{
    /* For Attacking */
    [SerializeField]
    private GameObject melee; // to detect for melee
    private float m_timer;

    GameObject temp;

    private Vector3 pos;

    void Start()
    {
        m_timer = 1.0F;
    }


    // Update is called once per frame
    void Update ()
    {
		// To fire a small bullet forward when player swings
        if (Input.GetMouseButtonDown(0) && !temp) // dont allow spam
        {
            //create a bullet
            temp = Instantiate(melee, transform.position, transform.rotation);
            m_timer = 1.0F;
        }

        if (m_timer <= 0.0F)
            DestroyImmediate(temp);

        if (temp)
        {
            m_timer -= Time.deltaTime;
            temp.transform.position += transform.parent.transform.forward * Time.deltaTime * 0.5f;
        }

        // Update the "bullet"
        //transform.position += transform.up * speed * Time.deltaTime;
    }
}
