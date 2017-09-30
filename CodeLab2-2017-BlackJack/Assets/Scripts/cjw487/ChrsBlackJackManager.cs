using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class ChrsBlackJackManager : BlackJackManager
    {
        //  BUG     : Ace value does not change when advantageous
        //  CATEGORY: GAME RULES
        //  STATUS  : PENDING REVIEW

        //  BUG     : On suboptimal plays Ace reassigning decrements score
        //  CATEGORY: GAME RULES
        //  STATUS  : OPEN

        //  This solution is not a complete one. If a player were to play
        //  sub-optimally this fix will break down.
        //
        //  Example:
        //          Player Hand : 4, 8, 7, A
        //          Player Score: 20
        //          *Player Hits*
        //          Player Hand : 4, 8, 7, A, 9
        //          Player Score: 19
        //          CorrectScore: 29
        //
        //  The issue is my hand does not know if it has reassigned its
        //  Aces.
        //
        //  One Solution is to have a bool inside card to check if has been
        //  reassigned.
        //
        //  Issue w/ Proposed solution:
        //      1.  This requires a change to the card class which I 
        //          don't have access too (?)
        //      2.  Logically it doesn't make sense for a card to know
        //          if it has been reassigned. That logic should only
        //          exist within the player aka the hand.

        public override int GetHandValue(List<DeckOfCards.Card> hand)
        {
            int handValue = 0;

            foreach (DeckOfCards.Card handCard in hand)
            {
                if (handValue + handCard.GetCardHighValue() > 21 && 
                    handCard.cardNum == DeckOfCards.Card.Type.A)
                {
                    handValue++;
                }
                else
                {
                    handValue += handCard.GetCardHighValue();
                }

                if (handValue > 21)
                {
                    handValue = ReassignAceValue(hand, handValue);
                }
            }

            return handValue;
        }

        protected int ReassignAceValue(List<DeckOfCards.Card> hand, int handValue)
        {
            int newValue = handValue;

            foreach (DeckOfCards.Card handCard in hand)
            {
                if (handCard.cardNum == DeckOfCards.Card.Type.A)
                {
                    newValue -= 10;
                }
            }

            return newValue;
        }
    }
}