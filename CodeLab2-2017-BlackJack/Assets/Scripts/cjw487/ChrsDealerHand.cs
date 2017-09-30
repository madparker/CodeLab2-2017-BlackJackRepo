using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class ChrsDealerHand : DealerHand
    {
        protected override void ShowValue()
        {
            BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
            BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

            //  BUG     : Player does not win on a "Natural Blackjack"
            //  CATEGORY: GAME RULES
            //  STATUS  : PENDING REVIEW
            if (playerHand.handVals == 21)
            {
                manager.PlayerWin();
            }
            else
            {
                base.ShowValue();
            }
        }

        //  BUG     : Dealer hits when above 17
        //  CETEGORY: GAME RULES
        //  STATUS  : PENDING REVIEW

        //  BUG     : If dealer is tied with player or ahead after player stays, dealer Hits
        //  CATEGORY: AI LOGIC
        //  STATUS  : PENDING REVIEW
        protected override bool DealStay(int handVal)
        {
            BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
            return handVal >= 17 || handVal >= playerHand.handVals;
        }
    }
}