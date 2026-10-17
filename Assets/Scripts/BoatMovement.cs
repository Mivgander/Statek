using UnityEngine;

/// <summary>
/// Moves and steers the boat from player input, using the
/// speed modifiers reported by the sail.
/// </summary>
public class BoatMovement : MonoBehaviour
{
    [Header("Movement Modifiers")]
    [SerializeField]
    private float _maxMovementSpeed = 10f;

    [SerializeField]
    private float _movementSpeedChangeMultiplier = 0.2f;

    [Header("Rotation Modifiers")]
    [SerializeField]
    private float _maxRotationSpeed = 50f;

    [SerializeField]
    private float _minRotationSpeed = -50f;

    [SerializeField]
    private float _rotationChangeSpeedMultiplier = 0.2f;

    [Header("References")]
    [SerializeField]
    private MastManager _mast;

    private BoatInput _input;

    private float _currentMaxMovementSpeed;
    private float _currentMovementSpeed;
    private float _currentRotationSpeed;

    private void Reset()
    {
        _input = GetComponentInParent<BoatInput>();
    }

    private void Awake()
    {
        if (_input == null)
        {
            _input = GetComponentInParent<BoatInput>();
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        _currentMaxMovementSpeed = _maxMovementSpeed
            * _mast.CurrentSailSpeedModifier
            * _mast.CurrentWindSpeedModifier;
        _currentMovementSpeed = Mathf.MoveTowards(
            _currentMovementSpeed,
            _currentMaxMovementSpeed,
            _movementSpeedChangeMultiplier
        );

        float translation = _currentMovementSpeed * Time.fixedDeltaTime;
        transform.Translate(translation, 0f, 0f);
    }

    private void UpdateRotation()
    {
        _currentRotationSpeed += _input.SteerBoat.x * _rotationChangeSpeedMultiplier;
        _currentRotationSpeed = Mathf.Clamp(
            _currentRotationSpeed,
            _minRotationSpeed,
            _maxRotationSpeed
        );

        float rotation = _currentRotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, rotation, 0f);
    }
}
