using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	//Hide Hit and Stay button when player busted
	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}

	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}


	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
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
