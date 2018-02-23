using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls of the Camera
public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        offset = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
	}

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}
