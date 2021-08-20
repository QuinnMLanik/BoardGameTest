using System.Collections;
using System.Collections.Generic;

public class Space
{
    public SpaceEffect effect;
    public List<Space> connectedSpaces;
    public bool hasOption;

    public Space(SpaceEffect effect)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>();
        hasOption = connectedSpaces.Count > 1 ? true : false;
    }
    
    public Space(Space connectedSpace, SpaceEffect effect)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>() { connectedSpace };
        hasOption = connectedSpaces.Count > 1 ? true : false;
    }

    public Space(List<Space> connectedSpaces, SpaceEffect effect)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>();
        foreach(Space space in connectedSpaces)
        {
            this.connectedSpaces.Add(space);
        }
        hasOption = connectedSpaces.Count > 1 ? true : false;
    }

    public void AddConnection(Space connection)
    {
        this.connectedSpaces.Add(connection);
    }
}
