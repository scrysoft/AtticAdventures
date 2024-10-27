using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform targetPosition;
    public string animationTriggerName;

    public Animator animator;

    public void PlayAnimation()
    {
        if (string.IsNullOrEmpty(animationTriggerName))
        {
            return;
        }

        if (animator != null)
        {
            animator.Play(animationTriggerName);
        }
    }

    public void SpawnPrefab()
    {
        if (prefabToSpawn == null || targetPosition == null)
        {
            return;
        }

        Opsive.Shared.Game.ObjectPoolBase.Instantiate(prefabToSpawn, targetPosition.position, targetPosition.rotation);
    }
}
