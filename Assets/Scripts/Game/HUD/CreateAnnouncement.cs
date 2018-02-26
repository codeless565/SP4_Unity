using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnnouncement : MonoBehaviour {

    public GameObject DisplayTextPrefab;
    public GameObject Placeholder;

	public void MakeAnnouncement(string text)
    {
        //GameObject tempText = Instantiate(DisplayTextPrefab, new Vector3(0, 0, 0), Quaternion.identity, Placeholder.transform);
        GameObject tempText = Instantiate(DisplayTextPrefab, Placeholder.transform);
        tempText.GetComponent<Announcement>().SetNewAnnouncement(text);
    }

}
