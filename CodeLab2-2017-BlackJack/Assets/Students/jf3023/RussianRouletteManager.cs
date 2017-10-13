using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RussianRouletteManager : MonoBehaviour {

    public Text statusText;
    public GameObject tryAgian;
    public GameObject pass;
    public GameObject loadGun;
    public GameObject dropOut;
    public GameObject pullTrigger;

	// Use this for initialization
	void Start () {
       pass = GameObject.Find("PassButton");
       loadGun = GameObject.Find("LoadGunButton");
       dropOut = GameObject.Find("DropOutButton");
       pullTrigger = GameObject.Find("PullTriggerButton");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropOut()
    {
        HidePlayerButtons();
        GameOverText("YOU CHICKENED OUT.", Color.red);

    }

    public void DealerDead()
    {
        HidePlayerButtons();
        GameOverText("YOU'RE OPPONENT SHOT THEMSELVES!.", Color.green);
    }

    public void PlayerDead()
    {
        HidePlayerButtons();
        GameOverText("YOU SHOT YOURSELF.", Color.red);
    }

    public void GameOverText(string str, Color color)
    {
        statusText.text = str;
        statusText.color = color;

        tryAgian.SetActive(true);
    }

    public void HidePlayerButtons()
    {
        if (pass.activeInHierarchy ==true)
        pass.SetActive(false);

        if(loadGun.activeInHierarchy == true)
        loadGun.SetActive(false);

        if(dropOut.activeInHierarchy == true)
        dropOut.SetActive(false);

        if (pullTrigger.activeInHierarchy == true)
        pullTrigger.SetActive(false);
    }

    public void ShowSetupButtons()
    {
        pass.SetActive(true);
        loadGun.SetActive(true);
    }

    public void ShowDecisionButtons()
    {
        dropOut.SetActive(true);
        pullTrigger.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
