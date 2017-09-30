using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}

	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}


	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
	}

	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}

        //if we've found the handValue and it's over 21
        //then we want to make sure it's not because of an Ace
        //so we turn handValue back to 0
        //and run it again, but if the card is an Ace
        //we only add 1 to the handvalue
        //else, do nothing
        if(handValue > 21)
        {
            handValue = 0;
            foreach (DeckOfCards.Card handCard in hand)
            {
                if(handCard.cardNum == DeckOfCards.Card.Type.A)
                {
                    handValue += 1;
                }
                else
                {
                    handValue += handCard.GetCardHighValue();
                }
            }
        }
		return handValue;
	}
}
