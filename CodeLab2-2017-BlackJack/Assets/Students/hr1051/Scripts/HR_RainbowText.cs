using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HR_RainbowText : MonoBehaviour {

	[SerializeField] Color[] myColors;
	private Text myText;

	// Use this for initialization
	void Start () {
		myText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		myText.color = myColors [Random.Range (0, myColors.Length)];
	}
}
