using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalSizeAnimator : MonoBehaviour
{
    public DecalProjector decalProjector;
    public float duration = 2f;

    [SerializeField] Vector3 initialSize = new Vector3(2f, 2f, 40f);

    private void OnEnable()
    {
        StartCoroutine(AnimateDecalSize());
    }

    private IEnumerator AnimateDecalSize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = t * t;
            decalProjector.size = Vector3.Lerp(Vector3.zero, initialSize, easedT);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        decalProjector.size = initialSize;
    }

    public void ResetSize()
    {
        decalProjector.size = new Vector3(0f, 0f, 40f);
    }
}
