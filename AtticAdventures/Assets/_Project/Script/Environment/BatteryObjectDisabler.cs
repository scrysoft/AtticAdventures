using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>(6);
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
        }

        CapsuleCollider capsuleCollider = copy.GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            capsuleCollider = copy.AddComponent<CapsuleCollider>();
        }

        copy.transform.localScale = objectSize;

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
