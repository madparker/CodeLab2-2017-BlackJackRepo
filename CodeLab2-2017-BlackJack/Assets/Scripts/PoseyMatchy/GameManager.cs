using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {

	public Sprite [] poses;
	public static ShuffleBag<Sprite> poseBag; 
	public Sprite currentPose;
	public float currentTimer;
	public float maxTimer;

	public Image player;
	public Image [] bandMates;
	public Image choreographer; 


	void Awake () 
	{
		if (!IsValidBag())  
		{
			poseBag = new ShuffleBag<Sprite> (); 
			for (int i = 0; i < poses.Length; i++) 
			{
				poseBag.Add (poses [i]);
			}
		}
	}
		
	void Start () 
	{
		player = GameObject.Find ("PlayerIcon").GetComponent<Image> ();
		choreographer = GameObject.Find ("Choreographer").GetComponent<Image> ();
		for (int i = 0; i < bandMates.Length; i++) 
		{
			bandMates[i] = GameObject.Find("BandMate_" + i).GetComponent<Image>();
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			GetNewPose ();
		}
		choreographer.sprite = currentPose;
		for (int i = 0; i < bandMates.Length; i++) 
		{
			bandMates [i].sprite = currentPose;
		}
	}

	bool IsValidBag()
	{
		return poseBag != null; 
	}

	void GetNewPose()
	{
		currentPose = poseBag.Next ();
	}
}
