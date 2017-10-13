using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	GameManager gm;

	// Use this for initialization
	void Start () {

		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Text> ().text = "Your Score: " + gm.score;
		
	}
}
