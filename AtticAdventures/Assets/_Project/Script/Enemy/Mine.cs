using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.HighDefinition;

public class Mine : MonoBehaviour
{
    public GameObject prefabToSpawn;          // Prefab, das bei der Explosion erzeugt wird
    public GameObject additionalPrefabToSpawn; // Zusätzliches Prefab, das nach der Explosion erzeugt wird

    [SerializeField] float delay = 2.0f;
    public GameObject decalProjector;
    public GameObject decalProjectorRing;

    [SerializeField] GameObject blinkingLightObject;
    [SerializeField] float warningBlinkTimeFactor = 4f;
    private float initialBlinkTime;

    private void Start()
    {
        initialBlinkTime = blinkingLightObject.GetComponent<MeshRenderer>().material.GetFloat("_BlinkingTime");
    }

    private void ToogleDecalProjector(bool value)
    {
        if (decalProjector == null && decalProjectorRing == null) return;

        if (value == false)
        {
            decalProjector.GetComponent<DecalSizeAnimator>().ResetSize();
        }

        decalProjector.SetActive(value);
        decalProjectorRing.SetActive(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetBlinkingTime(warningBlinkTimeFactor);
            ToogleDecalProjector(true);
            Invoke("Explode", delay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetBlinkTime();
            ToogleDecalProjector(false);
            CancelInvoke("Explode");
        }
    }

    private void SetBlinkingTime(float value)
    {
        blinkingLightObject.GetComponent<MeshRenderer>().material.SetFloat("_BlinkingTime", initialBlinkTime * value);
    }

    private void ResetBlinkTime()
    {
        blinkingLightObject.GetComponent<MeshRenderer>().material.SetFloat("_BlinkingTime", initialBlinkTime);
    }

    public void Explode()
    {
        // Primäres Prefab instanziieren
        Opsive.Shared.Game.ObjectPoolBase.Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        // Zusätzliche Prefab instanziieren
        if (additionalPrefabToSpawn != null)
        {
            Quaternion upwardRotation = Quaternion.Euler(90f, 0f, 0f);
            Instantiate(additionalPrefabToSpawn, transform.position, upwardRotation);
        }

        // Mine zerstören
        Destroy(gameObject);
    }
}
