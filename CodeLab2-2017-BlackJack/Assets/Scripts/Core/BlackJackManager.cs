using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	public InputField playerBetInputField; //the input field where the player will type in their bet amount

	public Text moneyText;
	public Text betText;


	private int money = 100; //the amount of money the player has left with which to bet
	private int betAmount = 0; //the amount the player has bet
	private int amountCurrentlyBet = 0;


	void Start(){
		moneyText.text = ("MONEY: $" + money);
		betText.text = ("BET: $" + betAmount);
	}

	public void OnBet(){ //when the player hits the bet button with an amount entered into the input field
		if(int.TryParse(playerBetInputField.text, out betAmount)){; //if the text they have entered is parsable into an int
			betText.text = ("BET: $" + betAmount);
			money -= betAmount;
			moneyText.text = ("MONEY: $" + money);
			Debug.Log ("Player bet $" + betAmount);
		}
	}


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
		HidePlayerButtons();
		GameOverText("YOU LOSE.", Color.red);
	}


	public void BlackJack(){ //this was never getting called, fixed this in ShowValue in BlackJackHand
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
		int numberOfAces = 0; //the number of aces we have in our hand
		 
		foreach(DeckOfCards.Card handCard in hand){
			if (handCard.cardNum == DeckOfCards.Card.Type.A){ //if the card is an ace
				numberOfAces++; //increment the number of aces we have
			}
			handValue += handCard.GetCardHighValue();
		}
		if (handValue > 21 && numberOfAces > 0){ //if the hand value is greater than 21 and we have one or more aces
			for (int i = 0; i < numberOfAces; i++) { //for each ace that we have
				if (handValue > 21) { //if our hand value is still greater than 21
					handValue -= 10; //reduce our hand value by 10, essentially using the ace's low value of 1 instead of 11
				} //if we still have more aces, we'll go back to the top to check if we're still over 21, and so on
				else break; //we break once we are below 21 or have used up our aces
			}
		}
		//Debug.Log (handValue);
		return handValue;
	}
}
