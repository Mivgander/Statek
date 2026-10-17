using System;

/// <summary>
/// Global events that broadcast changes to the sail state.
/// </summary>
public static class GlobalEvents
{
    /// <summary>
    /// Raised when the sail is raised or lowered. The
    /// argument is how far the sail is open, from 0 (closed)
    /// to 1 (fully open).
    /// </summary>
    public static event Action<float> SailHeightChanged;

    /// <summary>
    /// Raised when the mast rotates. The argument is the
    /// mast's local Y rotation in degrees.
    /// </summary>
    public static event Action<float> MastRotationChanged;

    /// <summary>
    /// Raises <see cref="SailHeightChanged"/>.
    /// </summary>
    /// <param name="newHeight01">
    /// How far the sail is open, from 0 to 1.
    /// </param>
    public static void RaiseSailHeightChanged(float newHeight01)
    {
        SailHeightChanged?.Invoke(newHeight01);
    }

    /// <summary>
    /// Raises <see cref="MastRotationChanged"/>.
    /// </summary>
    /// <param name="newYRotation">
    /// The sail's local Y rotation in degrees.
    /// </param>
    public static void RaiseMastRotationChanged(float newYRotation)
    {
        MastRotationChanged?.Invoke(newYRotation);
    }
}
