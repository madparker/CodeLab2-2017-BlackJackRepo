﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	//an inner class, public so it can be accessed anywhere
	public class Card{
		//an enum is essentially a description of the potential possible states a thing can be in. 
		//in this case, a suit can be spades, clubs, hearts, or diamonds, but not bears
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
				val = 11; //problem here, the value should change based on situation
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

		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
		}

		Debug.Log("Cards in Deck: " + deck.Count);
	}

	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	protected virtual void AddCardsToDeck(){
		for (int i = 0; i < 4; i++) { //here was the problem, needed to add this loop so that we get 4 decks worth of cards
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){ //some problem here? this is virtual, wonder why
					deck.Add(new Card(type, suit));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual Card DrawCard(){
		Card nextCard = deck.Next();

		if (deck.Cursor < 20){ //if there are 20 or less cards in the deck
			deck.Clear();// clear the cards in the deck
			AddCardsToDeck(); //add a new set of cards to the deck from which to pull
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
