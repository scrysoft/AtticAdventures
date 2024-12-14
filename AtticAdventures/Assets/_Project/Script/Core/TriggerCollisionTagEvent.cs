using UnityEngine;
using UnityEngine.Events;


public class TriggerCollisionTagEvent : MonoBehaviour
{
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;
    public string compareTag = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(compareTag))
        {
            onPlayerEnter.Invoke();    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(compareTag))
        {
            onPlayerExit.Invoke();
        }
    }
}
