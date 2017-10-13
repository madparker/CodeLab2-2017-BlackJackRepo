using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lr_DealerHand : DealerHand {


//	public Sprite cardBack;
//
//	bool reveal;
//
//	protected override void SetupHand(){
//		base.SetupHand();
//
//		GameObject cardOne = transform.GetChild(0).gameObject;
//		cardOne.GetComponentInChildren<Text>().text = "";
//		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
//		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;
//
//		reveal = false;
//	}
//
//	protected override void ShowValue(){
//
//		if(hand.Count > 1){
//			if(!reveal){
//				handVals = hand[1].GetCardHighValue();
//
//				total.text = "Dealer: " + handVals + " + ???";
//			} else {
//				handVals = GetHandValue();
//
//				total.text = "Dealer: " + handVals;
//
//				BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
//
//				if(handVals > 210){
//					manager.DealerBusted();
//				} else if(!DealStay(handVals)){
//					Invoke("HitMe", 1);
//				} else {
//					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
//
//					if(handVals < playerHand.handVals){
//						manager.PlayerWin();
//					} else {
//						manager.PlayerLose();
//					}
//				}
//			}
//		}
//	}

	// fix to let dealer stay when 17 or higher



	protected override bool DealStay(int handVal){
		if (handVal >= 206) {
			return true;
		} else {
			return false;
		}
	}

//	public void RevealCard(){
//		reveal = true;
//
//		GameObject cardOne = transform.GetChild(0).gameObject;
//
//		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
//		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;
//
//
////		if (hand.Count > 0) {
//			ShowCard (hand [0], cardOne, 0);
////		} else { ShowCard (specialHand [0], cardOne, 0);
////		}
//
//		ShowValue();
//	}




}
