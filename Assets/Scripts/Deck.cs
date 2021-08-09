using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<Card> drawPile;
    private List<Card> discardPile;
    private DeckType type;

    public enum DeckType
    {
        Corn = 0,
        Cattle = 1,
        Beef = 2,
        Ethanol = 3,
        Special = 4,
    }

    public Deck(List<Card> drawPile, DeckType type)
    {
        this.drawPile = drawPile;
        this.discardPile = new List<Card>();
        this.type = type;
        Shuffle(drawPile);
    }

    /// <summary>
    /// Executes a number of random card location swaps determined by the size of the deck being shuffled
    /// </summary>
    public static void Shuffle(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            // Pick an element of the list to swap
            int k = Random.Range(0, n);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    /// <summary>
    /// Draws the first card in the deck and does not replace it
    /// </summary>
    /// <returns></returns>
    public static Card DrawCardFromDeck(Deck deck)
    {
        // In case any deck ever fully runs out of cards
        if (deck.drawPile.Count == 0)
        {
            RemakeDeck(deck);
        }

        Card draw = deck.drawPile[0];
        deck.drawPile.Remove(draw);
        return draw;
    }

    /// <summary>
    /// Refills the deck's draw pile from the cards in the discard pile, then reshuffles the draw pile.
    /// </summary>
    /// <param name="deck"></param>
    private static void RemakeDeck(Deck deck)
    {
        // Make new draw pile out of the discard pile, then reshuffle. This does/should not recapture any cards currently held by players.
        deck.drawPile.AddRange(deck.discardPile);
        Shuffle(deck.drawPile);
    }
}
