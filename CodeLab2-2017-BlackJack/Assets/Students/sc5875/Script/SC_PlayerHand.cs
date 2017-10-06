using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerHand : BlackJackHand {
	protected override void ShowValue(){
		handVals = GetHandValue();
		
		total.text = "Player: " + handVals;
		if(handVals == 21){
			BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
			manager.HidePlayerButtons();
			manager.PlayerWin();
		}
				
		if(handVals > 21){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}
}
