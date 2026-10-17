using UnityEngine;

/// <summary>
/// Singleton that picks a new random wind direction at a
/// fixed interval.
/// </summary>
public class WindManager : MonoBehaviour
{
    [SerializeField]
    private float _windChangeRepeatRate = 10f;

    /// <summary>
    /// Gets the current wind direction on the XZ plane.
    /// </summary>
    public Vector3 WindDirection { get; private set; }

    /// <summary>
    /// Gets the active wind manager in the scene.
    /// </summary>
    public static WindManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        InvokeRepeating(
            nameof(GenerateRandomDirection),
            0f,
            _windChangeRepeatRate
        );
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(GenerateRandomDirection));
    }

    private void GenerateRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        WindDirection = new(x, 0f, z);
    }
}
