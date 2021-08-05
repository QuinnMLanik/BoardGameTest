using System.Collections;
using System.Collections.Generic;

public class SpaceEffect
{
    public readonly StateType Type;

    private SpaceEffect(StateType type)
    {
        this.Type = type;
    }

    public enum StateType
    {
        None = 0,
        ProduceCorn = 1,
        ProduceFuel = 2,
        ProduceCattle = 3,
        ProduceBeef = 4,
        DrawCornCard = 5,
        DrawFuelCard = 6,
        DrawCattleCard = 7,
        DrawBeefCard = 8,
        DrawSpecialCard = 9,
        Teleport = 10,
        Market = 11
    }

    public static SpaceEffect None = new SpaceEffect(StateType.None);
    public static SpaceEffect ProduceCorn = new SpaceEffect(StateType.ProduceCorn);
    public static SpaceEffect ProduceFuel = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect ProduceCattle = new SpaceEffect(StateType.ProduceCattle);
    public static SpaceEffect ProduceBeef = new SpaceEffect(StateType.ProduceBeef);
    public static SpaceEffect DrawCornCard = new SpaceEffect(StateType.DrawCornCard);
    public static SpaceEffect DrawFuelCard = new SpaceEffect(StateType.DrawFuelCard);
    public static SpaceEffect DrawCattleCard = new SpaceEffect(StateType.DrawCattleCard);
    public static SpaceEffect DrawBeefCard = new SpaceEffect(StateType.DrawBeefCard);
    public static SpaceEffect DrawSpecialCard = new SpaceEffect(StateType.DrawSpecialCard);
    public static SpaceEffect Teleport = new SpaceEffect(StateType.Teleport);
    public static SpaceEffect Market = new SpaceEffect(StateType.Market);
}