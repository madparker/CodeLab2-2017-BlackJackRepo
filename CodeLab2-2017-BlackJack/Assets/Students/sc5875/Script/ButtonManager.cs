﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour {
	[SerializeField] List<Button> Button_list;
	// Use this for initialization
	void Start () {
		Button_list.Clear();
		Button_list.AddRange(FindObjectsOfType<Button>());
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			UnityEngine.SceneManagement.SceneManager.LoadScene("SC_PickYourWife");
		}
	}

	public void Disable_all_button(){
		foreach(Button a_button in Button_list){
			a_button.interactable = false;
		}
	}
}
