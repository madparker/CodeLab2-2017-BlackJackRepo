using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDeckOfCards : DeckOfCards {

    /*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    private const int TOTAL_CARDS_USED = 4;
    private const int MINNUM_NUMBER_OF_CARDS = 20;

    public class BlackJackCard : DeckOfCards.Card {
        public BlackJackCard(Type cardNum, Suit suit) : base(cardNum, suit) {
        }

        public int GetCardLowValue() {
            int val;

            switch (cardNum) {
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

    protected override void AddCardsToDeck() {
        for (int i = 0; i < TOTAL_CARDS_USED; i++) {
            foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))) {
                foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))) {
                    deck.Add(new BlackJackCard(type, suit));
                }
            }
        }
    }

    protected override bool IsValidDeck() {
        if (deck != null) {
            return deck.Count > MINNUM_NUMBER_OF_CARDS;
        }
        return deck != null;
    }

    public override Card DrawCard() {
        BlackJackCard nextCard = (BlackJackCard)deck.Next();
        deck.Remove(nextCard);
        return nextCard;
    }
}
