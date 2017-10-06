using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;
	public DeckOfCards.Card [] stackedDeck;
	public int numberOfDecks;
	public int reshuffleAt;
	public static GameObject instance;
	int cardy;

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

	public static ShuffleBag<Card> deck;

	// Use this for initialization
	void Awake () {
		cardy = 0;
		
		if (!IsValidDeck ()) {
			deck = new ShuffleBag<Card> ();
			for (int i = 0; i < numberOfDecks; i++) {
				AddCardsToDeck ();
			}
			DontDestroyOnLoad (transform.root.gameObject);
		} else {
			Destroy (gameObject);
		}

		stackedDeck = new DeckOfCards.Card[6];
		stackedDeck [0] = new Card (Card.Type.A, Card.Suit.CLUBS);
		stackedDeck [1] = new Card (Card.Type.EIGHT, Card.Suit.CLUBS);
		stackedDeck [2] = new Card (Card.Type.TWO, Card.Suit.DIAMONDS);
		stackedDeck [3] = new Card (Card.Type.TWO, Card.Suit.HEARTS);
		stackedDeck [4] = new Card (Card.Type.A, Card.Suit.SPADES);
		stackedDeck [5] = new Card (Card.Type.TWO, Card.Suit.SPADES);
	}

	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	protected virtual void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Cards left in deck: " + deck.Cursor);
		if (deck.Cursor < reshuffleAt) 
		{
			deck.reShuffle ();
		}
	}

	public virtual Card DrawCard(){
		Card nextCard = null;
		if (cardy < stackedDeck.Length) {
			nextCard = stackedDeck [cardy];
			cardy++;
		} else {
			nextCard = deck.Next();
		}

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
