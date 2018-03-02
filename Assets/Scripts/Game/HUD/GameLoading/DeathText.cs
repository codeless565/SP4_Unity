using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathText : MonoBehaviour
{
    public Text ScreenTitle;
    string m_originalTitle;

    public float MessageChangeDelay = 3;
    bool m_textchanged;

    float elapseTime;
    int counter;

    string m_killedByResult;
    string m_DeathMsg;

    // Use this for initialization
    void Start()
    {
        ScreenTitle = transform.parent.GetComponent<Text>();
        m_originalTitle = ScreenTitle.text;

        m_killedByResult = PlayerPrefs.GetString("KilledBy");

        switch (m_killedByResult)
        {
            case "Time":
                m_DeathMsg = "You ran out of time";
                break;
            case "Projectile":
                m_DeathMsg = "You were killed by the boss";
                break;
            case "NonDeflect":
                m_DeathMsg = "You were blasted by the boss's rage";
                break;
            case "NonDeflectSelf":
                m_DeathMsg = "You were tricked by the boss";
                break;
            default:
                m_DeathMsg = "";
                break;
        }

        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = m_DeathMsg;

        m_textchanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapseTime += Time.deltaTime;
        counter += Random.Range(0, 2);

        if (elapseTime >= MessageChangeDelay)
        {
            //if (ScreenTitle.text == m_originalTitle)
            //    ScreenTitle.text = "CONTINUE?";
            //else
            //    ScreenTitle.text = m_originalTitle;

            elapseTime = 0;
            if (!m_textchanged)
            {
                switch (m_killedByResult)
                {
                    case "Time":
                        m_DeathMsg = "Time is not your friend, try to clear the floors quickly.";
                        break;
                    case "Projectile":
                        m_DeathMsg = "You can reflect the shots by attacking it.";
                        break;
                    case "NonDeflect":
                        m_DeathMsg = "Avoid the rage shots! You cannot deflect them.";
                        break;
                    case "NonDeflectSelf":
                        m_DeathMsg = "The rage shots will take in your attacks and deal them to you, evade them!";
                        break;
                    default:
                        m_DeathMsg = "";
                        break;
                }
            }
        }
        if (counter % 15 == 0)
        {
            ScreenTitle.text = " " + m_originalTitle;
        }
        else if (counter % 18 == 0)
        {
            ScreenTitle.text = m_originalTitle + " ";
        }
        else
            ScreenTitle.text = m_originalTitle;

    }
}
