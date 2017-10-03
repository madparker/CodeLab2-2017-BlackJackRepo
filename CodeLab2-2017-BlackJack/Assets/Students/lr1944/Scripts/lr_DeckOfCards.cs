using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_DeckOfCards : DeckOfCards {

	// fix to reshuffe when deck only has 20 cards left
	int nrCardsLeft = 52;
	int decksToUse = 4;

	public override Card DrawCard(){
		// didnt work because of reloading scene

//		if (nrCardsLeft > 19) {
//			nrCardsLeft--;
//			Card nextCard = deck.Next ();
//			Debug.Log (nrCardsLeft);
//			return nextCard;
//		} else {
//			nrCardsLeft = 52;
//			deck = new ShuffleBag<Card> ();
//			AddCardsToDeck ();
//			Card nextCard = deck.Next ();
//			Debug.Log (nrCardsLeft);
//			return nextCard;
//		}

		Card nextCard = deck.Next();
		if (deck.Cursor < 20) {
			deck.Clear ();
			AddCardsToDeck ();
		}
		Debug.Log (deck.Cursor);
		return nextCard;

	}
	// fix to use 4 decks of cards
	protected override void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				for (int i = 0; i < decksToUse; i++) {
					deck.Add (new Card (type, suit));
				}
			}
		}
	}

}
