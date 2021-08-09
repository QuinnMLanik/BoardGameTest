using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private int id;
    private string cardName;
    private string description;
    private Action cardEffect;
    private UseType type;

    public enum UseType
    {
        ImmediateOnSelf = 0,
        ImmediateOnOthers = 1,
        DelayedOnSelf = 2,
        DelayedOnOthers = 3,
    }

    public Card(int id, string cardName, string description, Action cardEffect, UseType type)
    {
        this.id = id;
        this.cardName = cardName;
        this.description = description;
        this.cardEffect = cardEffect;
        this.type = type;
    }

    public static UseType GetUseTypeFromString(string type)
    {
        switch(type)
        {
            case "ImmediateOnSelf":
                return UseType.ImmediateOnSelf;
            case "ImmediateOnOthers":
                return UseType.ImmediateOnOthers;
            case "DelayedOnSelf":
                return UseType.DelayedOnSelf;
            case "DelayedOnOthers":
                return UseType.ImmediateOnOthers;
            default:
                throw new Exception("Unrecognized card use type: " + type);
        }
    }
}
