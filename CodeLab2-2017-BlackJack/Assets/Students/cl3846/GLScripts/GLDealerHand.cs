using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDealerHand : DealerHand {

    /*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    protected override void ShowValue() {
        BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
        BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

        if (playerHand.handVals == 21) {
            manager.PlayerWin();
        } else {
            base.ShowValue();
        }
    }

    protected override bool DealStay(int handVal) {
        BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
        return handVal >= 17 || handVal >= playerHand.handVals;
    }
}
