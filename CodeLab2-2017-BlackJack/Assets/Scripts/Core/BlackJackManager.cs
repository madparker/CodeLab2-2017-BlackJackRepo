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
		GameOverText("YOU WIN!", Color.green);
	}
		
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}


	public void BlackJack(){
		GameOverText("BLACK JACK!", Color.green);
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
		int nonAceValue = 0;
		int tempAceValue = 0;
		List<DeckOfCards.Card> aces = new List<DeckOfCards.Card> ();
		List<DeckOfCards.Card> nonAces = new List<DeckOfCards.Card> ();
		List<int> possibleResults = new List<int> ();
		foreach (DeckOfCards.Card handCard in hand) 
		{
			if (handCard.cardNum == DeckOfCards.Card.Type.A) {
				aces.Add (handCard);
			} else {
				nonAces.Add (handCard);
			}
		}

		foreach(DeckOfCards.Card nonAce in nonAces)
		{
			handValue += nonAce.GetCardHighValue ();
		}
		foreach(DeckOfCards.Card ace in aces)
		{
			if (handValue + 11 > 21) {
				handValue++;
			}
			else 
			{
				handValue += ace.GetCardHighValue ();
			}

		}
		return handValue;
	}
}
