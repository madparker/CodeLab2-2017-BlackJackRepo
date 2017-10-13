using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mod_AgentHand : Mod_PlayerHand {

	public Sprite cardBack;
	public Dropdown dropDown;

	bool reveal;

	public string cardName;

	protected override void SetupHand(){

		//Draws the Perp card and Hides it
		deck = GameObject.Find("Deck").GetComponent<Mod_DeckOfCards>();
		hand = new List<Mod_DeckOfCards.Card>();
		HitMe();

		GameObject cardOne = transform.GetChild(1).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;



		reveal = false;
	}

	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(1).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		cardName = cardOne.ToString ();

	}


}
