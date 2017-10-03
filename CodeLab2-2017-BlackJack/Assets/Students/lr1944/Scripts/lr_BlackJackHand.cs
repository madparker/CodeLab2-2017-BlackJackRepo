using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_BlackJackHand : BlackJackHand {

	protected override void SetupHand() {
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		HitMe();
		HitMe();

		// fix to let player win with natural blackjack

		handVals = GetHandValue ();
		if (handVals == 21) {
			GameObject.Find("BlackJackManager").GetComponent<lr_BlackJackManager>().BlackJack();
		}

	}

}
