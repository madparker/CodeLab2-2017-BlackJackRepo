using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_DeckOfCards : DeckOfCards {

	// fix to reshuffe when deck only has 20 cards left
	int decksToUse = 4;
	int specialDecksToUse = 10;



	public override Card DrawCard(){

		Card nextCard = deck.Next();
		if (deck.Cursor < 20) {
			deck.Clear ();
			AddCardsToDeck ();
		}
//		Debug.Log (deck.Cursor);
		return nextCard;

	}

	// added special cards
	public class SpecialCard : Card{

		public enum SpecialSuit {
			SPECIAL
		};

		public enum SpecialType {
			PLUS1 = 0,
			PLUS2 = 0,
			PLUS3 = 0,
			PLUS4 = 0,
		};

		public SpecialType spcCardNum;

		public SpecialSuit spcSuit;

		public SpecialCard(SpecialType spcNum, SpecialSuit spcSuit) : base() {
			this.spcCardNum = spcNum;
			this.spcSuit = spcSuit;
		}

		public override string ToString (){
			return "The " + cardNum + " of " + suit;
		}

		public int GetHighValue() {
			int val;
			val = 0;
			return val;
		}
	}


	protected override void Awake () {
		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();
			AddCardsToDeck();
		}

//		Debug.Log("Cards in Deck: " + deck.Count);
		
	}


	// fix to use 4 decks of cards
	protected override void AddCardsToDeck(){
		foreach (SpecialCard.SpecialSuit specialSuit in SpecialCard.SpecialSuit .GetValues(typeof(SpecialCard.SpecialSuit))){
			foreach (SpecialCard.Type specialType in SpecialCard.Type.GetValues(typeof(SpecialCard.SpecialType))){
				for (int i = 0; i < decksToUse; i++) {
//					deck.Add (new SpecialCard (null, null, specialType, specialSuit));
				}
			}
		}
		
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				for (int i = 0; i < decksToUse; i++) {
					deck.Add (new Card (type, suit));
				}
			}
		}
	}

}
