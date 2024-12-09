using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>(6);
    [SerializeField] private GameObject interactableTrigger;
    [SerializeField] private GameObject vfxPrefab;
    private float ejectForce = 10f;
    private Vector3 objectSize = new Vector3(2.5f, 2.5f, 2.5f);

    public UnityEvent AllObjectsDisabled;

    public void DisableNextObject()
    {
        foreach (var obj in objects)
        {
            if (obj != null && obj.activeSelf)
            {
                EjectObject(obj);
                obj.SetActive(false);
                CheckAllObjectsDisabled();
                return;
            }
        }

        CheckAllObjectsDisabled();
    }

    private void EjectObject(GameObject original)
    {
        GameObject copy = Instantiate(original, original.transform.position, original.transform.rotation);
        copy.SetActive(true);

        Rigidbody rb = copy.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = copy.AddComponent<Rigidbody>();
            rb.drag = 2f;
        }

        CapsuleCollider capsuleCollider = copy.GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            capsuleCollider = copy.AddComponent<CapsuleCollider>();
        }

        copy.transform.localScale = objectSize;

        if (interactableTrigger != null)
        {
            GameObject child1 = Instantiate(interactableTrigger, copy.transform);
            child1.transform.localPosition = Vector3.zero;
        }

        if (vfxPrefab != null)
        {
            GameObject child2 = Instantiate(vfxPrefab, copy.transform);
            child2.transform.localPosition = Vector3.zero;
            child2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        rb.AddForce(randomDirection * ejectForce, ForceMode.Impulse);

        rb.angularVelocity = Random.insideUnitSphere * 10f;
    }

    private void CheckAllObjectsDisabled()
    {
        foreach (var obj in objects)
        {
            if (obj != null && obj.activeSelf)
            {
                return;
            }
        }

        AllObjectsDisabled?.Invoke();
    }
}
