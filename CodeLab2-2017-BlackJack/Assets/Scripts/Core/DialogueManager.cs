using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public GameObject playerDialogue;
    public GameObject dealerDialogue;
    [Header("Dialogue For Intro")]
    public string[] dialogueLines;
    [Header("Dialogue For Play")]
    public string[] adviceLines;
    public string[] winOrLoseLines;
    private char currentLetter;
    private bool startingDialogue;
    private bool readyForNextChar;
    private int charCount;
    private int lineIndex;
    private bool startingGame;

    // Use this for initialization
    void Start () {


        playerDialogue.SetActive(false);
        dealerDialogue.SetActive(false);
        GameObject death = GameObject.Find("Death");
        if(SceneManager.GetActiveScene().name != "BlackjackHorror")
        {
            death.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (startingGame)
        {
            Debug.Log("opening eyes");
            GameObject death = GameObject.Find("Death");
            death.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
            GameObject.Find("StartButton").SetActive(false);
            startingGame = false;
            StartCoroutine(StartDialogue(2, 0.05f, lineIndex, dealerDialogue, dialogueLines));
        }

    }

    public IEnumerator StartDialogue(float time, float textSpeed, int lineIndex, GameObject character, string[] lines)
    {
        charCount = 0;
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

    public void StartNextLine()
    {
        lineIndex++;
        if (lineIndex == 26)
        {
            dealerDialogue.SetActive(false);
            SceneManager.LoadScene("BlackJackHorror");
        }
        else if (lineIndex == 2 || lineIndex == 6 || lineIndex == 12 || lineIndex == 16 || lineIndex == 24)
        {
            dealerDialogue.SetActive(false);
            StartCoroutine(StartDialogue(0, 0, lineIndex, playerDialogue, dialogueLines));
        }
        else
        {
            playerDialogue.SetActive(false);
            StartCoroutine(StartDialogue(0, 0.05f, lineIndex, dealerDialogue, dialogueLines));
        }
    }

    IEnumerator WaitForChar(float time, int lineIndex, string[] lines)
    {
        yield return new WaitForSeconds(time);
        if (charCount < lines[lineIndex].Length)
        {
            charCount++;
        }
    }

    public void OpenEyes()
    {
        startingGame = true;
    }
}
