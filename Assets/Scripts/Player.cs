using System.Collections;
using System.Collections.Generic;

public class Player
{
    public int Corn { get; protected set; }
    public int Fuel { get; protected set; }
    public int Cattle { get; protected set; }
    public int Beef { get; protected set; }

    public Space currentSpace = Board.StartingSpace;

    public void ProduceCorn()
    {
        Corn += GameManager.cornProduction.Current;
    }

    public void ProduceFuel()
    {
        if(Corn >= 5)
        {
            Corn -= 5;
            Fuel += GameManager.fuelProduction.Current;
        }
    }

    public void ProduceCattle()
    {
        if(Corn >= 5)
        {
            Corn -= 5;
            Cattle += GameManager.cattleProduction.Current;
        }
    }

    public void ProduceBeef()
    {
        if(Cattle >= 3)
        {
            Cattle -= 3;
            Beef += GameManager.beefProduction.Current;
        }
    }
}
