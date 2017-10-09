using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour
{

    public Text cardNumUI;
    public Image cardImageUI;
    public Sprite[] cardSuits;
    public AudioClip drawCard;
    AudioSource source;

    //inner class. a class inside the deckOfCards script
    //to refer to it, you have to say "DeckOfCards.Card"
    //purely for architecture and logic
    public class Card
    {

        public enum Suit
        {
            SPADES, 	//0
            HEARTS,     //1
            DIAMONDS,   //2
            CLUBS       //3
        };

        public enum Type
        {
            TWO = 2,
            THREE = 3,
            FOUR = 4,
            FIVE = 5,
            SIX = 6,
            SEVEN = 7,
            EIGHT = 8,
            NINE = 9,
            TEN = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        };

        public Type cardNum;

        public Suit suit;

        public Card(Type cardNum, Suit suit)
        {
            this.cardNum = cardNum;
            this.suit = suit;
        }

        public override string ToString()
        {
            return "The " + cardNum + " of " + suit;
        }

        public int GetCardHighValue()
        {
            BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();
            int val;

            switch (cardNum)
            {
                //change this here so that the A can be 11 or 1
                case Type.A:
                    if (manager.handValue + 11 > 21) val = 1;
                    else val = 11;
                    break;
                case Type.K:
                case Type.Q:
                case Type.J:
                    val = 10;
                    break;
                default:
                    val = (int)cardNum;
                    break;
            }

            return val;
        }
    }

    public static ShuffleBag<Card> deck;
    public static DeckOfCards deckScript;

    // Use this for initialization
    void Awake()
    {

        //if the deck does not exist
        //make a new deck
        //but we wanna make the deck a singleton
        //if we don't, then every time we load a new scene, it's going to make a new shufflebag
        //so we're not actually reusing the same deck, rather just making a new one every time
        //so we make the canvas attached to the deck a singleton
        //if the deck DOES exist, then on start, destroy the canvas
        if (!IsValidDeck())
        {
            deck = new ShuffleBag<Card>();
            //DontDestroyOnLoad(transform.root.gameObject);
            AddCardsToDeck();
        }
        //else Destroy(gameObject);

        //if there are less than 20 cards left in the deck, reshuffle everything
        //since the shuffleBag doesn't actually remove the card
        //we have to keep track of how many cards are still left in the deck
        //if we've drawn enough cards that there are less than 20, then make a new shufflebag
        //because I don't know how to reset the cursor manually
        //then reset cardsDrawn to 0 because the next deck hasn't been touched
        if (deck.Cursor < 20)
        {
            deck = null;
            deck = new ShuffleBag<Card>();
            AddCardsToDeck();
        }

        source = GetComponent<AudioSource>();
    }

    protected virtual bool IsValidDeck()
    {
        return deck != null;
    }

    //the game should be playe with 4 decks,
    //so we run this 4 times.
    protected virtual void AddCardsToDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type)))
                {
                    deck.Add(new Card(type, suit));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual Card DrawCard()
    {
        Debug.Log(deck.Cursor);
        Card nextCard = deck.Next();
        source.clip = drawCard;
        source.Play();
        return nextCard;
    }


    public string GetNumberString(Card card)
    {
        if (card.cardNum.GetHashCode() <= 10)
        {
            return card.cardNum.GetHashCode() + "";
        }
        else
        {
            return card.cardNum + "";
        }
    }

    public Sprite GetSuitSprite(Card card)
    {
        return cardSuits[card.suit.GetHashCode()];
    }
}
