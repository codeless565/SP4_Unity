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

    private Camera m_MinimapCam;
    private float m_DefaultCamSize;
    private float m_OpenedCamSize;

    // Use this for initialization
    public void Init () {
        m_ClosedPos  = GetComponent<RectTransform>().anchoredPosition;
        m_ClosedSize = GetComponent<RectTransform>().sizeDelta;

        m_OpenedSize = m_ClosedSize * 3;
        m_OpenedPos  = new Vector2(m_OpenedSize.x * -0.5f, m_OpenedSize.y * -0.5f);

        m_MinimapCam = GameObject.FindGameObjectWithTag("Minimap Camera").GetComponent<Camera>();

        m_DefaultCamSize = m_MinimapCam.orthographicSize;
        m_OpenedCamSize = m_DefaultCamSize * 3;

        b_isOpened = false;
    }

    public void ToggleMap()
    {
        if (!b_isOpened)
        {
            GetComponent<RectTransform>().sizeDelta = m_OpenedSize;
            GetComponent<RectTransform>().anchoredPosition = m_OpenedPos;
            m_MinimapCam.orthographicSize = m_OpenedCamSize;

            b_isOpened = true;
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = m_ClosedSize;
            GetComponent<RectTransform>().anchoredPosition = m_ClosedPos;
            m_MinimapCam.orthographicSize = m_DefaultCamSize;

            b_isOpened = false;
        }
    }
}
