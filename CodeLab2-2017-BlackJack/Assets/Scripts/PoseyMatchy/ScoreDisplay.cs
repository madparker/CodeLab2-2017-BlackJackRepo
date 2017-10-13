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

		if (gameObject.name == "Your Score") 
		{
			GetComponent<Text> ().text = "Your Score: " + gm.score;
		}
		if (gameObject.name == "Hits") 
		{
			GetComponent<Text> ().text = "Hit: " + gm.hits;
		}
		if (gameObject.name == "Misses") 
		{
			GetComponent<Text> ().text = "Missed: " + gm.misses;
		}
		if (gameObject.name == "LongestStreak") 
		{
			GetComponent<Text> ().text = "Longest Streak: " + gm.streak;
		}
		
	}
}
