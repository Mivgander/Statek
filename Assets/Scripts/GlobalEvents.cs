using System;
using UnityEngine;

public static class GlobalEvents
{
    public static event Action<float> OnSailHeightChange;
    public static event Action<float> OnSailRotationChange;

    public static void SailHeightChange(float newSailHeight01)
    {
        OnSailHeightChange?.Invoke(newSailHeight01);
    }

    public static void SailRotationChange(float newSailYRotation)
    {
        OnSailRotationChange?.Invoke(newSailYRotation);
    }
}
