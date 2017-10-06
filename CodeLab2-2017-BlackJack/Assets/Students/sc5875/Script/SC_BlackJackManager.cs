using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BlackJackManager : BlackJackManager {
	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			if(handCard.cardNum != DeckOfCards.Card.Type.A){
				handValue += handCard.GetCardHighValue();
			}
		}
		
		foreach(DeckOfCards.Card handCard in hand){
			if(handCard.cardNum == DeckOfCards.Card.Type.A){
				handValue += 11;
				if(handValue > 21){
					handValue -= 10;
				}
			}
		}

		return handValue;
	}

	public void GameTie(){
		GameOverText("Tie!", Color.yellow);
		HidePlayerButtons();
	}
}
