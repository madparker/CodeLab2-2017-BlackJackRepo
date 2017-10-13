using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_BlackJackManager : BlackJackManager {

//	lr_BlackJackHand bJHand;

	void Awake() {
//		bJHand = GameObject.Find("Deck").GetComponent<lr_DeckOfCards>();
	}
	
	public override int GetHandValue(List<lr_DeckOfCards.Card> hand){
		int handValue = 0;
		int nrAces = 0;


		foreach (lr_DeckOfCards.Card handCard in hand) {
			//			changed get hand value for special cards
			if (handCard is lr_DeckOfCards.SpecialCard) {
				if ((handCard as lr_DeckOfCards.SpecialCard).spcSuit == lr_DeckOfCards.SpecialCard.SpecialSuit.MINUSVALUE) {
					handValue -= (handCard as lr_DeckOfCards.SpecialCard).GetSpecialCardHighValue ();
				} else if ((handCard as lr_DeckOfCards.SpecialCard).spcSuit == lr_DeckOfCards.SpecialCard.SpecialSuit.MULTIPLY) {
					handValue = handValue * (handCard as lr_DeckOfCards.SpecialCard).GetSpecialCardHighValue ();
				} else {
					int timesToDraw = (handCard as lr_DeckOfCards.SpecialCard).GetSpecialCardHighValue ();
					for (int i = 0; i < timesToDraw; i++) {
					}
				}
				
			} else {
				// normal cards 
				handValue += handCard.GetCardHighValue ();
				if (handCard.cardNum == lr_DeckOfCards.Card.Type.A) {
					nrAces++;
				}
			}
			
				
//			fix to make value of ace to 1 if hand value goes > 21

			for (int i = 0; i < nrAces; i++) {
				if (handValue > 210) {
					handValue -= 10;

				} else {
					break;
				}
			}
		}



		return handValue;
	}
	






}
