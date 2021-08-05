using System.Collections;
using System.Collections.Generic;

public class Space
{
    private SpaceEffect effect;
    private List<Space> connectedSpaces;

    public Space(SpaceEffect effect)
    {
        this.connectedSpaces = new List<Space>();
    }
    
    public Space(Space connectedSpace, SpaceEffect effect)
    {
        this.connectedSpaces = new List<Space>();
        connectedSpaces.Add(connectedSpace);
    }

    public Space(List<Space> connectedSpaces, SpaceEffect spaceEffect)
    {
        this.connectedSpaces = new List<Space>();
        foreach(Space space in connectedSpaces)
        {
            this.connectedSpaces.Add(space);
        }
        this.effect = spaceEffect;
    }

    public void AddConnection(Space connection)
    {
        this.connectedSpaces.Add(connection);
    }
}
