using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*remember to attach script to the bar itself*/
public class UIbar : MonoBehaviour {
	private float fillAmount;
	Player2D_StatsHolder stats;
	private Image content;

	public float MaxValue{ get; set; }

	public float Value
	{
		set
		{ 
			fillAmount = Map (value, 0, MaxValue, 0, 1); 
		}
	}

	// Use this for initialization
	void Start () {
		stats = GetComponent<Player2D_StatsHolder> ();
		content = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	private void HandleBar()
	{
		if (fillAmount != content.fillAmount) {
			content.fillAmount = fillAmount;
		}
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		//inMin is result of health, inMax is max health
		//outMin is 0, outMax is 1
		return(value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}


}
