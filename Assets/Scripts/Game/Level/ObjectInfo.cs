using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour {

    // Variable //
	private int m_RoommIndex;
    private Room m_RoomDetail;
    private Vector2 m_MapPosition;

    // "Constructor" //
    public void Init(int _roomIndex, Room _roomDetail, Vector2 _mapPosition)
    {
        m_RoommIndex  = _roomIndex;
        m_RoomDetail  = _roomDetail;
        m_MapPosition = _mapPosition;
    }

    // Getter and Setter //
    public int RoomIndex
    {
        get
        {
            return m_RoommIndex;
        }
        set
        {
            m_RoommIndex = value;
        }
    }

    public Room RoomDetail
    {
        get
        {
            return m_RoomDetail;
        }
        set
        {
            m_RoomDetail = value;
        }
    }

    public Vector2 MapPostion
    {
        get
        {
            return m_MapPosition;
        }
        set
        {
            m_MapPosition = value;
        }
    }
}
