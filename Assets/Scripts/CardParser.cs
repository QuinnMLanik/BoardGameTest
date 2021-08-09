using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class used to read in the cards from JSON and convert them into game objects
/// </summary>
public class CardParser : MonoBehaviour
{
    /// <summary>
    /// Inner class to handle intermediate data
    /// </summary>
    public class CardData
    {
        public int cardId;
        public string cardUseType;
        public string cardName;
        public string description;
        public int effectId;
        public int quantity;
        
        public CardData(int id, string cardUseType, int quantity)
        {
            this.cardId = id;
            this.cardUseType = cardUseType;
            this.quantity = quantity;
        }
    }
    
    public Deck ParseDeck(string filepath)
    {
        List<Card> cards = new List<Card>();
        Deck.DeckType deckType = 0;
        if(filepath == "Assets/Resources/CornCards.json")
        {
            deckType = Deck.DeckType.Corn;
        }

        /* Assuming the JSON is formatted as follows:
         * cards: 
         *     {
         *         cardID: 7,
         *         cardName: "Visible foot of free will",
         *         cardUseType: "ImmediateOnSelf",
         *         description: "blah blah flavor text",
         *         effectId: 24,
         *         quantity: 1,
         *      }
         *      {...}
         *  }
         */
        
        string fileJson = File.ReadAllText(filepath);
        List<CardData> temp = JsonUtility.FromJson<List<CardData>>(fileJson);

        // Create Cards out of CardData
        foreach(CardData cd in temp)
        {
            // Make sure to create actual duplicate cards for those that have more than one
            for(int i = 0; i < cd.quantity; i++)
            {
                Card c = new Card(cd.cardId, cd.cardName, cd.description, GameManager.GetActionFromID(cd.effectId), Card.GetUseTypeFromString(cd.cardUseType));
                cards.Add(c);
            }
        }

        // Create and return Deck
        return new Deck(cards, deckType);
    }
}
