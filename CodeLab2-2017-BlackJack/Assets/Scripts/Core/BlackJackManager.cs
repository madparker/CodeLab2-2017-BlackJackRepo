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
		aces.Clear();
	}

	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
		aces.Clear();
	}
		
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
		aces.Clear();
	}
		
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
		aces.Clear();
	}


	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
		aces.Clear();
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

	public List<DeckOfCards.Card> aces = new List<DeckOfCards.Card>();
	public int num = 0;
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){

			if(handCard.cardNum == DeckOfCards.Card.Type.A){ //if the card drawn is an Ace
				aces.Add(handCard); //increment the number of aces.
				if(handValue + handCard.GetCardHighValue() >= 17 && handValue + handCard.GetCardHighValue() <= 21){
					handValue += handCard.GetCardHighValue();
				} else if(handValue + handCard.GetCardHighValue() > 21){
					handValue += handCard.GetCardLowValue();
					aces.RemoveAt(0);
				} else if (handValue + handCard.GetCardHighValue() < 17){
					handValue += handCard.GetCardHighValue();
				}
   			} else { //if the card drawn is everything but an Ace.
				//1. Add that card's value to the handValue.
				handValue += handCard.GetCardHighValue();
  				//if the new handValue exceeds 21, 
				if(handValue > 21 && aces.Count > 0){
					while (handValue > 21 && aces.Count>0){
					 
						//if adding a non-Ace card busts a player and there are aces in the hand,
						//keep subtracting 10 until player isn't busted.
						handValue -= 10;
						Debug.Log("Subtracting 10!" + handCard);
						aces.RemoveAt(0);//remove an ace from the list every time its value is subtracted.
					}   
				} 	// 3 + 1 + 10 = 14 
					// 3 + 11 + 10 = 24
					//  	
			}



			// if(handCard.cardNum == DeckOfCards.Card.Type.A && handValue >= 11){
			// 	handValue += handCard.GetCardLowValue();				
			// }
			// else {
			// 	handValue += handCard.GetCardHighValue();
			// }
		}
		aces.Clear(); // make sure there are no aces that carry over to the dealer's hand.
		return handValue;
	}
}
