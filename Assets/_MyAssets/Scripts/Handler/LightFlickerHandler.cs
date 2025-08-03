using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlickerHandler : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f; // Time between flickers

    private Light flickerLight;
    private float timer;

    void Start()
    {
        flickerLight = GetComponent<Light>();
        timer = flickerSpeed;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
            timer = flickerSpeed;
        }
    }
}
