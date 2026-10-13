using UnityEngine;

public class WindIndicator : MonoBehaviour
{
    [SerializeField] private Transform windImage;

    private void Update()
    {
        Vector3 target = new Vector3(
            windImage.position.x + WindManager.Instance.windDirection.x,
            windImage.position.y,
            windImage.position.z + WindManager.Instance.windDirection.z
        );
        Vector3 direction = Vector3.Normalize(target - windImage.position);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        windImage.rotation = Quaternion.Euler(90f, angle, 0f);
    }
}
