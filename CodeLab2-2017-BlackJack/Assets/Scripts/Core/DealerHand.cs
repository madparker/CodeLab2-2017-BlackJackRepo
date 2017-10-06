using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DealerHand : BlackJackHand {

	public Sprite cardBack;
	
	bool reveal;

	protected override void SetupHand(){
		base.SetupHand();
		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){
			if(!reveal){ //dealer's second card has not been revealed
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else { //if dealer's second card HAS been revealed.
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

				if(handVals > 21){
					manager.DealerBusted();
				} else if (handVals == 21){
					manager.PlayerLose();
				} else if(!DealStay(handVals)){
 					Invoke("HitMe", 1);
				} else if (DealStay(handVals)){
						
 					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
				else {
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		} 
	}

	protected virtual bool DealStay(int handVal){
		//problem here is, if player stays at a value below 17, say, 16, the dealer will keep asking for hits until it reaches 17,
		//despite victory being assured already.
		bool dealStay = false;
		// BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

		if(handVal < 17){//first check if dealer hand is less than 17.
			//dealer should never stay if hand is less than 17.
			dealStay = false;	
		}
		else if(handVal >= 17){
			dealStay = true;			// as long as hand is >= 17, ALWAYS STAY.
		}	
		return dealStay; 
		// return handVal > 17;
	}

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		ShowValue();
	}
}
