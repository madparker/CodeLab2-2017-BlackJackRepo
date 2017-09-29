using UnityEngine;
using System.Collections;

public class OverBlackJackHand : BlackJackHand {

	protected override void SetupHand(){
		base.SetupHand();

		if(GetHandValue() == 21){
			BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

			manager.BlackJack();
		}
	}
		
}
