
using UnityEngine;

public class OscillateZ : MonoBehaviour
{
    public float amplitude = 1.0f;
    public float frequency = 1.0f;

    private float currentTime = 0.0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        float zPos = amplitude * Mathf.Sin(frequency * currentTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
