﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapChest : MonoBehaviour
{
    private Sprite s_Unseen;
    private Sprite s_TooFar;
    private Sprite s_Exposed;
    private Camera c_MainCam;
    private float viewDist;
    private bool exposed;
    private bool highDifficulty;

    void Start()
    {
        // get Sprites from holder
        s_Unseen = GameObject.FindGameObjectWithTag("Holder").GetComponent<MinimapIconHolder>().Black;
        s_TooFar = GameObject.FindGameObjectWithTag("Holder").GetComponent<MinimapIconHolder>().Floor;
        s_Exposed = GameObject.FindGameObjectWithTag("Holder").GetComponent<MinimapIconHolder>().Chest;

        // Get Camera and set view distance
        c_MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        viewDist = c_MainCam.orthographicSize * 2; // orthographicSize is in center to top/bottom, but center to left/right is about twice that length

        // Set unseen Sprite
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = s_Unseen;
        exposed = false;
        highDifficulty = false;
    }

    void Update()
    {
        if (Time.frameCount % 10 == 0)  // Check every 10th frame so as to not lag the performance too much
        {
            // Check distance to reduce the amount if calculation against the camera at line 37~38
            Vector2 distFromCam = GetComponent<Transform>().position - c_MainCam.GetComponent<Transform>().position;
            if (distFromCam.magnitude > viewDist)
            {
                if (exposed) // only change to floor instead if player has seen it
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = s_TooFar; // Disappear on the map once player is out of range
                return;
            }

            // Check if this tile is within the view frustrum of the player's main camera
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(c_MainCam);
            if (GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<BoxCollider2D>().bounds))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = s_Exposed; // Change the mapicon sprite
                exposed = true;

                if (!highDifficulty)
                    Destroy(this);
            }
        }
    }
}
