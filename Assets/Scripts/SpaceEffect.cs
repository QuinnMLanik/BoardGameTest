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
    public static SpaceEffect ProduceCattle = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect ProduceBeef = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect DrawCornCard = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect DrawFuelCard = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect DrawCattleCard = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect DrawSpecialCard = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect Teleport = new SpaceEffect(StateType.ProduceFuel);
    public static SpaceEffect Market = new SpaceEffect(StateType.ProduceFuel);
}