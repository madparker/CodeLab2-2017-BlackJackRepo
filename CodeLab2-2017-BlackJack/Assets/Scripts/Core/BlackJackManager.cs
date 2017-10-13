using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;

	public GameObject restart;
	public GameObject betUI;
	public GameObject actionUI;
	public GameObject doubleDownUI;

	public string loadScene;

	public BlackJackHand playerHand;
	public DealerHand dealerHand;
	public DeckOfCards deckOfCards;


	public InputField playerBetInputField; //the input field where the player will type in their bet amount
	public InputField doubleDownInputField; //the input field where the player will type in their double down amount 

	public Text moneyText; //the ui text for diplaying money
	public Text betText; //the ui text for displaying current bet
	public Text betWarningText; //the ui text for displaying warnings for incorrect betting procedure


	private static int money = 100; //the amount of money the player has left with which to bet
	private int betAmount = 0; //the amount the player has bet


	void Start(){
		UpdateMoneyText(); //set text to reflect starting values
	}

	public void OnBet(){ //when the player hits the bet button with an amount entered into the input field
		if(int.TryParse(playerBetInputField.text, out betAmount)){; //if the text they have entered is parsable into an int
			if(betAmount > money){ //if the player bet more than they have
				betWarningText.text = ("NOT ENOUGH $$"); //tell them the don't have enough
			}else { //if they have enough to make the bet
				betWarningText.text = (""); //clear status text in case they had warnings
				money -= betAmount; //subtract amount bet from money
				UpdateMoneyText(); //update our text to reflect new bet/money values
				Debug.Log ("Player bet $" + betAmount);

				ToggleBetUI(); //toggle off the bet UI
				ToggleActionUI();//toggle on the player action UI

				playerHand.SetupHand();// setup the player and dealer hands
				dealerHand.SetupHand();
			}

		}
	}

	public void OnDoubleDown(){
		int oldBet = betAmount; //store the value of the original bet
		Debug.Log ("Old bet was: " + oldBet);
		if(int.TryParse(doubleDownInputField.text, out betAmount)){; //if the text they have entered is parsable into an int, set that as the value of the bet
			if(betAmount > money){ //if the player bet more than they have
				betWarningText.text = ("NOT ENOUGH $$"); //tell them the don't have enough
			}else if(betAmount > oldBet){ //if the new bet amount is more than their original bat, tell them it's too high
				betWarningText.text = ("BET TOO HIGH"); 
			}else{
				betWarningText.text = (""); //clear status text in case they had warnings
				money -= betAmount; //subtract the new amount bet from money
				betAmount += oldBet; //add the old bet to the new bet to reflect the total amount bet
				UpdateMoneyText(); //update our text to reflect new bet/money values
				Debug.Log ("Player bet $" + betAmount);

				ToggleDoubleDownUI(); //toggle off the double down UI;
				ToggleActionUI();

				playerHand.Invoke("HitMe", 1);
				dealerHand.Invoke("RevealCard", 1);
			}
		}
	}

	public void UpdateMoneyText(){ //call this to update our money and bet texts
		moneyText.text = ("MONEY: $" + money); //set money text 
		betText.text = ("BET: $" + betAmount); //set bet text 
	}


	public void PlayerBusted(){
		betAmount = 0;
		UpdateMoneyText();

		ToggleActionUI();
		GameOverText("YOU BUST", Color.red);
	}

	public void DealerBusted(){
		money += (betAmount * 2);
		betAmount = 0;
		UpdateMoneyText();

		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	public void PlayerWin(){
		money += (betAmount * 2);
		betAmount = 0;
		UpdateMoneyText();


		GameOverText("YOU WIN!", Color.green);
	}
		
	public void PlayerLose(){
		betAmount = 0;
		UpdateMoneyText();

		GameOverText("YOU LOSE.", Color.red);
	}


	public void BlackJack(){ //this was never getting called, fixed this in ShowValue in BlackJackHand
		money += (betAmount * 2);
		betAmount = 0;
		UpdateMoneyText();

		ToggleActionUI();
		GameOverText("BLACK JACK!", Color.green);
	}

	public void GameOverText(string str, Color color){
		if(money > 0){
			statusText.text = str;
			statusText.color = color;
			tryAgain.SetActive(true);
		} else {
			statusText.text = ("GAME OVER");
			statusText.color = Color.red;
			restart.SetActive(true);
		}
	}

	public void ToggleBetUI(){
		if(betUI.activeSelf){
			betUI.SetActive(false);
		}else{
			betUI.SetActive(true);
		}
	}

	public void ToggleActionUI(){
		if(actionUI.activeSelf){
			actionUI.SetActive(false);
		}else{
			actionUI.SetActive(true);
		}
	}

	public void ToggleDoubleDownUI(){
		if(doubleDownUI.activeSelf){
			doubleDownUI.SetActive(false);
			statusText.text = ("");
		}else{
			doubleDownUI.SetActive(true);
		}
	}

	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}
	
	public void Restart(){ //this resets everything for a new game
		money = 100; //sets money to starting value

		DeckOfCards.deck.Clear();// clear the cards in the deck
		deckOfCards.AddCardsToDeck(); //add a new set of cards to the deck from which to pull

		SceneManager.LoadScene(loadScene); //reloads scene
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
