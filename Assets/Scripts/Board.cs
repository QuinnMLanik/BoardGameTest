using System.Collections;
using System.Collections.Generic;

public class Board
{
    public static Space StartingSpace { get; protected set; }
    public static List<Space> TeleportSpaces { get; protected set; }
    private Space CurrentSpace;
    private Space BridgeFromCornToFuel = new Space(SpaceEffect.None);
    private Space BridgeFromCornToCattle = new Space(SpaceEffect.None);
    private Space BridgeFromCornToMarket = new Space(SpaceEffect.None);
    private Space BridgeFromCattleToBeef = new Space(SpaceEffect.None);
    private Space BridgeFromBeefToMarket = new Space(SpaceEffect.None);
    private Space BridgeFromFuelToMarket = new Space(SpaceEffect.None);
    public List<Space> teleportSpaces;
    public List<Player> players;

    public void SetUpCornArea()
    {
        StartingSpace = new Space(SpaceEffect.ProduceCorn);
        CurrentSpace = StartingSpace;

        CurrentSpace = new Space(CurrentSpace,SpaceEffect.None);

        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToFuel }, SpaceEffect.DrawSpecialCard);

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);

        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToMarket }, SpaceEffect.DrawCornCard);

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);

        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToCattle }, SpaceEffect.None);

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCornCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCornCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport);
        TeleportSpaces.Add(CurrentSpace);
        StartingSpace.AddConnection(CurrentSpace);

    }

    public void SetUpFuelArea()
    {
        Space FuelProduction = new Space(SpaceEffect.ProduceFuel);
        CurrentSpace = FuelProduction;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);

        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromFuelToMarket }, SpaceEffect.Teleport);
        TeleportSpaces.Add(CurrentSpace);

        Space FuelTeleport = CurrentSpace;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        FuelProduction.AddConnection(CurrentSpace);

        CurrentSpace = new Space(FuelTeleport, SpaceEffect.None);
        CurrentSpace = new Space(FuelTeleport, SpaceEffect.None);
        BridgeFromCornToFuel.AddConnection(CurrentSpace);
    }
}
