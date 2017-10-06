using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLBlackJackHand : BlackJackHand {

	/*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    protected override void ShowValue() {
        handVals = GetHandValue();

        total.text = "Player: " + handVals;

        if (handVals == 21) {
            GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerWin();
        }

        if (handVals > 21) {
            GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
        }
    }
}