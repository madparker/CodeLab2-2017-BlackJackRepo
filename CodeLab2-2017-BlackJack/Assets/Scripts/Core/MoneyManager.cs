using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

	public static MoneyManager instance = null;

	public int playerMoney = 1000;
	public int dealerMoney = 10000;
	public int minimumBet = 50;
	public int bet = 50;

	public Text dealerMoneyAmount;
	public Text playerMoneyAmount;

	void Awake () {
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of MoneyManager.
			Destroy (gameObject);

		//Set MoneyManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	//use up and down arrows to bet up or bet down
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			// print("Up arrow pressed");
			bet += minimumBet;
			//
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			// print("Down arrow pressed");
			if (minimumBet >= 100) {
				minimumBet -= bet;
			} else if (Input.GetKeyDown(KeyCode.Return)) {
				print("You have placed a bet of " + bet + " dollars.");
			}
		}

		//Display amount of money held by player and dealer
		dealerMoneyAmount.text = "DEALER: " + dealerMoney.ToString();
		playerMoneyAmount.text = "PLAYER: " + playerMoney.ToString();

	}


}
