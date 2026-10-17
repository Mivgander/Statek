using UnityEngine;

/// <summary>
/// Rotates the wind indicator image to point in the current
/// wind direction.
/// </summary>
public class WindIndicator : MonoBehaviour
{
    [SerializeField]
    private Transform _windImage;

    private void Update()
    {
        Vector3 wind = WindManager.Instance.WindDirection;
        Vector3 position = _windImage.position;
        Vector3 target = new(
            position.x + wind.x,
            position.y,
            position.z + wind.z
        );
        Vector3 direction = Vector3.Normalize(target - position);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        _windImage.rotation = Quaternion.Euler(90f, angle, 0f);
    }
}
