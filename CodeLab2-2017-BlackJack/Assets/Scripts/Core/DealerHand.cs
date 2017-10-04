using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//dealer hand script
public class DealerHand : BlackJackHand {

	public Sprite cardBack;

	bool reveal;

	//the same as player hand, but hide the first card
	protected override void SetupHand(){

		base.SetupHand();
		//get the first card
		GameObject cardOne = transform.GetChild(0).gameObject;
		//clear the card num text
		cardOne.GetComponentInChildren<Text>().text = "";
		//set the image of the first card to card back
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		//disable the image of the suit
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;
		//not reveal yet
		reveal = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){
			//if the first card has not been revealed, only show the value of the first card
			if(!reveal){
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} 
			//if reveal the first card, get the value of the whole hand
			else {
				
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
				//if dealer handval > 21, call dealerbusted 
				if(handVals > 21){
					manager.DealerBusted();
				} 
				//if dealer stay, get one more card
				else if(!DealStay(handVals)){
					Invoke("HitMe", 1);

				} 
				//
				else {
					
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
					//if dealer hand < player hand, call player win
					if(handVals < playerHand.handVals){
						manager.PlayerWin();

					} 
					else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

	protected virtual bool DealStay(int handVal){
		return handVal > 17;
	}

	//this function will be called if player hit 'stay' button, show the first card
	public void RevealCard(){
		
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		ShowValue();
	}
}
