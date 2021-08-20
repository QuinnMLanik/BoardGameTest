using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class used to read in the cards from JSON and convert them into game objects
/// </summary>
public class CardParser
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
        
        public CardData(int id, string cardUseType, string cardName, string description, int effectId, int quantity)
        {
            this.cardId = id;
            this.cardUseType = cardUseType;
            this.cardName = cardName;
            this.description = description;
            this.effectId = effectId;
            this.quantity = quantity;
        }
    }
    
    public static Deck ParseDeck(string filepath)
    {
        List<Card> cards = new List<Card>();
        Deck.DeckType deckType = 0;
        if (filepath == "Assets/Resources/CornCards.json")
        {
            deckType = Deck.DeckType.Corn;
        }
        if (filepath == "Assets/Resources/CattleCards.json")
        {
            deckType = Deck.DeckType.Cattle;
        }
        if (filepath == "Assets/Resources/BeefCards.json")
        {
            deckType = Deck.DeckType.Beef;
        }
        if (filepath == "Assets/Resources/FuelCards.json")
        {
            deckType = Deck.DeckType.Fuel;
        }
        if (filepath == "Assets/Resources/SpecialCards.json")
        {
            deckType = Deck.DeckType.Special;
        }

        CardData day = new CardData(1, "ImmediateOnSelf", "Sunny Day", "It's a nice day", 0, 20);
        //Debug.Log(JsonUtility.ToJson(day));
        CardData rain = new CardData(2, "ImmediateOnSelf", "Rainy Day", "It's a dreary day", 0, 20);
        //Debug.Log(JsonUtility.ToJson(rain));

        // =============================================== For some reason, none of this garbage works ==============================================
        ////string toSerialize;
        //string json = "[" + JsonUtility.ToJson(day) + "," + JsonUtility.ToJson(rain) + "]";
        //Debug.Log(json);
        //File.WriteAllText(filepath, json);

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

        //string fileJson = File.ReadAllText(filepath);
        //CardData[] temp = JsonUtility.FromJson<CardData[]>(json);

        //============================================= End "For some reason, none of this garbage works" =========================================


        List<CardData> temp = new List<CardData>() { day, rain };
        // Create Cards out of CardData
        foreach(CardData cd in temp)
        {
            // Make sure to create actual duplicate cards for those that have more than one
            for(int i = 0; i < cd.quantity; i++)
            {
                Card c = new Card(cd.cardId, cd.cardName, cd.description, GameManager.Instance.GetActionFromID(cd.effectId), Card.GetUseTypeFromString(cd.cardUseType));
                cards.Add(c);
            }
        }
        Debug.Log(filepath + " cards loaded. Count: " + cards.Count);
        // Create and return Deck
        return new Deck(cards, deckType);
    }
}
