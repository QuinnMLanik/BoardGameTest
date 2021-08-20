using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player
{
    // I'm sorry for this line
    public bool canTradeForVP => 
        this.Corn > GameManager.Instance.cornVictory.Current || this.Fuel > GameManager.Instance.fuelVictory.Current || this.Beef > GameManager.Instance.beefVictory.Current;
    public int Corn { get; protected set; }
    public int Fuel { get; protected set; }
    public int Cattle { get; protected set; }
    public int Beef { get; protected set; }
    public int id;

    public Space currentSpace = Board.StartingSpace;

    public int VictoryPoints { get; set; }

    public Player(int id)
    {
        this.id = id;
    }

    protected void ProduceCorn()
    {
        Corn += GameManager.Instance.cornProduction.Current;
        Debug.Log(string.Format("Player %d gained %d Corn for a total of %d", this.id, GameManager.Instance.cornProduction.Current, Corn));
    }

    protected void ProduceFuel()
    {
        if(Corn >= 5)
        {
            Corn -= 5;
            Fuel += GameManager.Instance.fuelProduction.Current;
            Debug.Log(string.Format("Player %d gained %d Fuel at a cost of 5 Corn for a total of %d Fuel and %d Corn", this.id, GameManager.Instance.fuelProduction.Current, Fuel, Corn));
        }
    }

    protected void ProduceCattle()
    {
        if(Corn >= 5)
        {
            Corn -= 5;
            Cattle += GameManager.Instance.cattleProduction.Current;
            Debug.Log(string.Format("Player %d gained %d Cattle at a cost of 5 Corn for a total of %d Cattle and %d Corn", this.id, GameManager.Instance.cattleProduction.Current, Cattle, Corn));
        }
    }

    protected void ProduceBeef()
    {
        if(Cattle >= 3)
        {
            Cattle -= 3;
            Beef += GameManager.Instance.beefProduction.Current;
            Debug.Log(string.Format("Player %d gained %d Beef at a cost of 5 Corn for a total of %d Beef and %d Corn", this.id, GameManager.Instance.beefProduction.Current, Beef, Corn));
        }
    }

    /// <summary>
    /// Exchanges the player's Corn for a VP at the rate determined by the GameManager
    /// </summary>
    /// <returns>true if a VP is successfully purchased, false otherwise</returns>
    public bool TradeCornForVP()
    {
        if(Corn >= GameManager.Instance.cornVictory.Current)
        {
            Corn -= GameManager.Instance.cornVictory.Current;
            VictoryPoints += 1;

            GameManager.Instance.cornDemand.DecreaseBy(1);
            Debug.Log(string.Format("Player %d now has %d VPs, and %d Corn remaining.", this.id, this.VictoryPoints, this.Corn));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Exchanges the player's Beef for a VP at the rate determined by the GameManager
    /// </summary>
    /// <returns>true if a VP is successfully purchased, false otherwise</returns>
    public bool TradeBeefForVP()
    {
        if (Beef >= GameManager.Instance.beefVictory.Current)
        {
            Beef -= GameManager.Instance.beefVictory.Current;
            VictoryPoints += 1;
            GameManager.Instance.beefDemand.DecreaseBy(1);
            Debug.Log(string.Format("Player %d now has %d VPs, and %d Beef remaining.", this.id, this.VictoryPoints, this.Beef));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Exchanges the player's Fuel for a VP at the rate determined by the GameManager
    /// </summary>
    /// <returns>true if a VP is successfully purchased, false otherwise</returns>
    public bool TradeFuelForVP()
    {
        if (Corn >= GameManager.Instance.fuelVictory.Current)
        {
            Corn -= GameManager.Instance.fuelVictory.Current;
            VictoryPoints += 1;

            GameManager.Instance.fuelDemand.DecreaseBy(1);
            Debug.Log(string.Format("Player %d now has %d VPs, and %d Fuel remaining.", this.id, this.VictoryPoints, this.Fuel));
            return true;
        }
        return false;
    }

    public virtual void Move(int dist)
    {
        for (int i = 0; i < dist; i++)
        {
            // Check if current space has an option of what to do or not
            if (currentSpace.hasOption)
            {
                int numOptions = currentSpace.connectedSpaces.Count;
                // Currently just makes a random choice. At some point, should add logic to make a smart decision.
                int choice = Random.Range(0, numOptions - 1);
                currentSpace = currentSpace.connectedSpaces[choice];
            }
            else
            {
                currentSpace = currentSpace.connectedSpaces[0];
            }

            // This allows the player to decide as they pass the space whether or not to make the trades (if relevant)
            if(currentSpace.effect.Type is SpaceEffect.StateType.ProduceBeef)
            {
                ProduceBeef();
            }
            else if(currentSpace.effect.Type is SpaceEffect.StateType.ProduceCattle)
            {
                ProduceCattle();
            }
            else if(currentSpace.effect.Type is SpaceEffect.StateType.ProduceCorn)
            {
                ProduceCorn();
            }
            else if(currentSpace.effect.Type is SpaceEffect.StateType.ProduceFuel)
            {
                ProduceFuel();
            }
        }
    }

    /// <summary>
    /// Purchases as many victory points as possible with the player's current resources. First trades Beef, then once the player doesn't have enough beef left it trades Fuel, the Corn.
    /// </summary>
    public void TradeForVPs()
    {
        /*
         * In the future, be wary of situations like the following:
         * Say the AI determines that despite having the necessary resources to do so, now is not a good time to buy a resource.
         * Currently, the logic in this function will end up in an infinite loop because nothing would break the deadlock of 
         * "stay in this loop as long as you can purchase VPs" vs "Now isn't a good time to buy VPs"
         * 
         */
        while(canTradeForVP)
        {
            // TODO: This method should eventually run some maths to figure out if it's currently a good time to trade or not before committing to each of these
            //       (may be able to be implemented within the TradeXForVP methods -- This should be better.)
            if (TradeBeefForVP()) continue;
            if (TradeFuelForVP()) continue;
            if (TradeCornForVP()) continue;
        }
    }

    public void ChooseTeleportDestination()
    {
        // For now, let's just have them always teleport to the Corn space
        List<Space> teleportOptions = new List<Space>(Board.TeleportSpaces);
        currentSpace = teleportOptions[0];

        //// Remove current space from consideration
        //teleportOptions.Remove(currentSpace);
    }
}
