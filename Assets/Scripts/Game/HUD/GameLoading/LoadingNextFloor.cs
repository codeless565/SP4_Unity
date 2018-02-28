using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingNextFloor : MonoBehaviour
{
    Image LoadingCircle;

    // Use this for initialization
    public void Init()
    {
        gameObject.SetActive(true);

        LoadingCircle = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdateLoadAnimation()
    {
        float currRotation = LoadingCircle.transform.rotation.z;
        LoadingCircle.transform.rotation = Quaternion.Euler(0, 0, currRotation - 100);
    }

}
