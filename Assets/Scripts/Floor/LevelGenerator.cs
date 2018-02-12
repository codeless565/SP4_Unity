using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject Floor  = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().Floor;
    
        GameObject Wall   = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StoneWall;
        GameObject Pillar = GameObject.FindGameObjectWithTag("StructureHolder").GetComponent<StructureObjectHolder>().StonePillar;


        for (int i = 0; i < 10; ++i)
        {
            float posX = Random.value * Floor.transform.localScale.x;
            float posY = Random.value * Floor.transform.localScale.x;
            Vector3 pos = new Vector3(posX, 0, posY);
            Instantiate(Pillar, pos, transform.rotation);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    /* TODO
     * Spawn Check with other structures
     * spawn exit last and check with all existing structures
     */
}
