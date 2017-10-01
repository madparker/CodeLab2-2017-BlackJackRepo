using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang{
	public class HR_DealerHand : DealerHand {

		private BlackJackHand myPlayer;

		private void Awake () {
			myPlayer = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
		}

		protected override bool DealStay(int handVal){
			if (GetHandValue() >= 17)
				return true;

			if (myPlayer.GetHandValue () <= GetHandValue ())
				return true;

			return false;
		}
	}
}
