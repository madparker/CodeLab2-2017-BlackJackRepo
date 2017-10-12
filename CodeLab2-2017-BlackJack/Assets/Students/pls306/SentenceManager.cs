using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SentenceManager : MonoBehaviour {

	public Text text;
	protected string newSentence;
	protected SentenceBlocks subjectBag;
	protected SentenceBlocks verbBag;
	protected SentenceBlocks predicateBag;

	// protected string sentence;
	 
	// Use this for initialization
	void Start () {
		subjectBag = GameObject.Find("Sentence").GetComponent<SentenceBlocks>();
		verbBag = GameObject.Find("Sentence").GetComponent<SentenceBlocks>();
		predicateBag = GameObject.Find("Sentence").GetComponent<SentenceBlocks>();
		newSentence = BuildSentence();
		text.text = newSentence; 
	}
	
	// Update is called once per frame
	void Update () {
		text.text = newSentence;
	}

	public string BuildSentence(){
 		string sentence;
		string subject = subjectBag.DrawSubject(); 
		string verb = verbBag.DrawVerb();
		string predicate = predicateBag.DrawPredicate();
 		sentence = subject + " " + verb + " " + predicate; 
		return sentence;
	}

	public void NewSentence(){
		newSentence = BuildSentence();
	}
}
