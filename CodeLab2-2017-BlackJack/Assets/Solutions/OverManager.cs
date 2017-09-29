using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverManager : BlackJackManager {

	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = base.GetHandValue(hand);

		if(handValue > 21){
			foreach(DeckOfCards.Card handCard in hand){
				if(handCard.cardNum == DeckOfCards.Card.Type.A){
					handValue -= 10;
					if(handValue < 21){
						break;
					}
				}
			}
		}

		return handValue;
	}
}
