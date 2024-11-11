using UnityEngine;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    private Material material;
    private Color originalColor;
    // private Color flashColor = new Color(0.46f, 0f, 0f, 0.64f); // Red
    private Color flashColor = new Color(1f, 1f, 1f, 1f); // White
    private Coroutine flashCoroutine;
    private float flashIntensity = 0.5f;

    private float flashDuration = 0.05f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            material = renderer.material;
            originalColor = material.GetColor("_EmissiveColor");
        }
    }

    public void TriggerFlash()
    {
        if (material != null)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(FlashRoutine(flashColor));
        }
    }

    private IEnumerator FlashRoutine(Color flashColor)
    {
        material.SetColor("_EmissiveColor", flashColor * flashIntensity);

        yield return new WaitForSeconds(flashDuration);

        material.SetColor("_EmissiveColor", Color.black);
        flashCoroutine = null;
    }
}
