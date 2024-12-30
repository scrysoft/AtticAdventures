using UnityEngine;
using System.Collections;

public class RandomSliderValue : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private string sliderPropertyName = "_Slider";
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;
    [SerializeField] private float minSliderValue = 0f;
    [SerializeField] private float maxSliderValue = 1f;

    Coroutine randomCoroutine;

    void Start()
    {
        targetMaterial = GetComponent<MeshRenderer>().material;
        randomCoroutine = StartCoroutine(ChangeSliderValueRandomly());
    }

    public void SetValueOnce(float value)
    {
        if (randomCoroutine != null) StopCoroutine(randomCoroutine);
        StartCoroutine(SetValueOnceRoutine(value));
    }

    IEnumerator SetValueOnceRoutine(float value)
    {
        targetMaterial.SetFloat(sliderPropertyName, value);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        randomCoroutine = StartCoroutine(ChangeSliderValueRandomly());
    }

    IEnumerator ChangeSliderValueRandomly()
    {
        while (true)
        {
            float d = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(d);
            float v = Random.Range(minSliderValue, maxSliderValue);
            if (targetMaterial && !string.IsNullOrEmpty(sliderPropertyName))
            {
                targetMaterial.SetFloat(sliderPropertyName, v);
            }
        }
    }
}
