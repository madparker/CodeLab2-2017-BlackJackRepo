using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Mod_DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	int deckNum = 1;
	public static int cardsUsed = 0;
	public static int remainingCards;



	//Inner Class
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
			return "The " + cardNum.ToString() + " of " + suit.ToString();
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

	public static ShuffleBag<Card> deck;

	ShuffleBag<Card> faceCards;
	public List<Card> startFaces; 

	// Use this for initialization
	void Awake () {

		SetupDeck ();

		// Sets remaining cards to amount of cards in the deck at the start
		remainingCards = deck.Count;


	}

	//Draw a card from the deck
	public virtual Card DrawCard(){
		//When card count falls below a minimum, forces player to guess.
		if (remainingCards <= 0) {
			
//			deck = null;
			Debug.Log("No More Cards");

			GameObject.Find ("GameManager").GetComponent<Mod_GameManager> ().PromptGuess ();
			return null;

		}
	
		//Draws card
		Card nextCard = deck.Next();


		//Tracks how many cards have been used
		cardsUsed++;
		remainingCards = deck.Count - cardsUsed;

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

	void SetupDeck(){

		//Setup up a new deck
		deck = new ShuffleBag<Card> ();

		// Makes sure the deck is made up of Face cards only
		deck = SeparateFaceCards ();

		//No cards used it, this is incremented in the DrawCard() method
		cardsUsed = 0;

	
	}

	ShuffleBag<Card> SeparateFaceCards(){

		//Makes a shufflebag of face cards and returns it
		 
		faceCards = new ShuffleBag<Card> ();
		for (int i = 0; i < deckNum; i++) {
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))) {
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))) {
					if (type == Card.Type.J || type == Card.Type.K || type == Card.Type.Q)
					faceCards.Add (new Card (type, suit));
				}
			}
		}

		return faceCards;
	}
		


}
