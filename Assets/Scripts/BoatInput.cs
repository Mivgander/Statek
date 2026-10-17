using UnityEngine;

/// <summary>
/// Owns the single shared <see cref="InputActions"/> instance for the
/// boat and exposes the current player input to other components.
/// </summary>
public class BoatInput : MonoBehaviour
{
    private InputActions _actions;

    /// <summary>
    /// Gets the current boat steering input (x = turn).
    /// </summary>
    public Vector2 SteerBoat => _actions.Player.SteerBoat.ReadValue<Vector2>();

    /// <summary>
    /// Gets the current sail input (x = mast rotation, y = sail height).
    /// </summary>
    public Vector2 SteerSail => _actions.Player.SteerSail.ReadValue<Vector2>();

    /// <summary>
    /// Gets the shared actions, e.g. for rebinding or switching maps.
    /// </summary>
    public InputActions Actions => _actions;

    private void Awake()
    {
        _actions = new();
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
    }
}
