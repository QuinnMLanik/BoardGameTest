using System.Collections;
using System.Collections.Generic;

public class Board
{
    public static Space StartingSpace { get; protected set; }
    public static List<Space> TeleportSpaces { get; protected set; }
    private Space CurrentSpace;
    private Space BridgeFromCornToFuel = new Space(SpaceEffect.None, AreaType.Fuel);
    private Space BridgeFromCornToCattle = new Space(SpaceEffect.None, AreaType.Cattle);
    private Space BridgeFromCornToMarket = new Space(SpaceEffect.None, AreaType.Market);
    private Space BridgeFromFuelToMarket = new Space(SpaceEffect.None, AreaType.Market);
    private Space BridgeFromCattleToBeef = new Space(SpaceEffect.None, AreaType.Beef);
    private Space BridgeFromCattleToMarket = new Space(SpaceEffect.None, AreaType.Market);
    private Space BridgeFromBeefToMarket = new Space(SpaceEffect.None, AreaType.Market);
    
    public List<Space> teleportSpaces;
    public List<Player> players;

    private void SetUpCornArea()
    {
        StartingSpace = new Space(SpaceEffect.ProduceCorn, AreaType.Corn);
        CurrentSpace = StartingSpace;

        CurrentSpace = new Space(CurrentSpace,SpaceEffect.None, AreaType.Corn);
        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToFuel }, SpaceEffect.DrawSpecialCard, AreaType.Corn);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Corn);
        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToMarket }, SpaceEffect.DrawCornCard, AreaType.Corn);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Corn);
        CurrentSpace = new Space(new List<Space>() { CurrentSpace, BridgeFromCornToCattle }, SpaceEffect.None, AreaType.Corn);

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCornCard, AreaType.Corn);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCornCard, AreaType.Corn);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport, AreaType.Corn);
        TeleportSpaces.Add(CurrentSpace);
        StartingSpace.AddConnection(CurrentSpace);

    }

    private void SetUpFuelArea()
    {
        Space FuelProduction = new Space(SpaceEffect.ProduceFuel, AreaType.Fuel);
        CurrentSpace = FuelProduction;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawFuelCard, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Fuel);

        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromFuelToMarket }, SpaceEffect.Teleport, AreaType.Fuel);
        TeleportSpaces.Add(CurrentSpace);

        Space FuelTeleport = CurrentSpace;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Fuel);
        FuelProduction.AddConnection(CurrentSpace);

        CurrentSpace = new Space(FuelTeleport, SpaceEffect.None, AreaType.Fuel);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Fuel);
        BridgeFromCornToFuel.AddConnection(CurrentSpace);
    }

    private void SetUpCattleArea()
    {
        Space CattleProduction = new Space(SpaceEffect.ProduceCattle, AreaType.Cattle);
        CurrentSpace = CattleProduction;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromCattleToMarket }, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCattleCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromCattleToBeef }, SpaceEffect.DrawCattleCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawCattleCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport, AreaType.Cattle);
        TeleportSpaces.Add(CurrentSpace);

        Space CattleTeleport = CurrentSpace;

        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CattleProduction.AddConnection(CurrentSpace);

        CurrentSpace = new Space(CattleTeleport, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        BridgeFromCornToCattle.AddConnection(CurrentSpace);
    }

    private void SetUpBeefArea()
    {
        Space BeefProduction = new Space(SpaceEffect.ProduceBeef, AreaType.Cattle);
        CurrentSpace = BeefProduction;
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawSpecialCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(new List<Space> { CurrentSpace, BridgeFromBeefToMarket }, SpaceEffect.DrawBeefCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawBeefCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.DrawBeefCard, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.Teleport, AreaType.Cattle);
        TeleportSpaces.Add(CurrentSpace);

        Space BeefTeleport = CurrentSpace;

        CurrentSpace = new Space(BeefTeleport, SpaceEffect.None, AreaType.Cattle);
        BeefTeleport.AddConnection(CurrentSpace);

        CurrentSpace = new Space(BeefTeleport, SpaceEffect.None, AreaType.Cattle);
        CurrentSpace = new Space(CurrentSpace, SpaceEffect.None, AreaType.Cattle);
        BridgeFromBeefToMarket.AddConnection(CurrentSpace);
    }

    private void SetUpMarket()
    {
        Space Market = new Space(SpaceEffect.Market, AreaType.Market);
        TeleportSpaces.Add(Market);

        CurrentSpace = new Space(Market, SpaceEffect.None, AreaType.Market);
        BridgeFromCornToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None, AreaType.Market);
        BridgeFromFuelToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None, AreaType.Market);
        BridgeFromCattleToMarket.AddConnection(CurrentSpace);

        CurrentSpace = new Space(Market, SpaceEffect.None, AreaType.Market);
        BridgeFromBeefToMarket.AddConnection(CurrentSpace);
    }
}
