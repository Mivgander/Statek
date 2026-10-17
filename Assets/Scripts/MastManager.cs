using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates mast, raises and lowers the sail from player input,
/// and works out the boat speed modifiers for the current
/// sail and wind conditions.
/// </summary>
public class MastManager : MonoBehaviour
{
    [Header("Mast Rotation")]
    [SerializeField]
    private float _rotationSpeed = 50f;

    [SerializeField]
    private float _minAngle = -45f;

    [SerializeField]
    private float _maxAngle = 45f;

    [Header("Sail Movement")]
    [SerializeField]
    private Transform _sail;

    [SerializeField]
    private float _maxSailXScale = 0.4f;

    [SerializeField]
    private float _minSailXScale = 0.07f;

    [Header("References")]
    [SerializeField]
    private Transform _upperLog;

    [Header("Lower Log Movement")]
    [SerializeField]
    private Transform _lowerLog;

    [SerializeField]
    private float _movementSpeed = 3f;

    [SerializeField]
    private float _maxHeight = 6.8f;

    [SerializeField]
    private float _minHeight = 3.4f;

    [Header("Boat Speed Modifiers")]
    [SerializeField]
    private List<WindConditionModifier> _windBoosts = new();

    private InputActions _inputActions;
    private Vector2 _moveInput;
    private float _currentYRotation;
    private float _currentYPosition;
    private float _currentSailStatus01 = 1f;

    /// <summary>
    /// Gets the boat speed multiplier for the sail's angle
    /// to the wind.
    /// </summary>
    public float CurrentWindSpeedModifier
    {
        get;
        private set;
    } = 1f;

    /// <summary>
    /// Gets the boat speed multiplier for how far the sail
    /// is open.
    /// </summary>
    public float CurrentSailSpeedModifier
    {
        get;
        private set;
    } = 1f;

    private void Awake()
    {
        _inputActions = new();

        _inputActions.Player.SteerSail.performed +=
            ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.SteerSail.canceled +=
            ctx => _moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }

    private void Start()
    {
        _currentYRotation = transform.localEulerAngles.y;
        if (_currentYRotation > 180f)
        {
            _currentYRotation -= 360f;
        }

        _currentYPosition = _lowerLog.localPosition.y;
    }

    private void Update()
    {
        UpdateSailPosition();
        UpdateWindSpeedModifier();
        UpdateSailSpeedModifier();
    }

    private void FixedUpdate()
    {
        UpdateLowerLogPosition();
        UpdateMastRotation();
    }

    private void UpdateSailStatus()
    {
        float availableDistance = _maxHeight - _minHeight;
        float currentDistance = _maxHeight - _currentYPosition;
        _currentSailStatus01 = currentDistance / availableDistance;
    }

    private void UpdateSailPosition()
    {
        float upperLogY = _upperLog.localPosition.y;
        Vector3 clothPosition = _sail.localPosition;
        clothPosition.y = (upperLogY + _currentYPosition) / 2f;
        _sail.localPosition = clothPosition;

        Vector3 clothScale = _sail.localScale;
        clothScale.x = Mathf.Lerp(
            _minSailXScale,
            _maxSailXScale,
            _currentSailStatus01
        );
        _sail.localScale = clothScale;
    }

    private void UpdateSailSpeedModifier()
    {
        CurrentSailSpeedModifier = _currentSailStatus01;
    }

    private void UpdateWindSpeedModifier()
    {
        float currentDot = GetWindDot();
        foreach (WindConditionModifier boost in _windBoosts)
        {
            if (currentDot >= boost.MinWindDot)
            {
                CurrentWindSpeedModifier = boost.SpeedModifier;
                break;
            }
        }
    }

    private float GetWindDot()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 wind = WindManager.Instance.WindDirection;
        Vector3 position = transform.position;
        Vector3 target = new(
            position.x + wind.x,
            position.y,
            position.z + wind.z
        );
        Vector3 toTarget = Vector3.Normalize(target - position);

        return Vector3.Dot(forward, toTarget);
    }

    private void UpdateMastRotation()
    {
        float rotationChange = _moveInput.x
            * _rotationSpeed
            * Time.fixedDeltaTime;
        _currentYRotation = Mathf.Clamp(
            _currentYRotation + rotationChange,
            _minAngle,
            _maxAngle
        );

        Vector3 euler = transform.localEulerAngles;
        euler.y = _currentYRotation;
        transform.localEulerAngles = euler;

        if (rotationChange != 0f)
        {
            GlobalEvents.RaiseMastRotationChanged(_currentYRotation);
        }
    }

    private void UpdateLowerLogPosition()
    {
        float positionChange = _moveInput.y
            * _movementSpeed
            * Time.fixedDeltaTime;
        _currentYPosition = Mathf.Clamp(
            _currentYPosition + positionChange,
            _minHeight,
            _maxHeight
         );

        Vector3 lowerLogPosition = _lowerLog.localPosition;
        lowerLogPosition.y = _currentYPosition;
        _lowerLog.localPosition = lowerLogPosition;

        if (positionChange != 0f)
        {
            UpdateSailStatus();
            GlobalEvents.RaiseSailHeightChanged(_currentSailStatus01);
        }
    }
}
