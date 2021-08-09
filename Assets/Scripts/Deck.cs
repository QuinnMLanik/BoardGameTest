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
            int k = Random.Range(0, n - 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }
}
