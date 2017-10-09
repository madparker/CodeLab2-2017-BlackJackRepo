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
    public Image playerBlood;
    public Image dealerBlood;
    public AudioClip gunShot;
    AudioSource source;
    public GameObject playerDialogue;
    public GameObject dealerDialogue;
    public string[] playerLines;
    public string[] dealerLines;
    private char currentLetter;
    public bool startingDialogue;
    public bool readyForNextChar;
    private int charCount;
    private int lineIndex;
    public GameObject eyes;
    public bool startingGame;
    float timeElapsed;

    void Start()
    {
        playerDialogue.SetActive(false);
        dealerDialogue.SetActive(false);
        HidePlayerButtons();
        GameObject death = GameObject.Find("Death");
        death.GetComponent<Image>().color = new Color(0, 0, 0, 255);
    }

    void Update()
    {
        if (startingGame)
        {
            Debug.Log("opening eyes");
            GameObject death = GameObject.Find("Death");
            death.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
            GameObject.Find("StartButton").SetActive(false);
            startingGame = false;
            StartCoroutine(StartDialogue(2, 0.05f, 0, dealerDialogue, dealerLines));
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    startingDialogue = true;
        //    playerDialogue.GetComponentInChildren<Text>().text = "";
        //}
        //if (startingDialogue == true && charCount < playerLines[lineIndex].Length)
        //{
        //    playerDialogue.SetActive(true);
        //    currentLetter = playerLines[lineIndex][charCount];
        //    playerDialogue.GetComponentInChildren<Text>().text += currentLetter;
        //    startingDialogue = false;
        //    StartCoroutine(WaitForChar(0.01f));
        //}

        if (playerLoss == 5)
        {
            PlayerDies();
        }

        if (dealerLoss == 5)
        {
            DealerDies();
        }
    }

    IEnumerator StartDialogue(float time, float textSpeed, int lineIndex, GameObject character, string[] lines)
    {
        yield return new WaitForSeconds(time);
        character.GetComponentInChildren<Text>().text = "";
        character.SetActive(true);
        while (charCount < lines[lineIndex].Length)
        {
            currentLetter = lines[lineIndex][charCount];
            character.GetComponentInChildren<Text>().text += currentLetter;
            StartCoroutine(WaitForChar(0, lineIndex, lines));
            yield return new WaitForSeconds(textSpeed);
            yield return null;
        }
        yield break;
    }


    IEnumerator WaitForChar(float time, int lineIndex, string[] lines)
    {
        yield return new WaitForSeconds(time);
        if (charCount < lines[lineIndex].Length)
        {
            charCount++;
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
        playerBlood = GameObject.Find("PlayerBloodSplatter").GetComponent<Image>();
        source = GetComponent<AudioSource>();
        source.clip = gunShot;
        source.Play();
        playerBlood.enabled = true;
        eyes.GetComponent<AudioSource>().Stop();
        tryAgain.SetActive(false);
        StartCoroutine(WaitToDie(1));
    }

    public void DealerDies()
    {
        playerLoss = 0;
        dealerLoss = 0;
        GameOverText("DEALER DIED", Color.red);
        dealerBlood = GameObject.Find("DealerBloodSplatter").GetComponent<Image>();
        source = GetComponent<AudioSource>();
        source.clip = gunShot;
        source.Play();
        dealerBlood.enabled = true;
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

    public void ShowPlayerButtons()
    {
        GameObject.Find("HitButton").SetActive(true);
        GameObject.Find("StayButton").SetActive(true);
    }

    public void TryAgain()
    {
        statusText.text = "";
        //ShowPlayerButtons();
        tryAgain.SetActive(false);
        SceneManager.LoadScene(loadScene);
    }

    public void OpenEyes()
    {
        startingGame = true;
    }

    IEnumerator WaitToDie(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject death = GameObject.Find("Death");
        death.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        yield break;
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
