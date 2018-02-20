using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour {

    public GameObject player;
    public GameObject Camera;
    public bool RandomSpawn = false;
    private GameObject floorholder; 
    private Room[] rooms;

	public void Init () {
        floorholder = gameObject.GetComponent<BoardGenerator>().boardHolder;
        rooms = gameObject.GetComponent<BoardGenerator>().GetRooms();

        Vector3 playerpos;

        if (RandomSpawn)
        {
            int randRm = Random.Range(0, rooms.Length - 1);
            playerpos = new Vector3(rooms[randRm].xPos + rooms[randRm].roomWidth * 0.5f, rooms[randRm].yPos + rooms[randRm].roomHeight * 0.5f, 0);
        }
        else
        {
            playerpos = new Vector3(rooms[0].xPos + rooms[0].roomWidth * 0.5f, rooms[0].yPos + rooms[0].roomHeight * 0.5f, 0);
        }

        Camera.GetComponent<CameraController>().SetPlayer(Instantiate(player, playerpos, Quaternion.identity, floorholder.transform));
    }
}
