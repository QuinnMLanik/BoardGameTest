using System.Collections;
using System.Collections.Generic;

public class Space
{
    private Rule effect;
    private List<Space> connectedSpaces;
    
    public Space(Space connectedSpace)
    {
        this.connectedSpaces = new List<Space>();
        connectedSpaces.Add(connectedSpace);
    }

    public Space(Space connectedSpace, Rule spaceEffect)
    {
        this.connectedSpaces = new List<Space>();
        connectedSpaces.Add(connectedSpace);
        this.effect = spaceEffect;
    }
}
