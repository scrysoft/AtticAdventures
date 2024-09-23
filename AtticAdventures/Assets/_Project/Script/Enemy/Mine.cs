using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.Rendering.DebugUI;

public class Mine : MonoBehaviour
{
    public GameObject prefabToSpawn;

    [SerializeField] float delay = 2.0f;
    public GameObject decalProjector;

    [SerializeField] GameObject blinkingLightObject;
    [SerializeField] float warningBlinkTimeFactor = 4f;
    private float initialBlinkTime;

    private void Start()
    {
        initialBlinkTime = blinkingLightObject.GetComponent<MeshRenderer>().material.GetFloat("_BlinkingTime");

        
    }

    private void ToogleDecalProjector(bool value)
    {
        if (decalProjector == null) return;

        if (value == false)
        {
            decalProjector.GetComponent<DecalSizeAnimator>().ResetSize();
        }

        decalProjector.SetActive(value);
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

        Destroy(gameObject);
    }
}
