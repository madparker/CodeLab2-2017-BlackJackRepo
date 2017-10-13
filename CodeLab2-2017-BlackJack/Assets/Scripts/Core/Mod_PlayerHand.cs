using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Mod_PlayerHand: MonoBehaviour {

	public Text total;
	public float xOffset;
	public float yOffset;
	public GameObject handBase;
	public GameObject flop;
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
		HitMe();


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
		float mod = 70f;

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

	protected virtual void DrawFlop(){

		float mod = 0.5f;
		Mod_DeckOfCards.Card card = deck.DrawCard();


		GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

		cardObj.name = card.ToString();

		cardObj.transform.SetParent(flop.transform);
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		cardObj.GetComponent<RectTransform>().anchoredPosition =  new Vector2( xOffset,  yOffset * (mod* Mod_GameManager.flopHand.Count));

		cardObj.GetComponentInChildren<Text> ().text = deck.GetNumberString (card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);

		Mod_GameManager.flopHand.Add(card);


	
	}
		

	public void FlopSuitCheck(){
		for (int i = 0; i < hand.Count; i++) {
			if (Mod_GameManager.flopHand.Count > 0) {
				if (Mod_GameManager.flopHand [Mod_GameManager.flopHand.Count - 1].suit == hand[i].suit) {
					return;
				} 
			}
				
		}

		HitMe ();
	}
}
