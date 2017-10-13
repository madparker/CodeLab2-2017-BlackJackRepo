using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lr_BlackJackHand : BlackJackHand {


	protected override void SetupHand() {
		deck = GameObject.Find ("Deck").GetComponent<lr_DeckOfCards> ();
		hand = new List<DeckOfCards.Card> ();
//		specialHand = new List<lr_DeckOfCards.SpecialCard>();
		HitMe ();
		HitMe ();
	
		// fix to let player win with natural blackjack (deactivated for mod)

//		handVals = GetHandValue ();
//		if (handVals == 21) {
//			GameObject.Find ("BlackJackManager").GetComponent<lr_BlackJackManager> ().BlackJack ();
//		}
//
	}

	protected override void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString ();

		cardObj.transform.SetParent (handBase.transform);
		cardObj.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		cardObj.GetComponent<RectTransform> ().anchoredPosition = 
			new Vector2 (
			xOffset + pos * 110, 
			yOffset);

		// added check to see if it is a special card

		if (!(card is lr_DeckOfCards.SpecialCard)) {

			cardObj.GetComponentInChildren<Text> ().text = deck.GetNumberString (card);
			cardObj.GetComponentsInChildren<Image> () [1].sprite = deck.GetSuitSprite (card);

		
		} else {
			cardObj.GetComponentInChildren<Text> ().text = deck.GetSpecialNumberString ((lr_DeckOfCards.SpecialCard)card);
			cardObj.GetComponentsInChildren<Image> () [1].sprite = deck.GetSpecialSuitSprite ((lr_DeckOfCards.SpecialCard)card);
//			Debug.Log ("special card added");
		}


	}


}
