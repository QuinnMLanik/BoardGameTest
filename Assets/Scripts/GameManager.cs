using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that handles the game logic
/// </summary>
public class GameManager
{
    public static StabilitySlider stabilitySlider = new StabilitySlider(8, 0, 8);
    public static DemandSlider cornDemand = new DemandSlider(8, -8, 0);
    public static DemandSlider fuelDemand = new DemandSlider(8, -8, 0);
    public static DemandSlider beefDemand = new DemandSlider(8, -8, 0);
    public static GeneralSlider cornProduction = new GeneralSlider(7, 3, 5);
    public static GeneralSlider fuelProduction = new GeneralSlider(5, 1, 3);
    public static GeneralSlider cattleProduction = new GeneralSlider(5, 1, 3);
    public static GeneralSlider beefProduction = new GeneralSlider(5, 1, 3);
    public static GeneralSlider cornVictory = new GeneralSlider(22, 18, 20);
    public static GeneralSlider fuelVictory = new GeneralSlider(8, 4, 6);
    public static GeneralSlider beefVictory = new GeneralSlider(6, 2, 4);
    
    public static List<Player> players;
    public static Dictionary<int, Action> cardEffectMap = new Dictionary<int, Action>();

    public static Action GetActionFromID(int cardId)
    {
        Action effect;
        cardEffectMap.TryGetValue(cardId, out effect);
        return effect;
    }
}

public abstract class Slider
{
    public int Max { get; protected set; }
    public int Min { get; protected set; }
    public int Current { get; protected set; }
    public abstract void IncreaseBy(int value);
    public abstract void DecreaseBy(int value);

}

public class GeneralSlider : Slider
{
    public GeneralSlider(int max, int min, int initial)
    {
        this.Max = max;
        this.Min = min;
        this.Current = initial;
    }
    
    public override void IncreaseBy(int value)
    {
        if (this.Current + value <= this.Max)
        {
            this.Current += value;
        }
        else
        {
            this.Current = this.Max;
        }
    }
    
    public override void DecreaseBy(int value)
    {
        if (this.Current - value > this.Min)
        {
            this.Current -= value;
        }
        else
        {
            this.Current = this.Min;
        }
    }
}

public class DemandSlider : Slider
{
    public DemandSlider(int max, int min, int initial)
    {
        this.Max = max;
        this.Min = min;
        this.Current = initial;
    }
    
    public override void IncreaseBy(int value)
    {
        while (value > 0)
        {
            if (this.Current + 1 < this.Max)
            {
                this.Current++;
                value--;
            }
            else
            {
                this.Current = this.Max;
                GameManager.stabilitySlider.DecreaseBy(1);
            }
        }
    }

    public override void DecreaseBy(int value)
    {
        while (value > 0)
        {
            if (this.Current - 1 > this.Min)
            {
                this.Current--;
                value--;
            }
            else
            {
                this.Current = this.Min;
                GameManager.stabilitySlider.DecreaseBy(1);
            }
        }
    }
}

public class StabilitySlider : Slider
{
    public StabilitySlider(int max, int min, int initial)
    {
        this.Max = max;
        this.Min = min;
        this.Current = initial;
    }
    
    public override void IncreaseBy(int value)
    {
        if (this.Current + value <= this.Max)
        {
            this.Current += value;
        }
        else
        {
            this.Current = this.Max;
        }
    }
    
    public override void DecreaseBy(int value)
    {
        if(this.Current - value > this.Min)
        {
            this.Current -= value;
        }
        else
        {
            this.Current = this.Min;
            //TODO: Womp womp
        }
    }
}