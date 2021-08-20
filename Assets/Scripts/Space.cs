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
    public SpaceEffect effect;
    public List<Space> connectedSpaces;
    public bool hasOption;
    private AreaType areaType;

    public Space(SpaceEffect effect, AreaType areaType)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>();
        hasOption = connectedSpaces.Count > 1 ? true : false;
        this.areaType = areaType;
    }
    
    public Space(Space connectedSpace, SpaceEffect effect, AreaType areaType)
    {
        this.effect = effect;
        this.connectedSpaces = new List<Space>() { connectedSpace };
        hasOption = connectedSpaces.Count > 1 ? true : false;
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
        hasOption = connectedSpaces.Count > 1 ? true : false;
        this.areaType = areaType;
    }

    public void AddConnection(Space connection)
    {
        this.connectedSpaces.Add(connection);
    }
}
