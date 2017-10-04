using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DealerHand : BlackJackHand {

	public Sprite cardBack;

	bool reveal;

	protected override void SetupHand(){
		base.SetupHand();

		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){
            if (!reveal) {
                //before the reveal
                handValHigh = hand[1].GetCardHighValue();

                total.text = "Dealer: " + handValHigh + " + ???";
            }
            else {

                BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

                handValHigh = GetHandValue();
                handValLow = GetHandLowValue();

                Debug.Log("dealer hand value low " + handValLow);
                Debug.Log("dealer hand value hi " + handValHigh);

                if (handValLow == handValHigh) {
                    handVal = handValHigh;
                } else if (handValLow != handValHigh && handValHigh > 21) {
                    handVal = handValLow;
                } else  {
                    handVal = handValHigh;
                }

                if (handVal <= 21) {
                    //if the high value of the dealer's hand is less than 21,
                    //show value and determine if they will hit
                    total.text = "Dealer: " + handVal;
                }
                else if (handVal > 21) {
                    //Debug.Log("is this bullshit being called????");
                    // if both hi and low values are greater than 21, dealer busts
                    manager.DealerBusted();
                    return;

                }

				if (!DealStay(handVal)) {
					//case when dealer has a high value over 21, low value under 21
					//(using aces as value 1), and is showing 16 or less, so they must
					//hit
					Invoke("HitMe", 1);
					
				}
				else {
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVal < playerHand.handVal){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

	protected virtual bool DealStay(int handVal){
		return handVal > 16;
	}

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		ShowValue();
	}
}
