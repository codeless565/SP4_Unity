using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatSpawn : MonoBehaviour
{    
    GameObject m_Floor;
    GameObject m_Exit;
    GameObject m_BossChest;

    public void InitSpawn(Vector3 _bossPos)
    {
        m_Floor = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().GameLevel;
        m_Exit = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().Exit;
        m_BossChest = GameObject.FindGameObjectWithTag("Holder").GetComponent<StructureObjectHolder>().BossChest;

        Vector3 ExitDisplacement = new Vector3(0, 3);

        Instantiate(m_Exit, _bossPos + ExitDisplacement, Quaternion.identity, m_Floor.transform);
        Instantiate(m_BossChest, _bossPos, Quaternion.identity, m_Floor.transform);
    }
}
