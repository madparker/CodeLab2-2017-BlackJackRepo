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
	}

	public static ShuffleBag<Card> deck; //deck defaults to null, so, if we assigned nothing to it, it's null

	// Use this for initialization
	void Awake () {

		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
		}

		Debug.Log("Cards in Deck: " + deck.Count);
	}



	// statement = sentance that says "do this", ends in a semicolon. ex: return true;
	// expression =  sentance that evaluates to a value. ex: true or false, which evaluates to true
	// return true or false = a statement which evaluates the value of true or false and returns it. In this case, true. 

	protected virtual bool IsValidDeck(){ 
		if (deck==null) {return false;} //if the deck doesn't exist, return false and create a new deck in Awake()
		else {return deck.Count > 19;} //else, if the deck has more than 19 cards, return true and don't create a new deck. If it has less, create a new deck. 

		//if deck is not greater than 20, return false. otherwise, return true.
		//return = break from the function and pass whatever the right hand side evaluates to to where the function was called
		//equal sign is inside a comparison operator because it is inside an expression

	}

	//now wrapped in a for loop that runs the code 4 times to add 4 of each to the deck
	protected virtual void AddCardsToDeck(){
		for (int i = 0; i < 4; i++) {
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

	//int i = 0;

	public virtual Card DrawCard(){
		Card nextCard = deck.Next();
		deck.Remove(nextCard);

		/* makes sure that every other card will be a king or seven
		if (if i % 2 == 0) {
			nextCard = new Card(Card.Type.K, Card.Suit.DIAMONDS);
		} else {
			nextCard = new Card(Card.Type.SEVEN, Card.Suit.DIAMONDS);
		} 

		i++

		//remember to turn on the int i variable above this function
		*/

		return nextCard;
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
