using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private Label sailOpenLabel;
    private Label sailRotationLabel;

    private void Awake()
    {
        panelRenderer = GetComponent<PanelRenderer>();
    }

    private void OnUIReloadedCallback(PanelRenderer panelRenderer, VisualElement rootElement, int version)
    {
        sailOpenLabel = rootElement.Q<Label>("SailOpenTextValue");
        sailRotationLabel = rootElement.Q<Label>("SailRotationTextValue");
    }

    private void OnEnable()
    {
        panelRenderer?.RegisterUIReloadCallback(OnUIReloadedCallback);

        GlobalEvents.OnSailHeightChange += OnSailHeightChange;
        GlobalEvents.OnSailRotationChange += OnSailRotationChange;
    }

    private void OnDisable()
    {
        panelRenderer?.UnregisterUIReloadCallback(OnUIReloadedCallback);

        GlobalEvents.OnSailHeightChange -= OnSailHeightChange;
        GlobalEvents.OnSailRotationChange -= OnSailRotationChange;
    }

    private void OnSailHeightChange(float newSailHeight01)
    {
        string value = Mathf.RoundToInt(newSailHeight01 * 100f).ToString();

        sailOpenLabel.text = $"{value}%";
    }

    private void OnSailRotationChange(float newSailYRotation)
    {
        string value = Mathf.RoundToInt(newSailYRotation).ToString();

        sailRotationLabel.text = $"{value}°";
    }
}
