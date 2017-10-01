using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang{
	public class HR_BlackJackHand : BlackJackHand {

		private BlackJackManager myManager;

		private void Awake () {
			myManager = GameObject.Find ("BlackJackManager").GetComponent<BlackJackManager> ();
		}

		protected override void SetupHand(){
			deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
			hand = new List<DeckOfCards.Card>();
			HitMe();
			HitMe();

			handVals = GetHandValue ();
			if(handVals == 21){
				myManager.BlackJack ();
			}
		}
	}
}
