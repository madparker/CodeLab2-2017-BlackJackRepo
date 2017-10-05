using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//player hand script
public class BlackJackHand : MonoBehaviour {

	public Text total; //The text box show the value of player hand
	public float xOffset; //X position of the first card
	public float yOffset; //y position of player cards
	public float xBoard; //x postion of the left board 
	public GameObject handBase; //put the cards under this gameobject  / child gameobject
	public int handVals; //the value of the player hand

	protected DeckOfCards deck;
	protected List<DeckOfCards.Card> hand;
	bool stay = false;

	// Use this for initialization
	void Start () {
		
		SetupHand();
	}

	//Set player hand
	protected virtual void SetupHand(){
		//get the deck
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		//where we put player hand cards
		hand = new List<DeckOfCards.Card>();

		//Deal two cards at the beginning of player round
		HitMe();
		HitMe();

		//if player hand = 21, call black jack
		if(handVals == 21){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().BlackJack();
		}


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void HitMe(){

		//if not stay
		if(!stay){
			//reference the inner class of that class
			//draw a card from the deck when player hit 
			DeckOfCards.Card card = deck.DrawCard();

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;
			//card position depends on the its order
			ShowCard(card, cardObj, hand.Count);
			//add this card to hand
			hand.Add(card);

			ShowValue();

			if (hand.Count == 4) {
				MoveCard ();
			}
			if (hand.Count == 6) {
				MoveCard ();
			}

		}
	}

	//set the card to the right position, and put the card under player hand gameobject
	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){

		cardObj.name = card.ToString();

		cardObj.transform.SetParent(handBase.transform);
		//scale is the original size
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		//y is the same, x depends on the order of this card, each card + 110
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset);

		//display card name and suit on the screen
		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

	//move each card to left
	protected void MoveCard(){

		for (int i=0; i<handBase.transform.childCount; i++){
			
			Transform child = handBase.transform.GetChild (i);
			Vector2 anchorPos = child.GetComponent<RectTransform> ().anchoredPosition;
			if (anchorPos.x > xBoard) {
				child.GetComponent<RectTransform> ().anchoredPosition = anchorPos + new Vector2 (-110, 0);
			} else
				break;
		}
	}

	//Calcluate card value and show it on screen. If player value > 21, call playerbusted function
	protected virtual void ShowValue(){
		
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;
		//if player hand >21, call player busted
		if(handVals > 21){
			GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}

	//call function in another script
	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
