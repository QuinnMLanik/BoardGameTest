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
    private Space BridgeFromFuelToMarket = new Space(SpaceEffect.None);
    private Space BridgeFromCattleToBeef = new Space(SpaceEffect.None);
    private Space BridgeFromCattleToMarket = new Space(SpaceEffect.None);
    private Space BridgeFromBeefToMarket = new Space(SpaceEffect.None);
    
    public List<Space> teleportSpaces;
    public List<Player> players;

    public Board()
    {
        TeleportSpaces = new List<Space>();
        SetUpCornArea();
        SetUpCattleArea();
        SetUpBeefArea();
        SetUpFuelArea();
    }

    private void SetUpCornArea()
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

    private void SetUpFuelArea()
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
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        BridgeFromCornToFuel.AddConnection(CurrentSpace);
    }

    private void SetUpCattleArea()
    {
        Space CattleProduction = new Space(SpaceEffect.ProduceCattle);
        CurrentSpace = CattleProduction;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromCattleToMarket }, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCattleCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromCattleToBeef }, SpaceEffect.DrawCattleCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCattleCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport);
        TeleportSpaces.Add(CurrentSpace);

        Space CattleTeleport = CurrentSpace;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CattleProduction.AddConnection(CurrentSpace);

        CurrentSpace = new Space(CattleTeleport, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        BridgeFromCornToCattle.AddConnection(CurrentSpace);
    }

    private void SetUpBeefArea()
    {
        Space BeefProduction = new Space(SpaceEffect.ProduceBeef);
        CurrentSpace = BeefProduction;
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromBeefToMarket }, SpaceEffect.DrawBeefCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawBeefCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawBeefCard);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport);
        TeleportSpaces.Add(CurrentSpace);

        Space BeefTeleport = CurrentSpace;

        CurrentSpace = new Space(BeefTeleport, SpaceEffect.None);
        BeefTeleport.AddConnection(CurrentSpace);

        CurrentSpace = new Space(BeefTeleport, SpaceEffect.None);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None);
        BridgeFromBeefToMarket.AddConnection(CurrentSpace);
    }

    private void SetUpMarket()
    {
        Space Market = new Space(SpaceEffect.Market);
        TeleportSpaces.Add(Market);

        CurrentSpace = new Space(Market, SpaceEffect.None);
        BridgeFromCornToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None);
        BridgeFromFuelToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None);
        BridgeFromCattleToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None);
        BridgeFromBeefToMarket.AddConnection(CurrentSpace);
    }
}
