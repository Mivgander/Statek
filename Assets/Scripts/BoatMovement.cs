using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxRotationSpeed = 50f;
    [SerializeField] private float minRotationSpeed = -50f;
    [SerializeField] private float rotationChangeSpeedMultiplier = 0.2f;
    [SerializeField] private SailMovement sail;

    private InputActions inputActions;
    private Vector2 moveInput;
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
        float translation = movementSpeed * sail.currentSailSpeedModifier * sail.currentWindSpeedModifier * Time.fixedDeltaTime;
        transform.Translate(translation, 0, 0);

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
