using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hang;

namespace Hang{
	public class HR_DeckOfCards : DeckOfCards {
		[SerializeField] int myDeckCount = 4;
		private int myCardAmount = 208;
		[SerializeField] Text myCardCountText;
		[SerializeField] RectTransform myCardBack;
		[SerializeField] Vector2 myCardBackXRange = new Vector2 (0, 20);

		public override Card DrawCard(){
			Card nextCard = deck.Next();

			myCardCountText.text = deck.Cursor.ToString ("#");
			myCardBack.anchoredPosition = new Vector2 (
				(float)deck.Cursor / (float)myCardAmount * (myCardBackXRange.y - myCardBackXRange.x) + myCardBackXRange.x,
				0
			);

//			Debug.Log (nextCard.ToString ());
			if (deck.Cursor < 20) {
				deck.Clear ();
				AddCardsToDeck ();
//				Debug.Log (nextCard.ToString ());
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

			myCardAmount = deck.Count;
		}

	}
}
