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


	int cardCountMin = 20;

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
			int val;

			switch(cardNum){
			case Type.A:
				val = 1;
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

		DrawFaceCards ();
		SetupDeck ();

		print (deck.Count);


	}

	protected virtual bool IsValidDeck(){
		return deck != null;

	}


	public void AddCardsToDeck(){
		// Uses the amount of decks set in deckNum to add cards to the game

		for (int i = 0; i < deckNum; i++) {
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))) {
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))) {
					Card card = new Card (type, suit);
					deck.Add (card);

				}
			}
		}

		RemoveSelectedFaceCardsFromDeck ();

	}

	void RemoveSelectedFaceCardsFromDeck(){
		for (int i = 0; i < startFaces.Count; i++) {
			for (int d = 0; d < deck.Count; d++) {
				if (startFaces [i].cardNum == deck [d].cardNum && startFaces [i].suit == deck [d].suit) {
					deck.Remove (deck [d]);
				}
			}

		}
	}

	public virtual Card DrawCard(){
		//When card count falls below a minimum, the decks are reshuffled
		if (remainingCards <= cardCountMin) {
			deck = null;
			SetupDeck ();
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

		if (!IsValidDeck ()) {
			deck = new ShuffleBag<Card> ();
			cardsUsed = 0;
//			DontDestroyOnLoad (transform.root.gameObject);
			AddCardsToDeck ();


		} else {
			Destroy (transform.gameObject);
		}
	}

	ShuffleBag<Card> SeparateFaceCards(){
		 
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

	public List<Card> DrawFaceCards(){

		startFaces = new List<Card> ();

		faceCards =  SeparateFaceCards();

		for (int i = 0; i < 3; i++) {
			startFaces.Add (faceCards.Next ());
		}

		return startFaces;

	}


}
