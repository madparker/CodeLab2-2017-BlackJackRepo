﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Mod_DealerHand : Mod_PlayerHand {

	public Sprite cardBack;

	bool reveal;

	protected override void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<Mod_DeckOfCards>();
		hand = new List<Mod_DeckOfCards.Card>();

		HitMe();

	}

	protected override void HitMe (){

		//Draws a card and then hides it
		base.HitMe ();
		HideCards (hand.Count-1);
	}


	//Hides the card passed into the HideCard function. The method takes an int (child index)
	void HideCards(int index){
		GameObject cardOne = transform.GetChild(index).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

	}
		


}
