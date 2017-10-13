using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {

	public bool cheatingMode;
	public Sprite [] poses;
	public Sprite good;
	public Sprite bad;
	public static ShuffleBag<Sprite> poseBag; 
	public Sprite currentPose;
	public float currentTimer;
	public float maxTimer;

	public float score;
	public float baseValue;
	public float firstMulti;
	public float secondMulti;
	public float thirdMulti;  
	public float multiplier;
	public float gameTimerMax;
	public float gameTimer;
	public float hits;
	public float misses;
	public float streak;
	public int inARow;
	float valueOfMatch;
	public Image player;
	Text scoreDisplay;
	Text multiplierDisplay;
	Text inARowDisplay;
	Image goodBad;
	public Image [] bandMates;
	public Image choreographer; 
	Image timeMeter;
	public float firstSpeedUp;
	public float secondSpeedUp;
	public float firstSpeed;
	public float secondSpeed;
	public float thirdSpeed;
	public float speedUpTextLife;
	public float endTime;
	bool theEnd = false;
	GameObject endText;
	GameObject scoreBoard;


	void Awake () 
	{
		maxTimer = firstSpeed;
		if (!IsValidBag())  
		{
			poseBag = new ShuffleBag<Sprite> (); 
			for (int i = 0; i < poses.Length; i++) 
			{
				poseBag.Add (poses [i]);
			}
		}
		gameTimerMax = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;
		gameTimer = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;
	}
		
	void Start () 
	{
		player = GameObject.Find ("PlayerIcon").GetComponent<Image> ();
		choreographer = GameObject.Find ("Choreographer").GetComponent<Image> ();
		timeMeter = GameObject.Find ("TimeMeter").GetComponent<Image> ();
		scoreDisplay = GameObject.Find ("Score").GetComponent<Text> ();
		multiplierDisplay = GameObject.Find ("Multiplier").GetComponent<Text> ();
		inARowDisplay = GameObject.Find ("In a Row").GetComponent<Text> ();
		goodBad = GameObject.Find ("GoodBad").GetComponent<Image> ();
		endText = GameObject.Find ("EndText");
		scoreBoard = GameObject.Find ("ScoreBoard");
		for (int i = 0; i < bandMates.Length; i++) 
		{
			bandMates[i] = GameObject.Find("BandMate_" + i).GetComponent<Image>();
		}
	}

	void Update () 
	{
		if (cheatingMode) 
		{
			player.sprite = choreographer.sprite;
		}
		gameTimer -= Time.deltaTime;
		valueOfMatch = baseValue * multiplier;
		if (Input.GetKeyDown (KeyCode.Space)) {
			GetNewPose ();
		}
		if (currentPose != null) {
			choreographer.sprite = currentPose;
			for (int i = 0; i < bandMates.Length; i++) {
				bandMates [i].sprite = currentPose;
			}
			if (player.sprite == choreographer.sprite) {
				goodBad.sprite = good;
			} else {
				goodBad.sprite = bad;
			}
		}
		if (theEnd == false) {
			playerInput ();
			handleTimer ();
		}
		UIDisplay ();
		handleMultiplier ();
		SpeedUp ();
		handleEnding ();
	}

	bool IsValidBag()
	{
		return poseBag != null; 
	}

	void GetNewPose()
	{
		currentPose = poseBag.Next ();
	}

	void playerInput ()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			changePlayerPose (0);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			changePlayerPose (1);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			changePlayerPose (2);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			changePlayerPose (3);
		}
	}

	void changePlayerPose(int sent)
	{
		player.sprite = poses [sent];
	}

	void handleTimer()
	{
		currentTimer = currentTimer - Time.deltaTime;
		timeMeter.fillAmount = currentTimer / maxTimer;
		if (currentTimer <= 0) 
		{
			if (player.sprite == choreographer.sprite) {
				score += valueOfMatch;
				inARow++;
				hits++;
			} else {
				inARow = 0;
				misses++;
			}
			GetNewPose ();
			currentTimer = maxTimer;
		}
	}

	void UIDisplay ()
	{
		scoreDisplay.text = "SCORE: " + score.ToString ();
		multiplierDisplay.text = "MULTIPLIER: X" + multiplier.ToString ();
		inARowDisplay.text = "IN A ROW: " + inARow.ToString ();
	}

	void handleMultiplier ()
	{
		if (inARow < firstMulti) {
			multiplier = 1;
		} else if (inARow >= firstMulti && inARow < secondMulti) {
			multiplier = 2;
		} else if (inARow >= secondMulti && inARow < thirdMulti) {
			multiplier = 3;
		} else if (inARow >= thirdMulti) 
		{
			multiplier = 4;
		}

		if (inARow > streak) 
		{
			streak = inARow;
		}
	}

	void SpeedUp ()
	{
		if (gameTimer > firstSpeedUp) {
			maxTimer = firstSpeed;
		} else if (gameTimer <= firstSpeedUp && gameTimer > secondSpeedUp) {
			maxTimer = secondSpeed;
		} else {
			maxTimer = thirdSpeed;
		}

		if (gameTimer <= firstSpeedUp + 1 && gameTimer > firstSpeedUp) 
		{
			activateSpeedText ();
		}

		if (gameTimer <= secondSpeedUp + 1 && gameTimer > secondSpeedUp) 
		{
			activateSpeedText ();
		}
	}

	void activateSpeedText()
	{
		GameObject.Find ("SpeedUp!").GetComponent<CoolText> ().wakeUp(speedUpTextLife);
	}

	void handleEnding ()
	{
		if (gameTimer < endTime) {
			theEnd = true;
		} else {
			theEnd = false;
		}

		if (theEnd) {
			endText.SetActive (true);
			scoreBoard.SetActive (false);
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene (0);
			}
		} else {
			endText.SetActive (false);
			scoreBoard.SetActive (true);
		}
	}
}
