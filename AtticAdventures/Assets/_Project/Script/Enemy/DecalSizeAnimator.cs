using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalSizeAnimator : MonoBehaviour
{
    public DecalProjector decalProjector; // Dein Decal Projector
    public float duration = 2f; // Zeitspanne f�r das Wachstum

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
            // Berechne den progressiven Wert f�r Ease-In
            float t = elapsedTime / duration;
            float easedT = t * t; // Quadratische Ease-In
            decalProjector.size = Vector3.Lerp(Vector3.zero, initialSize, easedT);

            elapsedTime += Time.deltaTime;
            yield return null; // Warten bis zum n�chsten Frame
        }

        // Stelle sicher, dass die Gr��e am Ende exakt die initiale Gr��e hat
        decalProjector.size = initialSize;
    }
}
