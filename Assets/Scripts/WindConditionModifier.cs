using System;
using UnityEngine;

/// <summary>
/// Boat speed modifier that applies while the dot product of
/// the sail direction and the wind direction is at least
/// <see cref="MinWindDot"/>. The first matching entry in the
/// list wins.
/// </summary>
[Serializable]
public struct WindConditionModifier
{
    /// <summary>
    /// Lowest sail-to-wind dot product (-1 to 1) this
    /// modifier applies to.
    /// </summary>
    [Range(-1f, 1f)]
    public float MinWindDot;

    /// <summary>
    /// Multiplier applied to the boat's maximum speed.
    /// </summary>
    public float SpeedModifier;
}
