using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	private static BlackJackManager instance = null;

	public static BlackJackManager Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}
	}

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	[SerializeField] Color[] myTextColors = new Color[2];
	public Color[] myCardColors = new Color[2];

	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText ("YOU BUST", myTextColors [0]);
	}

	public void DealerBusted(){
		GameOverText ("DEALER BUSTS!", myTextColors [1]);
	}
		
	public void PlayerWin(){
		GameOverText ("YOU WIN!", myTextColors [1]);
	}
		
	public void PlayerLose(){
		GameOverText ("YOU LOSE.", myTextColors [0]);
	}


	public void BlackJack(){
		GameOverText ("Rainbow Jack!", myTextColors [1]);
		statusText.GetComponent<HR_RainbowText> ().enabled = true;
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
		return handValue;
	}

	public virtual bool CheckBusted (List<DeckOfCards.Card> hand) {
		//count the card amount in each suit
		int[] t_suitCounts = { 0, 0, 0, 0 }; 
		foreach(DeckOfCards.Card handCard in hand){
			int t_suitNum = (int)handCard.suit;
//			Debug.Log (t_suitNum);
			t_suitCounts [t_suitNum]++;
			if (t_suitCounts [t_suitNum] >= 3)
				return true;
		}
		return false;
	}

	public virtual bool CheckBlackJack (List<DeckOfCards.Card> hand) {
		//count the card amount in each suit
		int[] t_suitCounts = { 0, 0, 0, 0 }; 
		foreach(DeckOfCards.Card handCard in hand){
			int t_suitNum = (int)handCard.suit;
			//			Debug.Log (t_suitNum);
			t_suitCounts [t_suitNum]++;
		}

		//if numbers are the same, rainbowJack
		if (t_suitCounts [0] == t_suitCounts [1] &&
		   t_suitCounts [0] == t_suitCounts [2] &&
		   t_suitCounts [0] == t_suitCounts [3])
			return true;
		
		return false;
	}
}
