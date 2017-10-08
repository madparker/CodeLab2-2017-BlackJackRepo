using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour
{

    public Text total;
    public float xOffset;
    public float yOffset;
    protected Vector3 firstPos = Vector3.zero;
    protected GameObject cardContainer;
    public GameObject handBase;
    public int handVals;
    protected DeckOfCards deck;
    protected List<DeckOfCards.Card> hand;
    bool stay = false;
    BlackJackManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
        SetupHand();
    }

    protected virtual void SetupHand()
    {
        deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
        hand = new List<DeckOfCards.Card>();
        HitMe();
        HitMe();
        //we want to check if it's a natural blackjack at setup
        //but only if we're not the dealer
        if (this.gameObject.tag != "Dealer")
        {
            handVals = GetHandValue();
            if (handVals == 21)
            {
                //BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
                manager.dealerLoss++;
                manager.BlackJack();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HitMe()
    {
        if (!stay)
        {

            //BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
            manager.handValue = 0;

            DeckOfCards.Card card = deck.DrawCard();
            //new DeckOfCards.Card(DeckOfCards.Card.Type.A, DeckOfCards.Card.Suit.CLUBS);

            GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

            //puts the card on the table
            //takes a card, a cardObject, and then passes the hand.count so that it can put cards at the right offset
            ShowCard(card, cardObj, hand.Count);

            hand.Add(card);

            ShowValue();
        }
    }

    protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos)
    {

        //float averageXPos = 0;
        cardObj.name = card.ToString();
        cardObj.transform.SetParent(handBase.transform);
        cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        cardObj.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                xOffset + pos * 110,
                yOffset);
        //averageXPos = (firstPos.x + cardObj.GetComponent<RectTransform>().transform.position.x);
        if (hand.Count > 2)
        {
            handBase.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(handBase.GetComponent<RectTransform>().anchoredPosition.x + (xOffset / 2),
                            handBase.GetComponent<RectTransform>().anchoredPosition.y);
        }
        cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
        cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
    }

    protected virtual void ShowValue()
    {
        handVals = GetHandValue();

        total.text = "Player: " + handVals;

        if (handVals > 21)
        {
            manager.playerLoss++;
            manager.PlayerBusted();
        }
    }

    public int GetHandValue()
    {
        return manager.GetHandValue(hand);
    }
}
