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
		cardOne.GetComponentsInChildren<Image> () [0].color = BlackJackManager.Instance.myCardColors [1];
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){
			if (!reveal) {
				handVals = hand [1].GetCardHighValue ();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				handVals = GetHandValue ();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find ("BlackJackManager").GetComponent<BlackJackManager> ();

				if (CheckBusted ()) {
					manager.DealerBusted ();
				} else if (!DealStay (handVals)) {
					Invoke ("HitMe", 1);
				} else {
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
		return handVal > 17;
	}

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).GetChild(0).gameObject;


		FlipCard(hand[0], cardOne, 0);

		ShowValue();
	}

	protected void FlipCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.GetComponentsInChildren<Image>()[1].enabled = true;

		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
		cardObj.GetComponent<Image> ().color = deck.GetSuitColor (card);
	}
}
