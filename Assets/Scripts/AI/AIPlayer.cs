using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    public GameContext context { get; set; }
    public AIPlayer(int id) : base(id)
    {

    }

    public override void Move(int dist)
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
            if (currentSpace.effect.Type is SpaceEffect.StateType.ProduceBeef)
            {
                ProduceBeef();
            }
            else if (currentSpace.effect.Type is SpaceEffect.StateType.ProduceCattle)
            {
                ProduceCattle();
            }
            else if (currentSpace.effect.Type is SpaceEffect.StateType.ProduceCorn)
            {
                ProduceCorn();
            }
            else if (currentSpace.effect.Type is SpaceEffect.StateType.ProduceFuel)
            {
                ProduceFuel();
            }
        }
    }
}
