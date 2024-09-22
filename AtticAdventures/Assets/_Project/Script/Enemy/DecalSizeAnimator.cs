using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalSizeAnimator : MonoBehaviour
{
    public DecalProjector decalProjector; // Dein Decal Projector
    public float duration = 2f; // Zeitspanne für das Wachstum

    [SerializeField] Vector3 initialSize = new Vector3(2f, 2f, 40f);

    void Start()
    {
        // Starte die Coroutine zum Animieren
        StartCoroutine(AnimateDecalSize());
    }

    private IEnumerator AnimateDecalSize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Berechne den progressiven Wert für Ease-In
            float t = elapsedTime / duration;
            float easedT = t * t; // Quadratische Ease-In
            decalProjector.size = Vector3.Lerp(Vector3.zero, initialSize, easedT);

            elapsedTime += Time.deltaTime;
            yield return null; // Warten bis zum nächsten Frame
        }

        // Stelle sicher, dass die Größe am Ende exakt die initiale Größe hat
        decalProjector.size = initialSize;
    }
}
