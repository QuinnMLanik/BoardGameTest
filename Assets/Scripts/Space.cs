using System.Collections;
using System.Collections.Generic;

public enum AreaType
{
    Corn = 0,
    Cattle = 1,
    Beef = 2,
    Fuel = 3,
    Market = 4
}

public class Space
{
    private SpaceEffect effect;
    private List<Space> connectedSpaces;
    private AreaType areaType;

    public Space(SpaceEffect effect, AreaType areaType)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>();
        this.areaType = areaType;
    }
    
    public Space(Space connectedSpace, SpaceEffect effect, AreaType areaType)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>() { connectedSpace };
        this.areaType = areaType;
    }

    public Space(List<Space> connectedSpaces, SpaceEffect effect, AreaType areaType)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>();
        foreach(Space space in connectedSpaces)
        {
            this.connectedSpaces.Add(space);
        }
        this.areaType = areaType;
    }

    public void AddConnection(Space connection)
    {
        this.connectedSpaces.Add(connection);
    }
}
