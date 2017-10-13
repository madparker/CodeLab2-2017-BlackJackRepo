using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mod_Options : MonoBehaviour {

	Dropdown drop;

	public Dictionary<int, string>faceVales = new Dictionary<int, string>();


	void Start () {
		drop = GetComponent<Dropdown> ();
		AddtoDictionary ();
	}

	public void AddtoDictionary(){

		//Adds options in the dropdown menu to a Dictionary storing the value as index and the card type as string

		for (int i = 0; i < drop.options.Count; i++){
			faceVales.Add (i, drop.options[i].text);
			
		}
			
		
	}
	

}
