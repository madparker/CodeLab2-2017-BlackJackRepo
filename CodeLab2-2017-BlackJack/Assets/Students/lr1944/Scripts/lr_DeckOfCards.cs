using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_DeckOfCards : DeckOfCards {

	// fix to reshuffe when deck only has 20 cards left
	int decksToUse = 4;
	int specialDecksToUse = 15;



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
			PLUSCARDS,
			MULTIPLY,
			MINUSVALUE,

		};

		public enum SpecialType {
			TWO = 2,
			THREE = 3,
			FOUR = 4,
			FIVE = 5,
		};
			

		public SpecialType spcCardNum;

		public SpecialSuit spcSuit;

		public SpecialCard(SpecialType spcNum, SpecialSuit spcSuit) : base() {
			this.spcCardNum = spcNum;
			this.spcSuit = spcSuit;
		}

		public override string ToString (){
			return "The " + spcCardNum + " of " + spcSuit;
		}

		public int GetSpecialCardHighValue(){
			int val;

			val = (int)spcCardNum;

			return val;
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


	// fix to use 4 decks of cards + special cards changes
	protected override void AddCardsToDeck(){
		foreach (SpecialCard.SpecialSuit specialSuit in SpecialCard.SpecialSuit .GetValues(typeof(SpecialCard.SpecialSuit))){
			foreach (SpecialCard.SpecialType specialType in SpecialCard.SpecialType.GetValues(typeof(SpecialCard.SpecialType))){
				for (int i = 0; i < specialDecksToUse; i++) {
					deck.Add (new SpecialCard (specialType, specialSuit));

//					Debug.Log("special card added");
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

	//added for special cards

	public string GetSpecialNumberString(SpecialCard card){
		
		return card.spcCardNum.GetHashCode() +"";
		
	}

	public Sprite GetSpecialSuitSprite(SpecialCard card){
//		Debug.Log(cardSuits[card.spcSuit.GetHashCode()]);
		return cardSuits[card.spcSuit.GetHashCode()+4];

	}
		

}
