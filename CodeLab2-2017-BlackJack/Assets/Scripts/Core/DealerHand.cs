using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DealerHand : BlackJackHand {

	public Sprite cardBack;

	bool reveal;
    bool play;

	protected override void SetupHand(){
		base.SetupHand();

		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
        play = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){
            if (!reveal)
            {
                handVals = 0;
                int count = 0;
                List<DeckOfCards.Card> aces = new List<DeckOfCards.Card>();

                for (int i = 1; i < hand.Count; i++)
                {
                    if(hand[i].cardNum == DeckOfCards.Card.Type.A)
                    {
                        aces.Add(hand[i]);
                    }
                    handVals += hand[i].GetCardHighValue();
                }

                if (handVals > 21 && aces.Count > 0)
                {
                    while (handVals > 21 && count < aces.Count)
                    {
                        handVals -= 10;
                        count++;
                    }
                }

                total.text = "Dealer: " + handVals + " + ???";

                //if its our turn play without revealing our card
                if (play)
                {
                    BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
                    if (handVals > 21)
                    {
                        manager.DealerBusted();
                    }
                    else if (!DealStay(handVals))
                    {
                        Invoke("HitMe", 1);
                    }
                    else
                    {
                        Invoke("RevealCard", 1);
                    }
                }

            } else {
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

				if(handVals > 21){
					manager.DealerBusted();
				} else {
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

	protected virtual bool DealStay(int handVal){
		return handVal >= 17;
	}

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		ShowValue();
	}

    //Clicking "Stay" Button now invokes this command
    public void DealerPlay()
    {
        play = true;
        ShowValue();
    }
}
