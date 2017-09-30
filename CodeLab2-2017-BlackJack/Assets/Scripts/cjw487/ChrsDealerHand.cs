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

            if(playerHand.handVals == 21)
            {
                manager.PlayerWin();
            }
            else
            {
                base.ShowValue();
            }
        }

        protected override bool DealStay(int handVal)
        {
            BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
            return handVal >= 17 || handVal >= playerHand.handVals;
        }
    }
}