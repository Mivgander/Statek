using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement Modifiers")]
    [SerializeField] private float maxMovementSpeed = 10f;
    [SerializeField] private float movementSpeedChangeMultiplier = 0.2f;
    [Header("Rotation Modifiers")]
    [SerializeField] private float maxRotationSpeed = 50f;
    [SerializeField] private float minRotationSpeed = -50f;
    [SerializeField] private float rotationChangeSpeedMultiplier = 0.2f;
    [Header("Referances")]
    [SerializeField] private SailMovement sail;

    private InputActions inputActions;
    private Vector2 moveInput;
    private float currentMaxMovementSpeed = 0f;
    private float currentMovementSpeed = 0f;
    private float currentRotationSpeed = 0f;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.SteerBoat.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.SteerBoat.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        currentMaxMovementSpeed = maxMovementSpeed * sail.currentSailSpeedModifier * sail.currentWindSpeedModifier;
        if (currentMaxMovementSpeed > currentMovementSpeed)
        {
            currentMovementSpeed += movementSpeedChangeMultiplier;
            if (currentMovementSpeed > currentMaxMovementSpeed)
            {
                currentMovementSpeed = currentMaxMovementSpeed;
            }
        }
        else if (currentMaxMovementSpeed < currentMovementSpeed)
        {
            currentMovementSpeed -= movementSpeedChangeMultiplier;
            if (currentMovementSpeed < currentMaxMovementSpeed)
            {
                currentMovementSpeed = currentMaxMovementSpeed;
            }
        }
        float translation = currentMovementSpeed * Time.fixedDeltaTime;
        transform.Translate(translation, 0, 0);
    }

    private void UpdateRotation()
    {
        currentRotationSpeed += moveInput.x * rotationChangeSpeedMultiplier;
        if (currentRotationSpeed > maxRotationSpeed)
        {
            currentRotationSpeed = maxRotationSpeed;
        }
        else if (currentRotationSpeed < minRotationSpeed)
        {
            currentRotationSpeed = minRotationSpeed;
        }

        float rotation = currentRotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
