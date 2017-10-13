using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolText : MonoBehaviour {

	public float sizeLerpSpeed;
	public float colorLerpSpeed;
	public Vector3 small;
	public Vector3 large;
	public Color light; 
	public Color dark; 
	public Color nothing;
	public bool alive;
	public float currentLifeSpan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		currentLifeSpan--;
		gameObject.transform.localScale = Vector3.Lerp(small, large, Mathf.PingPong(Time.time * sizeLerpSpeed, 1));
		if (alive) {
			GetComponent<Text> ().color = Color.Lerp (light, dark, Mathf.PingPong (Time.time * sizeLerpSpeed, 1));  
		} 
		else 
		{
			GetComponent<Text> ().color = nothing;
		}

		if (currentLifeSpan <= 0) {
			currentLifeSpan = 0;
			alive = false;
		} 
	}

	public void wakeUp (float span)
	{
		currentLifeSpan = span;
		alive = true;
	}
		
}
