using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mod_GameManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	public GameObject perp;

	public static List<Mod_DeckOfCards.Card> flopHand =  new List<Mod_DeckOfCards.Card> (); 

	void Awake(){
		flopHand.Clear ();
	}

	public void PlayerWin(){
		GameOverText("CAUGHT!", Color.green);
	}

	public void PlayerLose(){
		GameOverText("SLANDER!", Color.red);
	}

	public void PlayerGuess(){
		statusText.text = "CALL";
		statusText.color = Color.white;
		HidePlayerButtons ();
	}

	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	public void HidePlayerButtons(){
		GameObject hitButton = GameObject.Find ("HitButton");
		hitButton.GetComponent<Image> ().enabled = false;
		hitButton.GetComponent<Button> ().enabled = false;
		hitButton.GetComponentInChildren<Text> ().enabled = false;

	}

	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	public void GuessPerp(){
		// When option is selected from the dropdown, crosses references Perp card with guess and determines result

		Dropdown drop = perp.GetComponentInChildren<Dropdown> ();

		perp.GetComponent<Mod_AgentHand> ().RevealCard ();

		//Storing card types as strings
		string s1 = drop.GetComponent<Mod_Options> ().faceVales [drop.value]  + " (UnityEngine.GameObject)";
		string s2 = perp.GetComponent<Mod_AgentHand> ().cardName;

		if (s1 == s2) {
			// If values match, player wins
//			print (s1 + " " + s2);
			PlayerWin ();
		} else {
			// If values don't match, player loses

			PlayerLose ();
		}

	}

	public void PromptGuess(){
		PlayerGuess ();

	}


}
