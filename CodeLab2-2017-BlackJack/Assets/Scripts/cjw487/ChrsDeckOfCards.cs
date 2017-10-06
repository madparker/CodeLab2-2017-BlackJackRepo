using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class ChrsDeckOfCards : DeckOfCards
    {
        private const int TOTAL_DECKS_USED  = 4;
        private const int MIN_NUM_CARDS     = 20;

        
        public class BlackJackCard : DeckOfCards.Card
        {
            public BlackJackCard(Type cardNum, Suit suit) : base(cardNum, suit)
            {
            }

            public int GetCardLowValue()
            {
                int val;

                switch (cardNum)
                {
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

        protected override void AddCardsToDeck()
        {
           for (int i = 0; i < TOTAL_DECKS_USED; i++)
           {
                foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type)))
                    {
                        deck.Add(new BlackJackCard(type, suit));
                    }
                }
            }
        }

        protected override bool IsValidDeck()
        {
            if(deck != null)
            {
                //  BUG     : Game does not reset when decck has less than 20 cards
                //  CATEGORY: GAME RULES
                //  STATUS  : PENDING REVIEW
                return deck.Count > MIN_NUM_CARDS;
            }

            return deck != null;
        }

        //  BUG     : Deck does not decrement after each Hit
        //  CATEGORY: GAME RULES
        //  STATUS  : PENDING REVIEW
        public override Card DrawCard()
        {
            BlackJackCard nextCard = (BlackJackCard)deck.Next();
            deck.Remove(nextCard);
            return nextCard;
        }
    }
}