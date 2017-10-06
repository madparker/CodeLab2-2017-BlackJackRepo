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
        //  STATUS  : PENDING REVIEW
        public override int GetHandValue(List<DeckOfCards.Card> hand)
        {
            int handValue = 0;
            foreach (ChrsDeckOfCards.BlackJackCard handCard in hand)
            {
                handValue += handCard.GetCardHighValue();
            }

            if (handValue > 21)
            {
                handValue = 0;
                foreach (ChrsDeckOfCards.BlackJackCard handCard in hand)
                {
                    handValue += handCard.GetCardLowValue();
                }
            }

            return handValue;
        }
    }
}