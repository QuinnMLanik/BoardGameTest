using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class that handles the game logic
/// </summary>
public class GameManager : MonoBehaviour
{
    public StabilitySlider stabilitySlider;
    public DemandSlider cornDemand;
    public DemandSlider fuelDemand;
    public DemandSlider beefDemand;
    public GeneralSlider cornProduction;
    public GeneralSlider fuelProduction;
    public GeneralSlider cattleProduction;
    public GeneralSlider beefProduction;
    public GeneralSlider cornVictory;
    public GeneralSlider fuelVictory;
    public GeneralSlider beefVictory;

    public List<Player> players;
    public Board board;
    public Dictionary<int, Action> cardEffectMap = new Dictionary<int, Action>();
    public Deck deckCorn;
    public Deck deckCattle;
    public Deck deckBeef;
    public Deck deckFuel;
    public Deck deckSpecial;

    public readonly int NUM_DICE = 2;
    private int currentPlayer = -1;
    private int lastRoll = 0;
    private bool gameWon = false;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
            else
            {
                return _instance;
            }
        }
    }

    public void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        this.board = new Board();
        this.players = new List<Player>();
        this.deckCorn = CardParser.ParseDeck("Assets/Resources/CornCards.json");
        this.deckCattle = CardParser.ParseDeck("Assets/Resources/CattleCards.json");
        this.deckBeef = CardParser.ParseDeck("Assets/Resources/BeefCards.json");
        this.deckFuel = CardParser.ParseDeck("Assets/Resources/FuelCards.json");
        this.deckSpecial = CardParser.ParseDeck("Assets/Resources/SpecialCards.json");
        this.stabilitySlider = new StabilitySlider(8, 0, 8);
        this.cornDemand = new DemandSlider(8, -8, 0);
        this.fuelDemand = new DemandSlider(8, -8, 0);
        this.beefDemand = new DemandSlider(8, -8, 0);
        this.cornProduction = new GeneralSlider(7, 3, 5);
        this.fuelProduction = new GeneralSlider(5, 1, 3);
        this.cattleProduction = new GeneralSlider(5, 1, 3);
        this.beefProduction = new GeneralSlider(5, 1, 3);
        this.cornVictory = new GeneralSlider(22, 18, 20);
        this.fuelVictory = new GeneralSlider(8, 4, 6);
        this.beefVictory = new GeneralSlider(6, 2, 4);

        for (int i = 0; i < 4; i++)
        {
            players.Add(new Player(i));   // Will be given IDs 0-3 instead of 1-4
        }
    }

    public void Update()
    {
        if (!gameWon && Input.GetKeyDown(KeyCode.Space))
        {
            this.SimulateTurn();
        }
    }

    public void SimulateTurn()
    {
        // Each player plays turn
        for(int i = 0; i < 4; i++)
        {
            currentPlayer = i;
            // Roll for movement (separate rolls to keep the probabilities of real dice)
            int firstRoll = Random.Range(1, 7);
            int secondRoll = Random.Range(1, 7);
            int movement = firstRoll + secondRoll;
            Debug.Log("Rolled " + movement + " for movement");
            players[i].Move(movement);

            // Do the tile effect of whatever was landed on
            switch(players[i].currentSpace.effect.Type)
            {
                case SpaceEffect.StateType.DrawCornCard:
                    Card cornDraw = deckCorn.DrawCardFromDeck();
                    Debug.Log(string.Format("Drew card: %s\n%d cards remain in %s deck", cornDraw.cardName, deckCorn.Count, players[i].currentSpace.effect.Type.ToString()));
                    cornDraw.cardEffect.Invoke();
                    break;

                case SpaceEffect.StateType.DrawCattleCard:
                    Card cattleDraw = deckCattle.DrawCardFromDeck();
                    Debug.Log(string.Format("Drew card: %s\n%d cards remain in %s deck", cattleDraw.cardName, deckCattle.Count, players[i].currentSpace.effect.Type.ToString()));
                    cattleDraw.cardEffect.Invoke();
                    break;

                case SpaceEffect.StateType.DrawBeefCard:
                    Card beefDraw = deckBeef.DrawCardFromDeck();
                    Debug.Log(string.Format("Drew card: %s\n%d cards remain in %s deck", beefDraw.cardName, deckBeef.Count, players[i].currentSpace.effect.Type.ToString()));
                    beefDraw.cardEffect.Invoke();
                    break;

                case SpaceEffect.StateType.DrawFuelCard:
                    Card fuelDraw = deckFuel.DrawCardFromDeck();
                    Debug.Log(string.Format("Drew card: %s\n%d cards remain in %s deck", fuelDraw.cardName, deckFuel.Count, players[i].currentSpace.effect.Type.ToString()));
                    fuelDraw.cardEffect.Invoke();
                    break;

                case SpaceEffect.StateType.DrawSpecialCard:
                    Card specialDraw = deckSpecial.DrawCardFromDeck();
                    Debug.Log(string.Format("Drew card: %s\n%d cards remain in %s deck", specialDraw.cardName, deckSpecial.Count, players[i].currentSpace.effect.Type.ToString()));
                    specialDraw.cardEffect.Invoke();
                    break;

                case SpaceEffect.StateType.Market:
                    HandleMarket();
                    break;

                case SpaceEffect.StateType.Teleport:
                    HandleTeleport();
                    break;

                case SpaceEffect.StateType.None:
                    break;

                // Production is taken care of in Player.cs so we don't actually need to do anything here
                case SpaceEffect.StateType.ProduceBeef:
                    goto case SpaceEffect.StateType.None;
                case SpaceEffect.StateType.ProduceCattle:
                    goto case SpaceEffect.StateType.None;
                case SpaceEffect.StateType.ProduceCorn:
                    goto case SpaceEffect.StateType.None;
                case SpaceEffect.StateType.ProduceFuel:
                    goto case SpaceEffect.StateType.None;

                // Something went wrong
                default:
                    throw new Exception("Unrecognized space effect: " + players[i].currentSpace.effect.Type);
            }
        }
    }

    private void HandleMarket()
    {
        players[currentPlayer].TradeForVPs();
        HandleTeleport();
    }

    private void HandleTeleport()
    {
        players[currentPlayer].ChooseTeleportDestination();
    }

    public Action GetActionFromID(int cardId)
    {
        this.cardEffectMap.TryGetValue(cardId, out Action effect);
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
                GameManager.Instance.stabilitySlider.DecreaseBy(1);
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
                GameManager.Instance.stabilitySlider.DecreaseBy(1);
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