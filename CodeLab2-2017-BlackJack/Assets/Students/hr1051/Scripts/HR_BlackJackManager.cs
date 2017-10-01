using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang{
	public class HR_BlackJackManager : BlackJackManager {
		void Update () {
			if (Input.GetKeyDown (KeyCode.Space))
				TryAgain ();
		}

		public override int GetHandValue(List<DeckOfCards.Card> hand){
			int handValue = 0;
			int t_AceCount = 0;

			foreach(DeckOfCards.Card handCard in hand){
				handValue += handCard.GetCardHighValue();

				if (handCard.cardNum == DeckOfCards.Card.Type.A)
					t_AceCount++;
			}

			for (int i = 0; i < t_AceCount; i++) {
				if (handValue > 21) {
					handValue -= 10;
					Debug.Log ("A == 1");
				} else
					break;
			}

			return handValue;
		}
	}
}
