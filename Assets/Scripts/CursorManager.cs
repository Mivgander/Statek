using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Hides the mouse cursor and keeps it confined to the
/// game window. Pressing Escape releases the cursor.
/// </summary>
public class CursorManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
