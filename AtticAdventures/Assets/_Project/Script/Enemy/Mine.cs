using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject additionalPrefabToSpawn;

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
        Opsive.Shared.Game.ObjectPoolBase.Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        if (additionalPrefabToSpawn != null)
        {
            Quaternion upwardRotation = Quaternion.Euler(90f, 0f, 0f);
            Instantiate(additionalPrefabToSpawn, transform.position, upwardRotation);
        }

        Destroy(gameObject);
    }
}
