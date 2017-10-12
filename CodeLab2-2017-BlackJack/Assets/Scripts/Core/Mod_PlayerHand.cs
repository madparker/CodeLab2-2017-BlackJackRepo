﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Mod_PlayerHand: MonoBehaviour {

	public Text total;
	public float xOffset;
	public float yOffset;
	public GameObject handBase;
	public int handVals;


	protected Mod_DeckOfCards deck;
	protected List<Mod_DeckOfCards.Card> hand;
	bool stay = false;

	// Use this for initialization
	void Start () {
		SetupHand();

	}

	protected virtual void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<Mod_DeckOfCards>();
		hand = new List<Mod_DeckOfCards.Card>();
		HitMe();
//		HitMe();


	}
	

	public void HitMe(){
		if(!stay){
			Mod_DeckOfCards.Card card = deck.DrawCard();

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

			hand.Add(card);

//			ShowValue();
		}
	}

	protected void ShowCard(Mod_DeckOfCards.Card card, GameObject cardObj, int pos){
		float mod = 110f;

		cardObj.name = card.ToString();

		cardObj.transform.SetParent(handBase.transform);
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * mod, 
				yOffset);

		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

//	protected virtual void ShowValue(){
//		handVals = GetHandValue();
//			
//		total.text = "Player: " + handVals;
//
//		if (handVals > BlackJackManager.blackJackValue) {
//			GameObject.Find ("BlackJackManager").GetComponent<BlackJackManager> ().PlayerBusted ();
//		} 
//	}
//
//	public int GetHandValue(){
//		BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
//
//		return manager.GetHandValue(hand);
//	}
}
