using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to assign a numerical value to the utility of the AI performing some action
/// </summary>
public class Appraisal : MonoBehaviour
{
    /// <summary>
    /// Base score of the choice.
    /// </summary>
    public float baseScore { get; set; } = 0;

    /// <summary>
    /// Final multiplier / veto of the choice.
    /// </summary>
    public float finalMultiplier { get; set; } = 1;
}
