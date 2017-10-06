using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class ChrsBlackJackHand : BlackJackHand
    {
        protected override void ShowValue()
        {
            handVals = GetHandValue();

            total.text = "Player: " + handVals;

            //  BUG     : Player does not automatically win when value is 21 after Hit
            //  CATEGORY: QUALITY OF LIFE
            //  STATUS  : PENDING REVIEW
            if(handVals == 21)
            {
                GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerWin();
            }

            if (handVals > 21)
            {
                GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
            }
        }
    }
}