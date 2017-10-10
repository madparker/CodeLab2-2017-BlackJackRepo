using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	private static BlackJackManager instance = null;
	private static int[] myScore = { 0, 0 };
	[SerializeField] Text myScoreText;

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

		ShowScore ();
	}

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	[SerializeField] Color[] myTextColors = new Color[2];
	public Color[] myCardColors = new Color[2];

	public void PlayerBusted(){
		myScore [1] += 1;
		HidePlayerButtons();
		GameOverText ("YOU BUST", myTextColors [0]);
	}

	public void DealerBusted(){
		myScore [0] += 1;
		GameOverText ("DEALER BUSTS!", myTextColors [1]);
	}
		
	public void PlayerWin(){
		myScore [0] += 1;
		GameOverText ("YOU WIN!", myTextColors [1]);
	}
		
	public void PlayerLose(){
		myScore [1] += 1;
		GameOverText ("YOU LOSE.", myTextColors [0]);
	}


	public void BlackJack(){
		myScore [0] += 1;
		GameOverText ("Rainbow Jack!", myTextColors [1]);
		statusText.GetComponent<HR_RainbowText> ().enabled = true;
		HidePlayerButtons();
	}

	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);

		ShowScore ();
		Debug.Log (myScore [0] + " " + myScore [1]);
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

	private void ShowScore () {
		string t_str0 = myScore [0] == 0 ? "0" : myScore [0].ToString ("#");
		string t_str1 = myScore [1] == 0 ? "0" : myScore [1].ToString ("#");
		myScoreText.text = "Score\n" + t_str0 + ":" + t_str1;
	}
}
