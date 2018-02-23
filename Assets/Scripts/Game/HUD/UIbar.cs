using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour {
	[SerializeField]
	private float fillAmount;
	Player2D_StatsHolder stats;
	private Image content;
	// Use this for initialization
	void Start () {
		stats = GetComponent<Player2D_StatsHolder> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			stats.Health -= 1;
		}
		HandleBar ();
	}

	private void HandleBar()
	{
		content.fillAmount = Map(stats.Health, 0, stats.MaxHealth, 0, 1);
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		//inMin is result of health, inMax is max health
		//outMin is 0, outMax is 1
		return(value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}


}
