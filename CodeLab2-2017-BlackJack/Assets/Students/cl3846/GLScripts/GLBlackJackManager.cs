using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLBlackJackManager : BlackJackManager {

	/*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    public override int GetHandValue(List<DeckOfCards.Card> hand) {
        int handValue = 0;
        foreach (GLDeckOfCards.BlackJackCard handCard in hand) {
            handValue += handCard.GetCardHighValue();
        }

        if (handValue > 21) {
            handValue = 0;
            foreach (GLDeckOfCards.BlackJackCard handCard in hand) {
                handValue += handCard.GetCardLowValue();
            }
        }

        return handValue;
    }
}
