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

//		public override int GetHandValue(List<DeckOfCards.Card> hand){
//			int handValue = 0;
//			int t_AceCount = 0;
//
//			foreach(DeckOfCards.Card handCard in hand){
//				handValue += handCard.GetCardHighValue();
//
//				if (handCard.cardNum == DeckOfCards.Card.Type.A)
//					t_AceCount++;
//			}
//
//			for (int i = 0; i < t_AceCount; i++) {
//				if (handValue > 21) {
//					handValue -= 10;
//					Debug.Log ("A == 1");
//				} else
//					break;
//			}
//
//			return handValue;
//		}

		public override int GetHandValue(List<DeckOfCards.Card> hand){
			int handValue = 0;
//			int t_AceCount = 0;

			foreach(DeckOfCards.Card handCard in hand){
				handValue += handCard.GetCardHighValue();
			}

			//count the cards in each suit
			int[] t_suitCounts = { 0, 0, 0, 0 }; 
			foreach(DeckOfCards.Card handCard in hand){
				int t_suitNum = (int)handCard.suit;
//				Debug.Log (t_suitNum);
				t_suitCounts [t_suitNum]++;
			}

			//get the min count
			int t_minCount = 3;
			foreach (int t_count in t_suitCounts) {
				if (t_count < t_minCount) {
					t_minCount = t_count;
				}
			}

			//for each 4 suit, plus 10
			handValue = handValue + t_minCount * 10;

			return handValue;
		}

	}
}
