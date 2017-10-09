//  Game Rules:
//              In Switcheroo you and your opponent simultaneously draw a card
//              The person with the higher value card is in the lead
//              The person that is behind has the opportunity to "Switcheroo"
//              A "Switcheroo" is when you keep drawing cards form your deck
//              up unitl the value of the first card.
//              After both players have passed their opportunity to "Switcheroo"
//              The round is over and the player with the higher value card wins
//              The game is over when one person's deck has been exhuasted
//
//              The person with the most round wins, wins the game
//
//              If there is a tie, no one gets the win
//
//              No Face cards allowed
//
//      Assets: New Card back
//              New

namespace Chrs
{
    public class SwitcherooDeck : ChrsDeckOfCards
    {
        private const int TOTAL_DECKS_USED  = 1;
        private const int MIN_NUM_CARDS     = 0;

        protected override void AddCardsToDeck()
        {
            for (int i = 0; i < TOTAL_DECKS_USED; i++)
            {
                foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type)))
                    {
                        BlackJackCard newCard = new BlackJackCard(type, suit);
                        if (ValidSwitcherooCard(newCard))
                        {
                            deck.Add(newCard);
                        }
                    }
                }
            }
        }

        protected bool ValidSwitcherooCard(BlackJackCard card)
        {
            bool isValid = card.cardNum != BlackJackCard.Type.J &&
                            card.cardNum != BlackJackCard.Type.Q &&
                            card.cardNum != BlackJackCard.Type.K;

            return isValid;
        }
    }
}