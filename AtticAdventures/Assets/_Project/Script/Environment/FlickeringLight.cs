using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 300f;
    public float maxIntensity = 1000f;
    public float flickerSpeed = 0.1f;

    private float targetIntensity;
    private float timeToNextFlicker;

    void Start()
    {
        SetRandomFlicker();
    }

    void Update()
    {
        if (flickeringLight == null) return;

        HDAdditionalLightData lightData;
        lightData = flickeringLight.GetComponent<HDAdditionalLightData>();
        lightData.intensity = Mathf.Lerp(flickeringLight.intensity, targetIntensity, flickerSpeed * Time.deltaTime);

        timeToNextFlicker -= Time.deltaTime;
        if (timeToNextFlicker <= 0)
        {
            SetRandomFlicker();
        }
    }

    void SetRandomFlicker()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        timeToNextFlicker = Random.Range(0.05f, 0.2f);
    }
}
