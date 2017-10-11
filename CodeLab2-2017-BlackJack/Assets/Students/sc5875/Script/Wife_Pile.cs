using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wife_Pile : MonoBehaviour {
	protected Sprite wife;
	// Use this for initialization
	void Start(){
		GetComponent<Image>().enabled = false;
		GetComponent<Button>().interactable = false;
	}
	public void wife_select(Sprite PickyWife){
		GetComponent<Image>().enabled = true;
		GetComponent<Button>().interactable = true;
		wife = PickyWife;
	}
	public void reveal_Wife(){
		GetComponent<Image>().sprite = wife;
		var colors = GetComponent<Button>().colors;
		colors.disabledColor = Color.white;
		GetComponent<Button>().colors = colors;

		FindObjectOfType<ButtonManager>().Disable_all_button();
		string phrase = FindObjectOfType<PickWives>().Get_String(wife);
		FindObjectOfType<TextManager>().Set_Text(phrase);
	}
	
}
