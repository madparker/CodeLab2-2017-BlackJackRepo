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

	protected DeckOfCards deck;
	protected List<DeckOfCards.Card> hand;
	bool stay = false;

	// Use this for initialization
	void Start () {
		SetupHand();
 	}

	protected virtual void SetupHand(){
		BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
		manager.aces.Clear(); //probably unnecessary... but making sure again that no aces carry over.
  		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		HitMe();
		HitMe();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void HitMe(){
		if(!stay){
			DeckOfCards.Card card = deck.DrawCard();

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

			hand.Add(card);

			ShowValue();
		}
	}

	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
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

		if(handVals > 21){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
		} 
		//check if handVals == 21 AND number of cards in hand is 2. only way this can happen is with face and Ace.
		else if (handVals == 21 && hand.Count == 2){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().BlackJack();
		}
	}

	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
