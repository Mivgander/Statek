using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WindModifierCondition
{
    [Range(-1f, 1f)]
    public float windCondition;
    [Range(0f, 10f)]
    public float speedModifier;
}

[Serializable]
public struct SailModifierCondition
{
    [Range(0f, 1f)]
    public float sailDropValue;
    [Range(0f, 10f)]
    public float speedModifier;
}

public class SailMovement : MonoBehaviour
{
    [Header("Sail Rotation")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;

    [Header("Sail Movement")]
    [SerializeField] private Transform cloth;
    [SerializeField] private float maxClothXScale = 0.4f;
    [SerializeField] private float minClothXScale = 0.07f;
    [SerializeField] private Transform upperLog;
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float maxHeight = 6.8f;
    [SerializeField] private float minHeight = 3.4f;

    [Header("Boat Speed Modifiers")]
    [SerializeField] private List<WindModifierCondition> windBoosts = new List<WindModifierCondition>();
    [SerializeField] private List<SailModifierCondition> sailBoosts = new List<SailModifierCondition>();

    public float currentWindSpeedModifier { get; private set; } = 1f;
    public float currentSailSpeedModifier { get; private set; } = 1f;

    private InputActions inputActions;
    private Vector2 moveInput;
    private float currentYRotation = 0f;
    private float currentYPosition = 0f;
    private float currentSailStatus01 = 1;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.SteerSail.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.SteerSail.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Start()
    {
        currentYRotation = transform.localEulerAngles.y;
        if (currentYRotation > 180f)
        {
            currentYRotation -= 360f;
        }

        currentYPosition = objectToMove.localPosition.y;
    }

    private void Update()
    {
        UpdateSailStatus();
        UpdateClothPosition();
        UpdateWindSpeedModifier();
        UpdateSailSpeedModifier();
    }

    private void UpdateSailStatus()
    {
        float availableDistance = maxHeight - minHeight;
        float currentDistance = maxHeight - currentYPosition;
        currentSailStatus01 = currentDistance / availableDistance;
    }

    private void UpdateClothPosition()
    {
        float newClothYPosition = (upperLog.localPosition.y + currentYPosition) / 2;
        cloth.localPosition = new Vector3(cloth.localPosition.x, newClothYPosition, cloth.localPosition.z);
        
        float newClothXSclale = Mathf.Lerp(minClothXScale, maxClothXScale, currentSailStatus01);
        cloth.localScale = new Vector3(newClothXSclale, cloth.localScale.y, cloth.localScale.z);
    }

    private void UpdateSailSpeedModifier()
    {
        foreach (SailModifierCondition sailCondition in sailBoosts)
        {
            if (currentSailStatus01 <= sailCondition.sailDropValue)
            {
                currentSailSpeedModifier = sailCondition.speedModifier;
                break;
            }
        }
    }

    private void UpdateWindSpeedModifier()
    {
        float currentDot = GetWindDot();
        foreach (WindModifierCondition windCondition in windBoosts)
        {
            if (currentDot <= windCondition.windCondition)
            {
                currentWindSpeedModifier = windCondition.speedModifier;
                break;
            }
        }
    }

    private float GetWindDot()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 target = new Vector3(
            transform.position.x + WindManager.Instance.windDirection.x,
            transform.position.y,
            transform.position.z + WindManager.Instance.windDirection.y
        );
        Vector3 toOther = Vector3.Normalize(target - transform.position);

        return Vector3.Dot(forward, toOther);
    }

    private void FixedUpdate()
    {
        float positionChange = moveInput.y * movementSpeed * Time.fixedDeltaTime;
        currentYPosition += positionChange;
        currentYPosition = Mathf.Clamp(currentYPosition, minHeight, maxHeight);
        objectToMove.localPosition = new Vector3(objectToMove.localPosition.x, currentYPosition, objectToMove.localPosition.z);
        if (positionChange != 0)
        {
            GlobalEvents.SailHeightChange(currentYPosition);
        }

        float rotationChange = moveInput.x * rotationSpeed * Time.fixedDeltaTime;
        currentYRotation += rotationChange;
        currentYRotation = Mathf.Clamp(currentYRotation, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, currentYRotation, transform.localEulerAngles.z);
        if (rotationChange != 0)
        {
            GlobalEvents.SailRotationChange(currentYRotation);
        }
    }
}
