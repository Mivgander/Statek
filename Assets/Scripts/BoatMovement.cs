using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    private InputActions inputActions;
    private Vector2 moveInput;

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
        float translation = movementSpeed * Time.fixedDeltaTime;
        transform.Translate(translation, 0, 0);

        float rotation = moveInput.x * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
