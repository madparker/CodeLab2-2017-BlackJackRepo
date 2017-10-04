﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	public class Card{
		//Four suits
		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,	//2
			HEARTS	 	//3
		};
		//Card Types
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
		//Card has two value: suit and number
		public Card(Type cardNum, Suit suit){
			this.cardNum = cardNum;
			this.suit = suit;
		}
		//Why do we need this?
		public override string ToString(){
			return "The " + cardNum + " of " + suit;
		}
		//get the highest value of the card?
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
	//false randomness
	public static ShuffleBag<Card> deck;

	// Use this for initialization
	void Awake () {

		if(!IsValidDeck()){
			
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
		}

		//Debug.Log("Cards in Deck: " + deck.Count);
	}

	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	//add 13 * 4 = 52 cards into deck
	protected virtual void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
	}

	//draw new card from the deck
	public virtual Card DrawCard(){
		
		Card nextCard = deck.Next();

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
