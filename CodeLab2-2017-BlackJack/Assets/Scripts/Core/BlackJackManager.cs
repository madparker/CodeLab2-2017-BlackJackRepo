using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	List <DeckOfCards.Card> aces = new List<DeckOfCards.Card>();

	public static int blackJackValue = 21;

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

		for (int i = 0; i < hand.Count; i++) {
			// Put Aces in a list to evaluate them separately 
			if (hand [i].cardNum != DeckOfCards.Card.Type.A) {
				handValue += hand [i].GetCardHighValue ();
			} else {
				if (!aces.Contains (hand [i])) {	
					aces.Add (hand [i]);
				}
			}
		}

		if (aces.Count > 0) {

			//Calculate Aces and see which need high value and which need low value

			for (int a = 0; a < aces.Count; a++) {
				if (handValue + aces [a].GetCardHighValue () > blackJackValue) {
					handValue += aces [a].GetCardLowValue ();
				} else {
					handValue += aces [a].GetCardHighValue ();
				}
			}
		}

		// If after calculating, the handvalue is great than 21, make them all low value
		if (handValue > blackJackValue) {
			handValue = 0;
			for (int l = 0; l < hand.Count; l++) {
				handValue += hand [l].GetCardLowValue ();
			}
		}


		return handValue;
	}


		
}
