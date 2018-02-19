using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public Animator anim;
    public float speed;
    public Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHori = Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHori, moveVerti, 0.0f);
        rigid.velocity = movement * speed;
        anim.SetFloat("MovementX", moveHori);
        anim.SetFloat("MovementY", moveVerti);
    }
}
