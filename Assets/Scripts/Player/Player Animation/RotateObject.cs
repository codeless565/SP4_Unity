using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
	private Transform thisTransform;
	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
        	thisTransform.Rotate(Vector3.up *-15* Input.GetAxis("Mouse X"));
      	}
	}
}
