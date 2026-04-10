using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float minScale = 1f;      // Scala minima
    public float maxScale = 1.2f;    // Scala massima
    public float speed = 2f;         // Velocità pulsazione

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * speed) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }
}
