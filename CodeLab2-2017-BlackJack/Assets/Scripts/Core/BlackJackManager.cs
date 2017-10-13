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

    //Added natural black jack check
    public void NaturalBlackJack()
    {
        GameOverText("Natural Black Jack!", Color.green);
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
        int count = 0;
        List<DeckOfCards.Card> aces = new List<DeckOfCards.Card>();

        foreach (DeckOfCards.Card handCard in hand){
            if(handCard.cardNum == DeckOfCards.Card.Type.A)
            {
                aces.Add(handCard);
            }
			handValue += handCard.GetCardHighValue();
		}

        //if our current hand value is over 21 and we have atleast 1 ace, decrease our score by 10 until we have under 21 or until we run out of aces
        if(handValue > 21 && aces.Count > 0)
        {
            while(handValue > 21 && count < aces.Count)
            {
                handValue -= 10;
                count++;
            }
        }

		return handValue;
	}
}
