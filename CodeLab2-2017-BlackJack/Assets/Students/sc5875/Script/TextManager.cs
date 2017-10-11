using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour {
	public void Set_Text(string text){
		GetComponent<Text>().text = text;
	}
}
