using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	public class Card{

		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,	//2
			HEARTS	 	//3
		};

		public enum Type {
			TWO		= 2,
			THREE	= 3,
			FOUR	= 4,
			FIVE	= 5,
			SIX		= 6,
			SEVEN	= 7,
			EIGHT	= 8,
			NINE	= 9,
			TEN		= 10,
			J		= 11,
			Q		= 12,
			K		= 13,
			A		= 14
		};

		public Type cardNum;
		
		public Suit suit;

		public Card(Type cardNum, Suit suit){
			this.cardNum = cardNum;
			this.suit = suit;
		}

		public override string ToString(){
			return "The " + cardNum + " of " + suit;
		}

		public int GetCardHighValue(){
			int val;

			switch(cardNum){
			case Type.A:
				val = 11;
				break;
			case Type.K:
			case Type.Q:
			case Type.J:
				val = 10;
				break;	
			default:
				val = (int)cardNum;
				break;
			}

			return val;
		}

		public int GetCardLowValue(){
			int val = (int)cardNum; 
			switch(cardNum){
			case Type.A:
				val = 1;
				break;
			default: 
				val = (int)cardNum;
				break;
			}
			
			return val;
		}
	}

	public static ShuffleBag<Card> deck;

	// Use this for initialization
	void Awake () {

		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
			Debug.Log("Cards in deck: " + deck.Count);
		}

 	}

	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	protected virtual void AddCardsToDeck(){
		for (int i = 0; i<4; i++){ //add cards to deck 4 times.
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
					deck.Add(new Card(type, suit));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual Card DrawCard(){
		if(deck.Cursor >= 20){ //keep getting the next card for as long as there are more than 20 in the deck.
			Card nextCard = deck.Next();
			return nextCard;
		}
		else { //if there are more than 20,
			deck.Clear(); //clear the deck of all the old cards
			AddCardsToDeck(); //add new ones
			Card nextCard = deck.Next(); //then get the next card in the newly shuffled four decks.
			return nextCard;
		}
	}


	public string GetNumberString(Card card){
		if(card.cardNum.GetHashCode() <= 10){
			return card.cardNum.GetHashCode() + "";
		} else {
			return card.cardNum + "";
		}
	}
		
	public Sprite GetSuitSprite(Card card){
		return cardSuits[card.suit.GetHashCode()];
	}
}
