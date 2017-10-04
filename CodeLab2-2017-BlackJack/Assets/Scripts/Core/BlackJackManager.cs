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

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		return handValue;
	}
}
