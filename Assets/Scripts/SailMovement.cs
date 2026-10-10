using UnityEngine;

public class SailMovement : MonoBehaviour
{
    [Header("Sail Rotation")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;

    [Header("Sail Movement")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxHeight = 6.8f;
    [SerializeField] private float minHeight = 3.4f;


    private InputActions inputActions;
    private Vector2 moveInput;
    private float currentYRotation = 0f;
    private float currentYPosition = 0f;

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

    private void FixedUpdate()
    {
        currentYPosition += moveInput.y * movementSpeed * Time.fixedDeltaTime;
        currentYPosition = Mathf.Clamp(currentYPosition, minHeight, maxHeight);
        objectToMove.localPosition = new Vector3(objectToMove.localPosition.x, currentYPosition, objectToMove.localPosition.z);

        currentYRotation += moveInput.x * rotationSpeed * Time.fixedDeltaTime;
        currentYRotation = Mathf.Clamp(currentYRotation, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, currentYRotation, transform.localEulerAngles.z);
    }
}
