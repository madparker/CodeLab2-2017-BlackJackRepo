using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DeckOfCard : DeckOfCards {
	protected override void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
				deck.Add(new Card(type, suit));
				deck.Add(new Card(type, suit));
				deck.Add(new Card(type, suit));
			}
		}
	}
}
