using UnityEngine;

public class WindManager : MonoBehaviour
{
    public Vector2 windDirection { get; private set; } = Vector2.zero;

    public static WindManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void OnEnable()
    {
        InvokeRepeating("GenerateRandomDirection", 0, 5);
    }

    private void OnDisable()
    {
        CancelInvoke("GenerateRandomDirection");
    }

    private void GenerateRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        windDirection = new Vector2(x, y);
    }
}
