using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : BlackJackHand {

    BlackJackManager manager;

    void Awake() {
        manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
    }

	protected override void SetupHand() {
        base.SetupHand();
        if(handValHigh == 21) {
            //player wins automatically
            manager.BlackJack();
        }
    }


}
