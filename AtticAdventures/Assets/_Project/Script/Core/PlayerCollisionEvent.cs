using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionEvent : MonoBehaviour
{
    public UnityEvent onPlayerHitFromAbove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsHitFromAbove(other))
            {
                onPlayerHitFromAbove.Invoke();
            }
        }
    }

    private bool IsHitFromAbove(Collider other)
    {
        return other.transform.position.y > transform.position.y;
    }
}
