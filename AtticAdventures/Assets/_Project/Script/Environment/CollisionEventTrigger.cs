using UnityEngine;
using UnityEngine.Events;

public class CollisionEventTrigger : MonoBehaviour
{
    public string targetTag = "";

    public UnityEvent onCollisionWithTarget;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");

        if (collision.gameObject.CompareTag(targetTag))
        {
            onCollisionWithTarget.Invoke();

        }
    }
}
