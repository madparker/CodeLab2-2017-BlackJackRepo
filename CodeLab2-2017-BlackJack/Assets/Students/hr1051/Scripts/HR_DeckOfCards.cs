using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang{
	public class HR_DeckOfCards : DeckOfCards {
		[SerializeField] int myDeckCount = 4;

		public override Card DrawCard(){
			Debug.Log (deck.Cursor);
			Card nextCard = deck.Next();
			if (deck.Cursor < 20) {
				deck.Clear ();
				AddCardsToDeck ();
			}
			return nextCard;
		}

		protected override void AddCardsToDeck(){
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
					for (int i = 0; i < myDeckCount; i++) {
						deck.Add (new Card (type, suit));
					}
				}
			}
		}

	}
}
