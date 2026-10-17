using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Shows the current sail state in the HUD.
/// </summary>
public class UIManager : MonoBehaviour
{
    private PanelRenderer _panelRenderer;
    private Label _sailOpenLabel;
    private Label _sailRotationLabel;

    private void Awake()
    {
        _panelRenderer = GetComponent<PanelRenderer>();
    }

    private void OnUIReloadedCallback(
        PanelRenderer panelRenderer,
        VisualElement rootElement,
        int version)
    {
        _sailOpenLabel = rootElement.Q<Label>("SailOpenTextValue");
        _sailRotationLabel = rootElement.Q<Label>("SailRotationTextValue");
    }

    private void OnEnable()
    {
        _panelRenderer?.RegisterUIReloadCallback(OnUIReloadedCallback);

        GlobalEvents.SailHeightChanged += OnSailHeightChanged;
        GlobalEvents.MastRotationChanged += OnSailRotationChanged;
    }

    private void OnDisable()
    {
        _panelRenderer?.UnregisterUIReloadCallback(
            OnUIReloadedCallback);

        GlobalEvents.SailHeightChanged -= OnSailHeightChanged;
        GlobalEvents.MastRotationChanged -= OnSailRotationChanged;
    }

    private void OnSailHeightChanged(float newHeight01)
    {
        int percent = Mathf.RoundToInt(newHeight01 * 100f);
        _sailOpenLabel.text = $"{percent}%";
    }

    private void OnSailRotationChanged(float newYRotation)
    {
        int degrees = Mathf.RoundToInt(newYRotation);
        _sailRotationLabel.text = $"{degrees}°";
    }
}
