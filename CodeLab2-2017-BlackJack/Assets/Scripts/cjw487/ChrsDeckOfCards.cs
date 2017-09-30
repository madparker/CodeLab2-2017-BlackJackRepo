using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class ChrsDeckOfCards : DeckOfCards
    {
        private const int TOTAL_DECKS_USED  = 4;
        private const int MIN_NUM_CARDS     = 20;

        protected override void AddCardsToDeck()
        {
           for (int i = 0; i < TOTAL_DECKS_USED; i++)
           {
                base.AddCardsToDeck();
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
            Card nextCard = deck.Next();
            deck.Remove(nextCard);
            return nextCard;
        }
    }
}