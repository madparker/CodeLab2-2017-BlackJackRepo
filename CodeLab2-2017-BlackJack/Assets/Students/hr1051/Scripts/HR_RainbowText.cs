using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HR_RainbowText : MonoBehaviour {

	[SerializeField] Color[] myColors;
	private Text myText;
//	[SerializeField] float mySwitchTime = 0.2f;
//	private float myTimer;

	// Use this for initialization
	void Start () {
//		myTimer = Time.time + mySwitchTime;
		myText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (myTimer <= Time.time) {
//			myTimer = Time.time + mySwitchTime;
			myText.color = myColors [Random.Range (0, myColors.Length)];
//		}
	}
}
