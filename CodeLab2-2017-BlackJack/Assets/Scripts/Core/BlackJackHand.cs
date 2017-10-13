using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour {

	public Text total;
	public float xOffset;
	public float yOffset;
	public GameObject handBase;
	public int handVals;

	protected lr_DeckOfCards deck;
	protected List<lr_DeckOfCards.Card> hand;
//	protected List<lr_DeckOfCards.SpecialCard> specialHand;
	bool stay = false;

	// Use this for initialization
	void Start () {
		SetupHand();
	}

	protected virtual void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<lr_DeckOfCards>();
		hand = new List<lr_DeckOfCards.Card>();

//		specialHand = new List<lr_DeckOfCards.SpecialCard>();
		HitMe();
		HitMe();
	}
	


	public void HitMe(){
		if(!stay){
			lr_DeckOfCards.Card card = deck.DrawCard();
//			Debug.Log ("is being hit");

		


			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

//			if (!(card is lr_DeckOfCards.SpecialCard)) {
				
				hand.Add (card);
//			}

//			if ((card as lr_DeckOfCards.SpecialCard).spcSuit == lr_DeckOfCards.SpecialCard.SpecialSuit.PLUSCARDS) {
//				int timesToDraw = (card as lr_DeckOfCards.SpecialCard).GetSpecialCardHighValue ();
//				for (int i = 0; i < timesToDraw; i++) {
//					HitMe ();
//				}
//			}

			ShowValue();
		}
	}

	protected virtual void ShowCard(lr_DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString();

		cardObj.transform.SetParent(handBase.transform);
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset);

		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

	protected virtual void ShowValue(){
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;

		if(handVals > 210){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}

	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
		GameObject gO = gameObject;
		return manager.GetHandValue(hand, gO);
	}
}
