using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_BlackJackManager : BlackJackManager {

	// fix to make value of ace to 1 if hand value goes > 21
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;
		int nrAces = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
			if (handCard.cardNum == DeckOfCards.Card.Type.A) {
				nrAces++;
			}
		}

		for (int i = 0; i < nrAces; i++) {
			if (handValue > 21) {
				handValue -= 10;
				Debug.Log ("ace = 1 worked");
			} else {
				break;
			}
		}
		return handValue;
	}

}
