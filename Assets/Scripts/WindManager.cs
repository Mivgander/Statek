using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField] private float windChangeRepeatRate = 10f;

    public Vector3 windDirection { get; private set; } = Vector3.zero;

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
        InvokeRepeating("GenerateRandomDirection", 0, windChangeRepeatRate);
    }

    private void OnDisable()
    {
        CancelInvoke("GenerateRandomDirection");
    }

    private void GenerateRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        windDirection = new Vector3(x, 0, z);
    }
}
