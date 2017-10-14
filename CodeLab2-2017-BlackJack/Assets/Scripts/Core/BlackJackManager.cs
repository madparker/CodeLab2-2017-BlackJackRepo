using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;
	public int BlackJackScore = 0;
	public int PlayerWinScore = 0;
	public int DealerWinScore = 0;
	public GameObject PlayerScoreText;
	public GameObject DealerScoreText;
	public GameObject BlackJackText;

	//Hide Hit and Stay button when player busted

	void Start(){

		PlayerWinScore = PlayerPrefs.GetInt("PlayerWinScore");
		DealerWinScore = PlayerPrefs.GetInt("DealerWinScore");
		BlackJackScore = PlayerPrefs.GetInt("BlackJackScore");
		PlayerScoreText.GetComponent<Text> ().text = "SCORE: " + PlayerWinScore;
		DealerScoreText.GetComponent<Text> ().text = "SCORE: " + DealerWinScore;
		BlackJackText.GetComponent<Text> ().text = "BLACKJACK: " + BlackJackScore;

	
	}
	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
		DealerWinScore++;
		DealerScoreText.GetComponent<Text> ().text = "SCORE: " + DealerWinScore;
		PlayerPrefs.SetInt("DealerWinScore", DealerWinScore);
	}

	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
		PlayerWinScore++;
		PlayerScoreText.GetComponent<Text> ().text = "SCORE: " + PlayerWinScore;
		PlayerPrefs.SetInt("PlayerWinScore", PlayerWinScore);
	}
		
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
		PlayerWinScore++;
		PlayerScoreText.GetComponent<Text> ().text = "SCORE: " + PlayerWinScore;
		PlayerPrefs.SetInt("PlayerWinScore", PlayerWinScore);
	}
		
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
		DealerWinScore++;
		DealerScoreText.GetComponent<Text> ().text = "SCORE: " + DealerWinScore;
		PlayerPrefs.SetInt("DealerWinScore", DealerWinScore);
	}


	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
		BlackJackScore++;
		BlackJackText.GetComponent<Text> ().text = "BLACKJACK: " + BlackJackScore;
		PlayerPrefs.SetInt("BlackJackScore", BlackJackScore);

	}

	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	//this function will be called if player hit 'stay' button
	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	public void TryAgain(){
		SceneManager.LoadScene(loadScene);

	}

	//Calculate the value of the cards on the hand
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){

		int handValue = 0;
		int A_Num = 0;

		foreach(DeckOfCards.Card handCard in hand){
			//if the hand doesn't have A, use the old way
			if (handCard.cardNum != DeckOfCards.Card.Type.A) {
				
				handValue += handCard.GetCardHighValue ();
			}
			//if the hand has A, count how many A it has
			else if (handCard.cardNum == DeckOfCards.Card.Type.A){

				A_Num++;
				handValue += handCard.GetCardHighValue ();
			}
		}
		//if hand card > 21 and we know it has A in it
		if (handValue > 21 && A_Num != 0) {
			//for each A, if the hand value is bigger than 21, turn A value to 1
			for (int i = 0; i < A_Num; i++) {

				if (handValue > 21) {
					handValue = handValue - 10;
				} 
				else break;
			}
		}

		return handValue;
	}
}
