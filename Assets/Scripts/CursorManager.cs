using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Hides the mouse cursor and keeps it confined to the
/// game window. Escape releases the cursor and the left
/// mouse button locks it again.
/// </summary>
public class CursorManager : MonoBehaviour
{
    private InputActions _actions;

    private void Awake()
    {
        _actions = new();
    }

    private void OnEnable()
    {
        _actions.Cursor.ReleaseCursor.performed += OnReleaseCursor;
        _actions.Cursor.LockCursor.performed += OnLockCursor;
        _actions.Cursor.Enable();
    }

    private void OnDisable()
    {
        _actions.Cursor.Disable();
        _actions.Cursor.ReleaseCursor.performed -= OnReleaseCursor;
        _actions.Cursor.LockCursor.performed -= OnLockCursor;
    }

    private void Start()
    {
        LockCursor();
    }

    private void OnReleaseCursor(InputAction.CallbackContext context)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnLockCursor(InputAction.CallbackContext context)
    {
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
