﻿using UnityEngine;
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
			if(!reveal){
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
				MoneyManager moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

				if(handVals > 21){
					manager.DealerBusted();
					moneyManager.playerMoney += moneyManager.bet;
					moneyManager.dealerMoney -= moneyManager.bet;
				} else if(!DealStay(handVals)){ //if the dealer's hand is not greater than 17
					Invoke("HitMe", 1); //dealer hits
				} else {
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						manager.PlayerWin();
						moneyManager.playerMoney += moneyManager.bet;
						moneyManager.dealerMoney -= moneyManager.bet;
					} else {
						manager.PlayerLose();
						moneyManager.playerMoney -= moneyManager.bet;
						moneyManager.dealerMoney += moneyManager.bet;
					}
				}
			}
		}
	}

	protected virtual bool DealStay(int handVal){
		return handVal >= 17;
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
