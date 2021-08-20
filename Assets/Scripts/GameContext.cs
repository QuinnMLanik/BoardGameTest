using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references to the information that the AI needs in order to make decisions
/// </summary>
public class GameContext
{
    /// <summary>
    /// Reference to all the players so that we can get info that would be able to be visually determined (e.g. # resources, # VPs, etc)
    /// </summary>
    List<Player> allPlayers;

    public GameContext()
    {
        allPlayers = GameManager.Instance.players;
    }

    public int MakeMoveDecision(List<Space> options, AIPlayer decider)
    {
        // Determine where on board we are (which resource area we're in (or heading to if in-between at the moment))

        // Use AI strategy to determine whether to stay in current area or head to next one.
        return 0;
    }
}
