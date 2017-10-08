using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour
{

    public Text statusText;
    public GameObject tryAgain;
    public int handValue;
    public string loadScene;
    public int playerLoss = 0;
    public int dealerLoss = 0;

    void Update()
    {
        if(playerLoss == 5)
        {
            PlayerDies();
        }

        if(dealerLoss == 5)
        {
            DealerDies();
        }
    }

    public void PlayerBusted()
    {
        HidePlayerButtons();
        GameOverText("YOU BUST", Color.red);
    }

    public void DealerBusted()
    {
        GameOverText("DEALER BUSTS!", Color.green);
    }

    public void PlayerWin()
    {
        GameOverText("YOU WIN!", Color.green);
    }

    public void PlayerLose()
    {
        GameOverText("YOU LOSE.", Color.red);
    }

    public void PlayerDies()
    {
        playerLoss = 0;
        dealerLoss = 0;
        GameOverText("YOU DIED", Color.red);
    }

    public void DealerDies()
    {
        playerLoss = 0;
        dealerLoss = 0;
        GameOverText("DEALER DIED", Color.red);
    }


    public void BlackJack()
    {
        GameOverText("Black Jack!", Color.green);
        HidePlayerButtons();
    }

    public void GameOverText(string str, Color color)
    {
        statusText.text = str;
        statusText.color = color;

        tryAgain.SetActive(true);
    }

    public void HidePlayerButtons()
    {
        GameObject.Find("HitButton").SetActive(false);
        GameObject.Find("StayButton").SetActive(false);
    }

    public void TryAgain()
    {
        statusText.text = "";
        tryAgain.SetActive(false);
        SceneManager.LoadScene(loadScene);
    }

    public virtual int GetHandValue(List<DeckOfCards.Card> hand)
    {
        handValue = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue();
        }

        //if we've found the handValue and it's over 21
        //then we want to make sure it's not because of an Ace
        //so we turn handValue back to 0
        //and run it again, but if the card is an Ace
        //we only add 1 to the handvalue
        //else, do nothing
        if (handValue > 21)
        {
            handValue = 0;
            foreach (DeckOfCards.Card handCard in hand)
            {
                if (handCard.cardNum == DeckOfCards.Card.Type.A)
                {
                    handValue += 1;
                }
                else
                {
                    handValue += handCard.GetCardHighValue();
                }
            }
        }
        return handValue;
    }
}
