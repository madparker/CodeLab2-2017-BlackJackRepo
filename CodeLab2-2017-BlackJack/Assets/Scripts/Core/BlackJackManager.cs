using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}

	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	public void PlayerWin(){
		HidePlayerButtons();
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

	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0; 
		int newValue = 0; // the new hand value
		int aceTotal = 0; // the number of aces in the hand
		int nonAceTotal = 0;
		//aceCount = how many aces there are currently in my hand
		//nonAceTotle = total value of all the other cards. This is added up first. Then we add the number of aces. 
		foreach(DeckOfCards.Card handCard in hand){ //for each card (hereafter referred to as handCard) in the deck "hand"
			newValue = handCard.GetCardHighValue(); // our new value = the value of the card
			if (newValue == 11) { //if the card is an ace
				aceTotal+=1; } //add 1 to our total number of aces
			else { 
				nonAceTotal+=newValue; //otherwise, if card is not an ace, add the value of that card to newValue
				}
			}
				nonAceTotal+=aceTotal; //add the number of aces we HAVE to the total value of all OTHER cards.
				// if we have 1 ace, and one 7, the total value of deck becomes 8
				// if we have 2 aces, and one 7, total value of deck becomes 9
				// you'll see why this works
			
			for(int i = 0; i < aceTotal; i++) { // while i is less than the number of aces we have in the deck (at most, 3), go through the loop
				if (nonAceTotal + 10 <= 21){ //remember, nonAceTotal is now equal to our cards' values, + the number of aces we have (1, 2 or 3)
					//so, we know if our nonAceTotal PLUS 10 is less than or equal to 21, our ace should actually be worth 11, so we just add 10 to make up for the lost 10 value points
					nonAceTotal+=10;}
			}
		handValue = nonAceTotal; //set handValue = to nonAceTotal
		return handValue;
	}
}
