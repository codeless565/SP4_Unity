using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMap : MonoBehaviour {
    /* Use this script on the display port for the minimap */

    private Vector2 m_ClosedPos;
    private Vector2 m_ClosedSize;

    private Vector2 m_OpenedPos;
    private Vector2 m_OpenedSize;

    private bool b_isOpened;

    private int m_CurrFrame;

    // Use this for initialization
    void Start () {
        m_ClosedPos  = GetComponent<RectTransform>().anchoredPosition;
        m_ClosedSize = GetComponent<RectTransform>().sizeDelta;

        m_OpenedSize = m_ClosedSize * 3;
        m_OpenedPos  = new Vector2(m_OpenedSize.x * -0.5f, m_OpenedSize.y * -0.5f);
        Debug.Log("OpenPos:" + m_OpenedPos);

        b_isOpened = false;
        m_CurrFrame = -1;
    }

    public void OpenMap()
    {
        if (!b_isOpened && m_CurrFrame != Time.frameCount)
        {
            GetComponent<RectTransform>().sizeDelta = m_OpenedSize;
            GetComponent<RectTransform>().anchoredPosition = m_OpenedPos;
            Debug.Log("O_size new:" + GetComponent<RectTransform>().sizeDelta);
            Debug.Log("O_Pos new:" + GetComponent<RectTransform>().localPosition);
            Debug.Log("O_rect new:" + GetComponent<RectTransform>().rect);

            b_isOpened = true;
            m_CurrFrame = Time.frameCount;
        }
    }

    public void CloseMap()
    {
        if (b_isOpened && m_CurrFrame != Time.frameCount)
        {
            GetComponent<RectTransform>().sizeDelta = m_ClosedSize;
            GetComponent<RectTransform>().anchoredPosition = m_ClosedPos;

            b_isOpened = false;
            m_CurrFrame = Time.frameCount;
        }
    }
}
